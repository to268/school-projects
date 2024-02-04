#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <assert.h>
#include <stdbool.h>
#include <pthread.h>
#include "matrix_inverse.h"
#include "common/socket.h"
#include "common/matrix_inverse.h"

#define MAX_SIZE 4096

#if defined(CPU_COUNT)
#define THREADS_COUNT CPU_COUNT
#else
#define THREADS_COUNT 4
#endif

#define ELEMENTS_PER_THREAD(els) (els / THREADS_COUNT)

typedef double matrix_t[MAX_SIZE][MAX_SIZE];

typedef struct matrix_inverse_ctx {
    matrix_t matrix;                 /* Base matrix */
    matrix_t result;                 /* Result matrix */
    int matrix_size;                 /* Matrix size	*/
    int max_elements;                /* Max number of elements */
    enum matrix_init_type init_type; /* Matrix init type */
    bool is_debug;                   /* Print switch */
    FILE* fd;                        /* Where to print */
} matrix_inverse_ctx_t;

struct thread_ctx {
    int start_idx;                    /* Start index */
    int end_idx;                      /* End index */
    matrix_inverse_ctx_t* matrix_ctx; /* Matrix inverse context */
};

void set_init_type(matrix_inverse_ctx_t* options, const char* type) {
    if (!strcmp(type, "fast")) {
        options->init_type = FAST;
        return;
    }

    if (!strcmp(type, "rand")) {
        options->init_type = RANDOM;
        return;
    }

    __builtin_unreachable();
}

void print_state(matrix_inverse_ctx_t* ctx, const char* state) {
    int row = 0;
    int col = 0;

    fprintf(ctx->fd, "State: %s\n", state);
    fprintf(ctx->fd, "Matrix:\n");
    for (row = 0; row < ctx->matrix_size; row++) {
        for (col = 0; col < ctx->matrix_size; col++)
            printf(" %5.2f", ctx->matrix[row][col]);
        printf("\n");
    }

    fprintf(ctx->fd, "Result:\n");
    for (row = 0; row < ctx->matrix_size; row++) {
        for (col = 0; col < ctx->matrix_size; col++)
            fprintf(ctx->fd, " %5.2f", ctx->result[row][col]);
        fprintf(ctx->fd, "\n");
    }

    fprintf(ctx->fd, "\n\n");
}

int handle_matrix_arguments(struct matinvpar_args* args, FILE* fd) {
    if (args->needs_help) {
        fprintf(fd, "\nHELP: try matinv -u \n\n");
        return EXIT_FAILURE;
    }

    if (args->needs_usage) {
        fprintf(fd, "\nUsage: matinv [-n problemsize]\n");
        fprintf(fd, "           [-D] show default values \n");
        fprintf(fd, "           [-h] help \n");
        fprintf(fd, "           [-I init_type] fast/rand \n");
        fprintf(fd, "           [-m maxnum] max random no \n");
        fprintf(fd, "           [-P print_switch] 0/1 \n");
        return EXIT_FAILURE;
    }

    if (args->needs_defaults) {
        fprintf(fd, "\nDefault:  n         = 5");
        fprintf(fd, "\n          Init      = fast");
        fprintf(fd, "\n          maxnum    = 15 ");
        fprintf(fd, "\n          P         = 1 \n\n");
        return EXIT_FAILURE;
    }

    return 0;
}

void* inverse_matrix_thread(void* arg) {
    struct thread_ctx* ctx = (struct thread_ctx*)arg;
    matrix_t* matrix = &ctx->matrix_ctx->matrix;
    matrix_t* result = &ctx->matrix_ctx->result;
    int matrix_size = ctx->matrix_ctx->matrix_size;

    int row = ctx->start_idx;
    int col = 0;
    /* 'p' stands for pivot (numbered from 0 to N-1) */
    int p = ctx->start_idx;
    double pivot_val = 0;

    /* Bringing the matrix A to the identity form */
    for (; p < ctx->end_idx; p++) {
        pivot_val = (*matrix)[p][p];
        for (col = 0; col < matrix_size; col++) {
            (*matrix)[p][col] = (*matrix)[p][col] / pivot_val;
            (*result)[p][col] = (*result)[p][col] / pivot_val;
        }
        assert((*matrix)[p][p] == 1.0);

        double multiplier = 0;
        for (row = ctx->start_idx; row < ctx->end_idx; row++) {
            multiplier = (*matrix)[row][p];

            if (row != p) {
                for (col = 0; col < matrix_size; col++) {
                    (*matrix)[row][col] =
                        (*matrix)[row][col] - (*matrix)[p][col] * multiplier;

                    (*result)[row][col] =
                        (*result)[row][col] - (*result)[p][col] * multiplier;
                }
                assert((*matrix)[row][p] == 0.0);
            }
        }
    }

    return NULL;
}

void inverse_matrix(matrix_inverse_ctx_t* ctx) {
    pthread_t threads[THREADS_COUNT];
    struct thread_ctx threads_data[THREADS_COUNT];
    int elements_per_thread = ELEMENTS_PER_THREAD(ctx->matrix_size);

    for (int i = 0; i < THREADS_COUNT; i++) {
        struct thread_ctx* current_data = &threads_data[i];
        current_data->matrix_ctx = ctx;
        current_data->start_idx = elements_per_thread * i;

        if (i == THREADS_COUNT - 1)
            current_data->end_idx = ctx->matrix_size;
        else
            current_data->end_idx = elements_per_thread * (i + 1);

        pthread_create(&threads[i], NULL, inverse_matrix_thread,
                       (void*)current_data);
    }

    for (int i = 0; i < THREADS_COUNT; i++)
        pthread_join(threads[i], NULL);

    /* Final merging thread */
    pthread_t final_thread;
    struct thread_ctx final_thread_data = {0, ctx->matrix_size, ctx};
    pthread_create(&final_thread, NULL, inverse_matrix_thread,
                   (void*)&final_thread_data);

    pthread_join(final_thread, NULL);
}

void init_matrix(matrix_inverse_ctx_t* ctx) {
    int row = 0;
    int col = 0;
    int matrix_size = ctx->matrix_size;
    int max_elems = ctx->max_elements;

    for (int r = 0; r < matrix_size; r++)
        for (int c = 0; c < matrix_size; c++)
            if (r == c)
                ctx->result[r][c] = 1.0;

    fprintf(ctx->fd, "\nsize      = %dx%d ", matrix_size, matrix_size);
    fprintf(ctx->fd, "\nmaxnum    = %d \n", ctx->max_elements);
    fprintf(ctx->fd, "Init	  = %s \n", ctx->init_type ? "rand" : "fast");
    fprintf(ctx->fd, "Initializing matrix...");

    if (ctx->init_type == RANDOM) {
        for (row = 0; row < matrix_size; row++) {
            for (col = 0; col < matrix_size; col++) {
                double random_no = (double)(rand() % max_elems);
                /* Diagonal dominance */
                if (row == col)
                    ctx->matrix[row][col] = random_no + 5.0;
                else
                    ctx->matrix[row][col] = random_no + 1.0;
            }
        }
    } else if (ctx->init_type == FAST) {
        for (row = 0; row < matrix_size; row++) {
            for (col = 0; col < matrix_size; col++) {
                /* Diagonal dominance */
                if (row == col)
                    ctx->matrix[row][col] = 5.0;
                else
                    ctx->matrix[row][col] = 2.0;
            }
        }
    }

    fprintf(ctx->fd, "done \n\n");
    if (ctx->is_debug) {
        print_state(ctx, "Begin: Input");
    }
}

void destroy_matrix_inverse_ctx(matrix_inverse_ctx_t* ctx) { free(ctx); }

matrix_inverse_ctx_t* create_matrix_inverse_ctx(struct matinvpar_args* args,
                                                FILE* fd) {
    matrix_inverse_ctx_t* ctx = calloc(1, sizeof(matrix_inverse_ctx_t));
    ctx->matrix_size = args->problem_size;
    ctx->max_elements = args->max_rand_no;
    ctx->init_type = args->init_type;
    ctx->is_debug = args->needs_print;
    ctx->fd = fd;

    return ctx;
}

int matrix_main(struct matinvpar_args* args, FILE* fd) {
    fprintf(fd, "Matrix Inverse\n");

    if (handle_matrix_arguments(args, fd))
        return 1;

    matrix_inverse_ctx_t* ctx = create_matrix_inverse_ctx(args, fd);

    init_matrix(ctx);
    inverse_matrix(ctx);

    if (ctx->is_debug) {
        print_state(ctx, "End: Input");
    }

    destroy_matrix_inverse_ctx(ctx);
    return 0;
}
