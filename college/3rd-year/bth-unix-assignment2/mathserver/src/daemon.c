#define _POSIX_C_SOURCE 200809L

#include <stdio.h>
#include <syslog.h>
#include <signal.h>
#include <fcntl.h>
#include <unistd.h>
#include <sys/stat.h>
#include <sys/resource.h>
#include "common/base.h"

void daemonize(void) {
    pid_t pid;
    struct rlimit rl;

    /* Set umask */
    umask(077);

    /* Fork */
    if ((pid = fork()) < 0)
        exit(error("can't fork\n"));
    else if (pid != 0)
        exit(0);

    if (setsid() < 0)
        exit(error("setsid() failed\n"));

    /* Ignore SIGHUP */
    signal(SIGHUP, SIG_IGN);

    /* Fork again */
    if ((pid = fork()) < 0)
        exit(error("can't fork\n"));
    else if (pid != 0) /* parent */
        exit(0);

    /* Print PID */
    printf("%d\n", getpid());

    /* Set process working directory to / */
    if (chdir("/") < 0)
        exit(error("can't change directory to /"));

    /* Get file descriptors limit  */
    if (getrlimit(RLIMIT_NOFILE, &rl) < 0)
        exit(error("can't get file limit\n"));

    /* Close all file descriptors  */
    if (rl.rlim_max == RLIM_INFINITY)
        rl.rlim_max = 1024;
    for (int i = 0; i < (int)rl.rlim_max; i++)
        close(i);

    /* Set stdin/stdout/stderr to /dev/null */
    int fd0 = open("/dev/null", O_RDWR);
    int fd1 = dup(0);
    int fd2 = dup(0);

    /* Initialize syslog */
    openlog("mathserver", LOG_CONS, LOG_DAEMON);
    if (fd0 != 0 || fd1 != 1 || fd2 != 2) {
        syslog(LOG_ERR, "unexpected file descriptors %d %d %d\n", fd0, fd1,
               fd2);
        exit(1);
    }
}
