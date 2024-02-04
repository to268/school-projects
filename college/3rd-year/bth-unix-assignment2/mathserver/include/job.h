#ifndef JOB_H
#define JOB_H

#include <stdio.h>
#include "common/socket.h"

void queue_job(request_t* req, FILE* fd);

#endif /* JOB_H */
