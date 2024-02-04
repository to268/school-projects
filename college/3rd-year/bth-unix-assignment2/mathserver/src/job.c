#include <stdio.h>
#include "kmeans.h"
#include "matrix_inverse.h"
#include "common/socket.h"
#include "job.h"

void queue_job(request_t* req, FILE* fd) {
    switch (req->algorithm) {

    case MATINVPAR:
        matrix_main(&req->minv_args, fd);
        return;

    case KMEANSPAR:
        kmeans_main(&req->kmeans_args, fd);
        return;

    default:
        break;
    }

    __builtin_unreachable();
}
