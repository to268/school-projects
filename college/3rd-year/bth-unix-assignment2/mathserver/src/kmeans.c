#define _POSIX_C_SOURCE 200809L

#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <limits.h>
#include <string.h>
#include <pthread.h>
#include "common/socket.h"

#define MAX_POINTS    4096
#define MAX_CENTROIDS 32

#if defined(CPU_COUNT)
#define THREADS_COUNT CPU_COUNT
#else
#define THREADS_COUNT 4
#endif

#define LINES_PER_THREAD(lines) (lines / THREADS_COUNT)

struct point {
    float x;       /* The x-coordinate of the point */
    float y;       /* The y-coordinate of the point */
    int centroids; /* The centroids that the point belongs to */
};

struct file_data {
    char* file;     /* File buffer */
    int line_count; /* Number of lines in the file */
};

typedef struct kmeans_ctx {
    int centroid_count;                    /* Number of centroids */
    pthread_mutex_t lock;                  /* Mutex Lock */
    struct point data[MAX_POINTS];         /* Data coordinates */
    struct point centroids[MAX_CENTROIDS]; /* Coordinates of each cluster */
    struct file_data file;                 /* File data */
} kmeans_ctx_t;

struct thread_ctx {
    kmeans_ctx_t* kmeans_ctx;
    pthread_mutex_t* lock;
    int first_point_idx;
    int last_point_idx;
};

void read_data(kmeans_ctx_t* ctx) {
    /* Initialize points from the data file */
    char* buffer = ctx->file.file;
    for (int i = 0; i < ctx->file.line_count; i++) {
        float x = 0;
        float y = 0;

        sscanf(buffer, "%f %f", &x, &y);
        ctx->data[i].x = x;
        ctx->data[i].y = y;
        ctx->data[i].centroids = -1;

        while (*buffer && *buffer != '\n')
            buffer++;
        buffer++;
    }

    /* Choose random points as centroids */
    srand(0);
    for (int centroid = 0; centroid < ctx->centroid_count; centroid++) {
        int random_point = rand() % ctx->file.line_count;
        ctx->centroids[centroid].x = ctx->data[random_point].x;
        ctx->centroids[centroid].y = ctx->data[random_point].y;
    }
}

int get_closest_centroid(const kmeans_ctx_t* ctx, int i) {
    /* find the nearest centroid */
    int nearest_centroids = -1;
    double xdist = 0;
    double ydist = 0;
    double dist = INT_MAX;
    double min_dist = INT_MAX;

    for (int centroid = 0; centroid < ctx->centroid_count; centroid++) {
        /*
         * Calculate the square of the Euclidean distance
         * between that centroid and the point
         */
        xdist = ctx->data[i].x - ctx->centroids[centroid].x;
        ydist = ctx->data[i].y - ctx->centroids[centroid].y;
        dist = xdist * xdist + ydist * ydist;

        if (dist <= min_dist) {
            min_dist = dist;
            nearest_centroids = centroid;
        }
    }

    return nearest_centroids;
}

bool assign_centroids_to_points(kmeans_ctx_t* ctx, pthread_mutex_t* lock) {
    bool has_changed = false;
    int old_centroids = -1;
    int new_centroids = -1;

    for (int i = 0; i < ctx->file.line_count; i++) {
        old_centroids = ctx->data[i].centroids;
        new_centroids = get_closest_centroid(ctx, i);

        /* Assign a centroids to the point */
        pthread_mutex_lock(lock);
        ctx->data[i].centroids = new_centroids;
        pthread_mutex_unlock(lock);

        if (old_centroids != new_centroids)
            has_changed = true;
    }

    return has_changed;
}

void update_centroids_centers(kmeans_ctx_t* ctx, pthread_mutex_t* lock) {
    /* Update the centroids centers */
    int c;
    int centroids_points[MAX_CENTROIDS] = {0};
    struct point temp[MAX_CENTROIDS] = {{0.0, 0.0, -1}};

    for (int i = 0; i < ctx->file.line_count; i++) {
        c = ctx->data[i].centroids;
        centroids_points[c]++;
        temp[c].x += ctx->data[i].x;
        temp[c].y += ctx->data[i].y;
    }

    pthread_mutex_lock(lock);
    for (int i = 0; i < ctx->centroid_count; i++) {
        ctx->centroids[i].x = temp[i].x / (float)centroids_points[i];
        ctx->centroids[i].y = temp[i].y / (float)centroids_points[i];
    }
    pthread_mutex_unlock(lock);
}

void* kmeans_thread(void* arg) {
    struct thread_ctx* ctx = (struct thread_ctx*)arg;
    bool has_changed = false;

    do {
        has_changed = assign_centroids_to_points(ctx->kmeans_ctx, ctx->lock);
        update_centroids_centers(ctx->kmeans_ctx, ctx->lock);
    } while (has_changed);

    return NULL;
}

void kmeans(kmeans_ctx_t* ctx) {
    pthread_t threads[THREADS_COUNT];
    struct thread_ctx threads_data[THREADS_COUNT];

    int thread_lines = LINES_PER_THREAD(ctx->file.line_count);

    /* Assign work to threads and launch them */
    for (int i = 0; i < THREADS_COUNT; i++) {
        struct thread_ctx* current_data = &threads_data[i];
        current_data->kmeans_ctx = ctx;
        current_data->lock = &ctx->lock;
        current_data->first_point_idx = thread_lines * i;

        if (i == THREADS_COUNT - 1)
            current_data->last_point_idx = ctx->file.line_count;
        else
            current_data->last_point_idx = thread_lines * (i + 1);

        pthread_create(&threads[i], NULL, kmeans_thread, (void*)current_data);
    }

    /* Wait for the threads */
    for (int i = 0; i < THREADS_COUNT; i++)
        pthread_join(threads[i], NULL);
}

void write_results(kmeans_ctx_t* ctx, FILE* fd) {
    for (int i = 0; i < ctx->file.line_count; i++) {
        fprintf(fd, "%.2f %.2f %d\n", ctx->data[i].x, ctx->data[i].y,
                ctx->data[i].centroids);
    }
}

int get_kmeans_data_lines_count(const char* file) {
    int line_count = 0;

    while (*file) {
        if (*file == '\n')
            line_count++;
        file++;
    }

    if (file[-1] != '\n')
        file++;

    return line_count;
}

int read_kmeans_data(struct file_data* file_mmap, char* file) {
    file_mmap->file = file;
    file_mmap->line_count = get_kmeans_data_lines_count(file);

    return 0;
}

void destroy_kmeans_ctx(kmeans_ctx_t* ctx) {
    pthread_mutex_destroy(&ctx->lock);
    free(ctx);
}

kmeans_ctx_t* create_kmeans_ctx(struct kmeanspar_args* args) {
    kmeans_ctx_t* ctx = calloc(1, sizeof(kmeans_ctx_t));
    ctx->centroid_count = args->cluster_count;
    read_kmeans_data(&ctx->file, args->input_file);
    pthread_mutex_init(&ctx->lock, NULL);

    return ctx;
}

int kmeans_main(struct kmeanspar_args* args, FILE* fd) {
    kmeans_ctx_t* ctx = create_kmeans_ctx(args);

    read_data(ctx);
    kmeans(ctx);

    write_results(ctx, fd);
    destroy_kmeans_ctx(ctx);
    return 0;
}
