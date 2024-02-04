#include <assert.h>
#include <stdlib.h>
#include <stdbool.h>
#include <stdint.h>
#include <string.h>
#include "common/base.h"
#include "common/arguments.h"
#include "arguments.h"

enum flag_kind {
    NONE = 0,
    HELP = 1,
    DAEMON_MODE = 2,
    PORT = 3 | ARG_BIT_MASK,
    STRATEGY = 4 | ARG_BIT_MASK,
};

static const char* flags[] = {
    [HELP] = "-h",
    [DAEMON_MODE] = "-d",
    [PORT] = "-p",
    [STRATEGY] = "-s",
};

static const int flags_count = sizeof(flags) / sizeof(flags[0]);

static const char* strategies[] = {
    [FORK] = "fork",
    [MUXBASIC] = "muxbasic",
    [MUXSCALE] = "muxscale",
};

static const int strategies_count = sizeof(strategies) / sizeof(strategies[0]);

[[nodiscard]] int assign_argument(struct server_args* serv_args,
                                  struct matched_flag* flag) {
    switch ((enum flag_kind)flag->kind) {
    case NONE:
    default:
        break;

    case HELP:
        serv_args->needs_help = true;
        return 0;

    case DAEMON_MODE:
        serv_args->daemon_mode = true;
        return 0;

    case PORT:
        assert(flag->value != NULL);
        serv_args->port = (uint16_t)atoi(flag->value);

        if (serv_args->port == 0)
            return error("Failed to parse port number: \"%s\"\n", flag->value);

        return 0;

    case STRATEGY:
        assert(flag->value != NULL);
        for (int i = 0; i < strategies_count; i++) {
            if (!strncmp(strategies[i], flag->value, MAX_ARG_BUF)) {
                serv_args->strategy = i;
                return 0;
            }
        }

        return error("Unknown strategy: \"%s\"\n", flag->value);
    }

    __builtin_unreachable();
}

[[nodiscard]] int parse_arguments(struct server_args* serv_args, int argc,
                                  char** argv) {
    for (int i = 1; i < argc; i++) {
        char* current_arg = argv[i];
        char* next_arg = argv[i + 1];

        if (current_arg[0] == '-') {
            struct matched_flag flag = {.kind = NONE};
            int processed_count =
                match_flag(&flag, current_arg, next_arg, flags, flags_count);

            if (processed_count == -1)
                return EXIT_FAILURE;

            if (assign_argument(serv_args, &flag))
                return EXIT_FAILURE;

            i += processed_count;
            continue;
        }

        return error("unknown parameter: \"%s\"\n", current_arg);
    }

    return 0;
}
