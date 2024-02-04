#ifndef COMMON_BASE_H
#define COMMON_BASE_H

#include <stdio.h>
#include <stdlib.h>
#include <stdarg.h>

#define DEFAULT_PORT 4278

[[nodiscard]] static int error(const char* format, ...) {
    va_list ap;
    va_start(ap, format);
    vfprintf(stderr, format, ap);
    va_end(ap);
    return EXIT_FAILURE;
}

#endif /* COMMON_BASE_H */
