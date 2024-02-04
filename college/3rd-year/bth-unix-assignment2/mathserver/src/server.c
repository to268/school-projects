#define _POSIX_C_SOURCE 200809L

#include <stdio.h>
#include <string.h>
#include <unistd.h>
#include <stdbool.h>
#include <fcntl.h>
#include <sys/socket.h>
#include <arpa/inet.h>
#include <sys/syslog.h>
#include <sys/select.h>
#include <sys/epoll.h>
#include "common/base.h"
#include "common/socket.h"
#include "arguments.h"
#include "daemon.h"
#include "job.h"

#define MAX_CLIENTS          10
#define MAX_EVENTS           MAX_CLIENTS
#define MAX_UNIQUE_NAME_SIZE 128
#define OUTPUT_DIRECTORY     "mathserver/computed_results"

void generate_unique_filename(enum algorithm algorithm, int client_no,
                              char* buff) {
    char temp[MAX_UNIQUE_NAME_SIZE];

    switch (algorithm) {

    case MATINVPAR:
        strcpy(buff, "matinv_client");
        break;

    case KMEANSPAR:
        strcpy(buff, "kmeans_client");
        break;

    default:
        __builtin_unreachable();
    }

    sprintf(temp, "%d", client_no);
    strcat(buff, temp);

    strcat(buff, "soln");

    strncpy(temp, buff, MAX_UNIQUE_NAME_SIZE);
    int i = 1;
    while (1) {
        char filename[MAX_UNIQUE_NAME_SIZE / 2];
        char tmp[MAX_UNIQUE_NAME_SIZE / 2];

        strncpy(filename, temp, MAX_UNIQUE_NAME_SIZE / 2);
        sprintf(tmp, "%d.txt", i);
        strcat(filename, tmp);

        char full_path[MAX_UNIQUE_NAME_SIZE];
        snprintf(full_path, sizeof(full_path), "%s/%s", OUTPUT_DIRECTORY,
                 filename);

        if (access(full_path, F_OK) == -1) {
            strncpy(buff, full_path, MAX_UNIQUE_NAME_SIZE);
            break;
        }
        i++;
    }
}

void handle_client(int client_socket, int client_no) {
    char req_buf[sizeof(request_t)];
    char res_buf[sizeof(awnser_t)];
    ssize_t bytes_read = 0;

    /* Receive and process client messages */
    do {
        FILE* fd = NULL;
        bytes_read =
            recv(client_socket, req_buf, sizeof(request_t), MSG_WAITALL);

        if (bytes_read <= 0)
            break;

        printf("received bytes %ld\n", bytes_read);

        /* Process received requests */
        printf("Received message from client %d\n", client_no);

        /* Read request */
        request_t req;
        memcpy(&req, req_buf, sizeof(request_t));

        /* Execute request and store result */
        char filename[MAX_UNIQUE_NAME_SIZE];
        generate_unique_filename(req.algorithm, client_no, filename);

        fd = fopen(filename, "w");
        if (fd == NULL)
            return;

        queue_job(&req, fd);
        fclose(fd);

        fd = fopen(filename, "r");
        if (fd == NULL)
            return;

        /* Send result */
        awnser_t res;
        memset(&res, 0, sizeof(awnser_t));
        fread(res.output, MAX_FILE_SIZE - 1, 1, fd);
        memcpy(res_buf, &res, sizeof(awnser_t));

        /* Send the response to the client */
        send(client_socket, res_buf, sizeof(awnser_t), 0);
        fclose(fd);
    } while (bytes_read > 0);

    /* Client disconnected or error occurred */
    if (bytes_read == 0)
        printf("Client %d disconnected\n", client_no);

    /* Close the client socket */
    close(client_socket);
}

int muxscale_strategy(int server_socket, int* client_count) {
    int epoll_fd = epoll_create1(0);
    struct epoll_event event;
    struct epoll_event events[MAX_EVENTS];

    if (epoll_fd == -1)
        return error("cannot create epoll");

    event.events = EPOLLIN;
    event.data.fd = server_socket;
    if (epoll_ctl(epoll_fd, EPOLL_CTL_ADD, server_socket, &event) == -1) {
        close(epoll_fd);
        return error("cannot control epoll\n");
    }

    while (1) {
        int num_events = epoll_wait(epoll_fd, events, MAX_EVENTS, -1);
        if (num_events == -1) {
            close(epoll_fd);
            return error("cannot wait epoll\n");
        }

        for (int i = 0; i < num_events; i++) {
            int fd = events[i].data.fd;
            if (fd == server_socket) {
                struct sockaddr_in client_addr;
                socklen_t client_addr_len = sizeof(client_addr);
                int client_socket =
                    accept(server_socket, (struct sockaddr*)&client_addr,
                           &client_addr_len);

                if (client_socket == -1)
                    continue;

                event.events = EPOLLIN;
                event.data.fd = client_socket;
                if (epoll_ctl(epoll_fd, EPOLL_CTL_ADD, client_socket, &event) ==
                    -1) {
                    close(epoll_fd);
                    return error("cannot control epoll client\n");
                }

                printf("New client connected: %s\n",
                       inet_ntoa(client_addr.sin_addr));
                (*client_count)++;
            } else {
                handle_client(fd, *client_count);
                epoll_ctl(epoll_fd, EPOLL_CTL_DEL, fd, NULL);
            }
        }
    }

    close(epoll_fd);
    return 0;
}

int muxbasic_strategy(int server_socket, int* client_count) {
    fd_set current_sockets;
    fd_set ready_sockets;

    FD_ZERO(&current_sockets);
    FD_SET(server_socket, &current_sockets);

    int biggest_sock = server_socket;

    /* Accept and handle client connections */
    while (1) {
        ready_sockets = current_sockets;

        if (select(biggest_sock + 1, &ready_sockets, NULL, NULL, NULL) < 0)
            return error("select");

        for (int i = 0; i <= biggest_sock; i++) {
            if (FD_ISSET(i, &ready_sockets)) {
                if (i == server_socket) {
                    struct sockaddr_in client_addr;
                    socklen_t client_addr_len = sizeof(client_addr);
                    int client_socket =
                        accept(server_socket, (struct sockaddr*)&client_addr,
                               &client_addr_len);

                    if (client_socket == -1)
                        continue;

                    FD_SET(client_socket, &current_sockets);
                    printf("New client connected: %s\n",
                           inet_ntoa(client_addr.sin_addr));
                    (*client_count)++;

                    if (biggest_sock < client_socket)
                        biggest_sock = client_socket;
                } else {
                    handle_client(i, *client_count);
                    FD_CLR(i, &current_sockets);
                }
            }
        }
    }

    return 0;
}

int fork_strategy(int server_socket, int* client_count) {
    /* Accept and handle client connections */
    while (1) {
        struct sockaddr_in client_addr;
        socklen_t client_addr_len = sizeof(client_addr);

        int client_socket = accept(
            server_socket, (struct sockaddr*)&client_addr, &client_addr_len);

        if (client_socket == -1)
            continue;

        /* Fork a new process to handle the client */
        pid_t pid = fork();
        if (pid < 0) {
            return error("fork\n");
        } else if (pid == 0) {
            printf("New client connected: %s\n",
                   inet_ntoa(client_addr.sin_addr));
            (*client_count)++;
            /* Child process */
            close(server_socket);

            /* Handle client */
            handle_client(client_socket, *client_count);
        } else {
            /* Parent process */
            close(client_socket);
        }
    }

    return 0;
}

int create_socket(struct server_args* args) {
    int server_socket = -1;
    struct sockaddr_in server_addr;

    /* Create the server socket */
    if ((server_socket = socket(AF_INET, SOCK_STREAM, 0)) == -1)
        return error("socket\n");

    /* Set up the server address structure */
    memset(&server_addr, 0, sizeof(server_addr));
    server_addr.sin_family = AF_INET;
    server_addr.sin_port = htons(args->port);
    server_addr.sin_addr.s_addr = INADDR_ANY;

    /* Bind the server socket to the specified address and port */
    if (bind(server_socket, (struct sockaddr*)&server_addr,
             sizeof(server_addr)) == -1) {
        return error("bind\n");
    }

    /* Listen the client connections */
    if (listen(server_socket, MAX_CLIENTS) == -1)
        return error("listen\n");

    printf("Server listening on port %d...\n", args->port);
    int client_count = 0;

    switch (args->strategy) {
    case FORK:
        if (fork_strategy(server_socket, &client_count))
            return error("failed to use fork strategy\n");
        break;

    case MUXBASIC:
        if (muxbasic_strategy(server_socket, &client_count))
            return error("failed to use muxbasic strategy\n");
        break;

    case MUXSCALE:
        if (muxscale_strategy(server_socket, &client_count))
            return error("failed to use muxscale strategy\n");
        break;

    default:
        __builtin_unreachable();
    }

    /* Close the server socket */
    close(server_socket);
    return 0;
}

int show_help_banner(void) {
    // TODO: Print help banner
    printf("Usage: Kmeans [-n problemsize]\n");
    printf("    [-d] Run as a daemon\n");
    printf("    [-p] Listen to port number (default: 4278)\n");
    printf("    [-s] Specify the request handling strategy: fork, muxbasic, "
           "muxscale (default: fork)\n");
    printf("    [-h] Print help text\n");

    return 0;
}

int main(int argc, char** argv) {
    struct server_args args = {.port = DEFAULT_PORT,
                               .daemon_mode = false,
                               .needs_help = false,
                               .strategy = FORK};

    if (parse_arguments(&args, argc, argv))
        return error("error parsing the arguments\n");

    if (args.needs_help)
        return show_help_banner();

    if (args.daemon_mode)
        daemonize();

    if (create_socket(&args))
        return error("error creating socket\n");

    if (args.daemon_mode)
        closelog();

    return 0;
}
