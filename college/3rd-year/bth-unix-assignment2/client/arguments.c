#include <assert.h>
#include <stdlib.h>
#include <stdbool.h>
#include <stdint.h>
#include <string.h>
#include "base.h"
#include "arguments.h"
#include "socket.h"

struct client_args {
    char ip[BUFFER_SIZE];
    uint16_t port;
};

enum flag_kind {
    NONE = 0,
    IP = 1 | ARG_BIT_MASK,
    PORT = 2 | ARG_BIT_MASK,
};

static const char* flags[] = {
    [IP] = "-ip",
    [PORT] = "-p",
};

static const int flags_count = sizeof(flags) / sizeof(flags[0]);

[[nodiscard]] int assign_argument(struct client_args* client_args,
                                  struct matched_flag* flag) {
    switch ((enum flag_kind)flag->kind) {
    case NONE:
    default:
        break;

    case IP:
        strncpy(client_args->ip, flag->value, BUFFER_SIZE);
        return 0;

    case PORT:
        assert(flag->value != NULL);
        client_args->port = (uint16_t)atoi(flag->value);

        if (client_args->port == 0)
            return error("Failed to parse port number: \"%s\"\n", flag->value);

        return 0;

        return error("Unknown strategy: \"%s\"\n", flag->value);
    }

    __builtin_unreachable();
}

int parse_client_arguments(struct client_args* client_args, int argc,
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

            if (assign_argument(client_args, &flag))
                return EXIT_FAILURE;

            i += processed_count;
            continue;
        }

        return error("unknown parameter: \"%s\"\n", current_arg);
    }

    return 0;
}
