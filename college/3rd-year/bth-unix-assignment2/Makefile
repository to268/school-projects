CC?=gcc
NCPUS=$(shell nproc 2>/dev/null || sysctl -n hw.ncpu 2>/dev/null || echo 1)
CFLAGS=-std=c2x -Og -g3 -Wall -Wextra -pipe -DCPU_COUNT=$(NCPUS)
LDFLAGS?=-lpthread

MATHSERVER_SRCDIR=./mathserver/src
MATHSERVER_SRC=$(wildcard $(MATHSERVER_SRCDIR)/*.c)
MATHSERVER_INCLUDES=-I ./mathserver/include
MATHSERVER_OBJDIR=./mathserver/objects
MATHSERVER_OBJS=$(patsubst $(MATHSERVER_SRCDIR)/%.c,$(MATHSERVER_OBJDIR)/%.o,$(MATHSERVER_SRC))
MATHSERVER_RESULTS_DIR=./mathserver/computed_results

CLIENT_SRCDIR=./client
CLIENT_SRC=$(wildcard $(CLIENT_SRCDIR)/*.c)
CLIENT_INCLUDES=-I ./mathserver/include/common
CLIENT_OBJDIR=./client/objects
CLIENT_OBJS=$(patsubst $(CLIENT_SRCDIR)/%.c,$(CLIENT_OBJDIR)/%.o,$(CLIENT_SRC))
CLIENT_RESULTS_DIR=./client/results

MATHSERVER_BIN=./server-bin
CLIENT_BIN=./client-bin

all: server client

release: CFLAGS=-std=c2x -O2 -DNDEBUG -fpie -Wunused -Wimplicit-fallthrough \
		-Werror=format-security -Wformat -Werror=implicit-function-declaration \
		-fstack-protector-strong -pipe -Wfloat-equal -Wundef -Wshadow \
		-Wpointer-arith -Wcast-align -Wstrict-prototypes -Wstrict-overflow=5 \
		-Wstring-compare -Wwrite-strings -Waggregate-return -Wcast-qual \
		-Wswitch-default -Wswitch-enum -Wconversion -Wunreachable-code -flto
release: server client

server: $(MATHSERVER_OBJDIR) $(MATHSERVER_BIN)

client: $(CLIENT_OBJDIR) $(CLIENT_BIN)

$(MATHSERVER_OBJDIR):
	mkdir -p $(MATHSERVER_OBJDIR)

$(CLIENT_OBJDIR):
	mkdir -p $(CLIENT_OBJDIR)

$(MATHSERVER_OBJDIR)/%.o: $(MATHSERVER_SRCDIR)/%.c
	$(CC) $(CFLAGS) $(MATHSERVER_INCLUDES) -c $< -o $@

$(MATHSERVER_BIN): $(MATHSERVER_OBJS)
	$(CC) $(LDFLAGS) $^ -o $@

$(CLIENT_OBJDIR)/%.o: $(CLIENT_SRCDIR)/%.c
	$(CC) $(CFLAGS) $(CLIENT_INCLUDES) -c $< -o $@

$(CLIENT_BIN): $(CLIENT_OBJS)
	$(CC) $(LDFLAGS) $^ -o $@

clang-tidy:
	./utils/run-clang-tidy.py

clean:
	rm -rf $(MATHSERVER_OBJDIR)
	rm -rf $(CLIENT_OBJDIR)

mrproper: clean
	rm -f $(MATHSERVER_BIN) $(CLIENT_BIN)
	rm -rf $(CLIENT_REUSLTS_DIR)/*_results
	rm -rf $(MATHSERVER_RESULTS_DIR)/*.txt

.PHONY: all release server client clang-tidy clean mrproper
