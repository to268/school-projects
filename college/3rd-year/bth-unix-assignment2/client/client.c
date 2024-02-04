#define _POSIX_C_SOURCE 200809L

#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <string.h>
#include <arpa/inet.h>
#include <netdb.h>
#include "base.h"
#include "socket.h"

#define MAX_UNIQUE_NAME_SIZE 128
#define OUTPUT_DIRECTORY     "client/results"

struct client_args {
    char ip[BUFFER_SIZE];
    uint16_t port;
};

extern int serialize(request_t* req, const char* cmdline);
extern int parse_client_arguments(struct client_args* client_args, int argc,
                                  char** argv);

static int sock_fd = -1;

void generate_unique_filename(char* buff) {
    char temp[MAX_UNIQUE_NAME_SIZE];
    strcpy(buff, "client");

    strncpy(temp, buff, MAX_UNIQUE_NAME_SIZE);
    int i = 1;
    while (1) {
        char filename[MAX_UNIQUE_NAME_SIZE / 2];
        char tmp[MAX_UNIQUE_NAME_SIZE / 2];

        strncpy(filename, temp, MAX_UNIQUE_NAME_SIZE / 2);
        sprintf(tmp, "%d_results", i);
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

[[nodiscard]] int resolve_hostname(const char* hostname, char* ip_buf) {
    struct addrinfo request;
    struct addrinfo* result;

    memset(&request, 0, sizeof(request));
    request.ai_family = AF_INET;
    request.ai_socktype = SOCK_STREAM;

    if (getaddrinfo(hostname, NULL, &request, &result))
        return error("getdaddrinfo failed\n");

    /* Getting the first result of the linked list */
    struct in_addr* addr = &((struct sockaddr_in*)result->ai_addr)->sin_addr;
    inet_ntop(result->ai_family, (void*)addr, ip_buf, INET_ADDRSTRLEN);

    freeaddrinfo(result);
    return 0;
}

void write_output(awnser_t* res) {
    char filename[MAX_UNIQUE_NAME_SIZE];
    generate_unique_filename(filename);

    FILE* fd = fopen(filename, "w");
    fprintf(fd, "%s", res->output);
    fclose(fd);
}

void cleanup(void) { close(sock_fd); }

int main(int argc, char* argv[]) {
    struct client_args args = {.port = DEFAULT_PORT};

    if (parse_client_arguments(&args, argc, argv))
        return error("error parsing the arguments\n");

    struct sockaddr_in serv_addr;
    char ip_buf[INET_ADDRSTRLEN];
    sock_fd = socket(AF_INET, SOCK_STREAM, 0);

    if (atexit(cleanup))
        return error("cannot register atexit function\n");

    if (sock_fd < 0)
        return error("error connecting socket\n");

    if (resolve_hostname(args.ip, ip_buf))
        return error("error resolving hostname\n");

    serv_addr.sin_family = AF_INET;
    serv_addr.sin_addr.s_addr = inet_addr(ip_buf);
    serv_addr.sin_port = htons(args.port);

    if (connect(sock_fd, (struct sockaddr*)&serv_addr, sizeof(serv_addr)) < 0)
        return error("connection failed\n");

    printf("Connected to server\n");

    char req_buf[sizeof(request_t)];
    char res_buf[sizeof(awnser_t)];

    ssize_t n = 0;
    while (1) {
        printf("Enter a command for the server: ");

        memset(&req_buf, 0, BUFFER_SIZE);
        fgets(req_buf, BUFFER_SIZE - 1, stdin);

        request_t req;
        if (serialize(&req, req_buf))
            continue;

        memcpy(req_buf, &req, sizeof(request_t));
        n = send(sock_fd, req_buf, sizeof(request_t), 0);
        if (n < 0)
            return error("error occured on sending\n");

        memset(&req_buf, 0, BUFFER_SIZE);
        n = recv(sock_fd, res_buf, sizeof(awnser_t), MSG_WAITALL);

        awnser_t res;
        memcpy(&res, res_buf, sizeof(awnser_t));
        write_output(&res);

        if (n < 0)
            return error("error on recieving\n");

        printf("Received the solution: %s\n", req_buf);
    }

    return EXIT_SUCCESS;
}
