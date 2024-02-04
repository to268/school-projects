#ifndef ARGUMENTS_H
#define ARGUMENTS_H

#include <stdint.h>

enum strategy {
    FORK,
    MUXBASIC,
    MUXSCALE,
};

struct server_args {
    uint16_t port;
    bool daemon_mode;
    bool needs_help;
    enum strategy strategy;
};

[[nodiscard]] int parse_arguments(struct server_args* serv_args, int argc,
                                  char** argv);

#endif /* ARGUMENTS_H */
