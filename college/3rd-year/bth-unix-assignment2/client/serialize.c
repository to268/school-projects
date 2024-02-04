#include <stdio.h>
#include <string.h>
#include "base.h"
#include "socket.h"
#include "arguments.h"

#define MAX_WORDS 24
#define NONE      0

enum matinv_flags {
    HELP = 1,
    DEFAULT_VALUES = 2,
    USAGE = 3,
    PROBLEM_SIZE = 4 | ARG_BIT_MASK,
    INIT_TYPE = 5 | ARG_BIT_MASK,
    MAXNUM = 6 | ARG_BIT_MASK,
    PRINT_SWITCH = 7 | ARG_BIT_MASK,
};

static const char* matinv_args[] = {
    [HELP] = "-h",         [DEFAULT_VALUES] = "-D", [USAGE] = "-u",
    [PROBLEM_SIZE] = "-n", [INIT_TYPE] = "-I",      [MAXNUM] = "-m",
    [PRINT_SWITCH] = "-P",
};

static const int matinv_count = sizeof(matinv_args) / sizeof(matinv_args[0]);

enum kmeans_flags {
    FILE_NAME = 1 | ARG_BIT_MASK,
    CLUSTER_COUNT = 2 | ARG_BIT_MASK,
};

static const char* kmeans_args[] = {
    [FILE_NAME] = "-f",
    [CLUSTER_COUNT] = "-k",
};

static const int kmeans_count = sizeof(kmeans_args) / sizeof(kmeans_args[0]);

static const char* algorithms[] = {
    [MATINVPAR] = "matinvpar",
    [KMEANSPAR] = "kmeanspar",
};

static const int algorithms_count = sizeof(algorithms) / sizeof(algorithms[0]);

struct matched_minv_flag {
    enum matinv_flags kind;
    char value[MAX_ARG_BUF];
};

int match_algorithm(request_t* req, const char* word) {
    for (int i = 0; i < algorithms_count; i++) {
        if (!strncmp(algorithms[i], word, BUFFER_SIZE)) {
            req->algorithm = i;
            return 0;
        }
    }

    return error("unknown algorithm: \"%s\"\n", word);
}

int assign_matinv_args(request_t* req, struct matched_flag* flag) {
    switch ((enum matinv_flags)flag->kind) {

    case HELP:
        req->minv_args.needs_help = true;
        return 0;

    case DEFAULT_VALUES:
        req->minv_args.needs_defaults = true;
        return 0;

    case USAGE:
        req->minv_args.needs_usage = true;
        return 0;

    case PROBLEM_SIZE:
        req->minv_args.problem_size = atoi(flag->value);

        if (req->minv_args.problem_size == 0)
            return error("Failed to parse problem size: \"%s\"\n", flag->value);

        return 0;

    case INIT_TYPE:
        if (!strncmp(flag->value, "fast", 4)) {
            req->minv_args.init_type = FAST;
            return 0;
        } else if (!strncmp(flag->value, "rand", 4)) {
            req->minv_args.init_type = RANDOM;
            return 0;
        }
        return error("Invalid init type: \"%s\"\n", flag->value);

    case MAXNUM:
        req->minv_args.max_rand_no = atoi(flag->value);

        if (req->minv_args.max_rand_no == 0)
            return error("Failed to parse maxnum: \"%s\"\n", flag->value);

        return 0;

    case PRINT_SWITCH: {
        int value = atoi(flag->value);
        req->minv_args.needs_print = value;

        if (value < 0 || value > 1)
            return error("Invalid print switch value: \"%s\"\n", flag->value);

        return 0;
    }

    default:
        break;
    }

    __builtin_unreachable();
}

int assign_kmeans_args(request_t* req, struct matched_flag* flag) {
    switch ((enum kmeans_flags)flag->kind) {
    case FILE_NAME: {
        FILE* f = fopen(flag->value, "r");

        if (f == NULL)
            return error("Invalid file: \"%s\"\n", flag->value);

        fread(req->kmeans_args.input_file, MAX_FILE_SIZE - 1, 1, f);
        return fclose(f);
    }

    case CLUSTER_COUNT:
        req->kmeans_args.cluster_count = atoi(flag->value);

        if (req->kmeans_args.cluster_count == 0)
            return error("Failed to parse cluster count: \"%s\"\n",
                         flag->value);

        return 0;

    default:
        break;
    }

    __builtin_unreachable();
}

int serialize(request_t* req, const char* cmdline) {
    char words[BUFFER_SIZE][MAX_WORDS];
    int words_count = 0;

    const char* previous_word = cmdline;
    size_t cmdline_len = strlen(cmdline);

    for (size_t i = 0; i < cmdline_len; i++) {
        if (words_count == MAX_WORDS)
            return error("too much arguments\n");

        if (cmdline[i] == ' ' || i == cmdline_len - 1) {
            memset(words[i], 0, BUFFER_SIZE);
            size_t length = (size_t)(&cmdline[i] - previous_word);

            strncpy(words[words_count], previous_word, length);
            words[words_count][length] = '\0';

            previous_word = &cmdline[i + 1];
            words_count++;
            continue;
        }
    }

    if (match_algorithm(req, words[0]))
        return 1;

    switch (req->algorithm) {
    case MATINVPAR:
        req->minv_args.init_type = FAST;
        req->minv_args.problem_size = 5;
        req->minv_args.max_rand_no = 15;
        req->minv_args.needs_print = true;
        req->minv_args.needs_usage = false;

        for (int i = 1; i < words_count; i++) {

            struct matched_flag flag = {.kind = NONE};
            int processed = match_flag(&flag, words[i], words[i + 1],
                                       matinv_args, matinv_count);
            if (processed == -1)
                return error("unknown flag\n");

            if (assign_matinv_args(req, &flag))
                return 1;

            i += processed;
        }
        return 0;

    case KMEANSPAR:
        req->kmeans_args.cluster_count = 9;

        for (int i = 1; i < words_count; i++) {
            struct matched_flag flag = {.kind = NONE};
            int processed = match_flag(&flag, words[i], words[i + 1],
                                       kmeans_args, kmeans_count);
            if (processed == -1)
                return error("unknown flag\n");

            if (assign_kmeans_args(req, &flag))
                return 1;

            i += processed;
        }
        return 0;

    default:
        break;
    }

    __builtin_unreachable();
}
