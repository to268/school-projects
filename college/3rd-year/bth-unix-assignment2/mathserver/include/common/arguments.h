#ifndef COMMON_ARGUMENTS_H
#define COMMON_ARGUMENTS_H

#include <string.h>
#include "base.h"

#define MAX_ARG_BUF 24
/* Mask to check if the argument is followed by a value */
#define ARG_BIT_MASK (1 << 2)

struct matched_flag {
    int kind;
    char value[MAX_ARG_BUF];
};

[[nodiscard]] static int match_flag(struct matched_flag* flag,
                                    char* current_arg, char* next_arg,
                                    const char** flags, int flags_count) {
    for (int i = 0; i < flags_count; i++) {
        if (flags[i] == NULL)
            continue;

        if (!strncmp(current_arg, flags[i], MAX_ARG_BUF)) {
            flag->kind = i;

            if (i & ARG_BIT_MASK) {
                if (next_arg == NULL)
                    return error("The argument \"%s\" requires a value\n",
                                 current_arg) -
                           2;

                strncpy(flag->value, next_arg, MAX_ARG_BUF - 1);
                flag->value[strlen(flag->value)] = '\0';
                return 1;
            }

            return 0;
        }
    }

    return error("Unknown argument \"%s\"\n", current_arg) - 2;
}

#endif /* COMMON_ARGUMENTS_H */
