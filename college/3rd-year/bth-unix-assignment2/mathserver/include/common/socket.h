#ifndef COMMON_SOCKET_H
#define COMMON_SOCKET_H

#include <stdbool.h>
#include "matrix_inverse.h"

#define MAX_FILE_SIZE 128000
#define BUFFER_SIZE   256

enum algorithm {
    MATINVPAR,
    KMEANSPAR,
};

struct matinvpar_args {
    int problem_size;
    bool needs_defaults;
    bool needs_help;
    bool needs_usage;
    enum matrix_init_type init_type;
    int max_rand_no;
    bool needs_print;
};

struct kmeanspar_args {
    char input_file[MAX_FILE_SIZE];
    int cluster_count;
};

typedef struct awnser {
    char output[MAX_FILE_SIZE];
} awnser_t;

typedef struct request {
    enum algorithm algorithm;
    union {
        struct matinvpar_args minv_args;
        struct kmeanspar_args kmeans_args;
    };
} request_t;

#endif /* COMMON_SOCKET_H */
