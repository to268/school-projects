AR?=ar
CC?=gcc
LD?=ld
CFLAGS=-std=c2x -Og -g3 -Wall -Wextra -pipe
LDFLAGS?=

CLIENT_SRCDIR=./client
CLIENT_SRC=$(wildcard $(CLIENT_SRCDIR)/*.c)
CLIENT_INCLUDES=-I ./mathserver/include/common
CLIENT_OBJDIR=./client/objects
CLIENT_OBJS=$(patsubst $(CLIENT_SRCDIR)/%.c,$(CLIENT_OBJDIR)/%.o,$(CLIENT_SRC))
CLIENT_RESULTS_DIR=./client/results

MATHSERVER_BIN=./server-bin
CLIENT_BIN=./client-bin

BIN_DIR=bin
LIB_DIR=lib
SRC_DIR=src

LEXYACC_SRCDIR=lexyacc-code
LEXYACC_OBJS=$(LEXYACC_SRCDIR)/y.tab.o $(LEXYACC_SRCDIR)/lex.yy.o

ASM_FILES=$(wildcard $(SRC_DIR)/*.s)
C_FILES=$(wildcard $(SRC_DIR)/*.c)
OBJS=$(ASM_FILES:.s=.o)
STATIC_LIB=$(LIB_DIR)/libcalc.a
DYNAMIC_LIB=$(LIB_DIR)/libcalc.so

TEST_FILES=$(wildcard tests/*.c)
TEST_BINS=$(TEST_FILES:.c=.elf)

all: lexyacc libcalc

release: CFLAGS=-std=c2x -O2 -DNDEBUG -Wall -Wextra -fpie -Wunused -Wimplicit-fallthrough \
		-Werror=format-security -Wformat -Werror=implicit-function-declaration \
		-fstack-protector-strong -pipe -Wfloat-equal -Wundef -Wshadow \
		-Wpointer-arith -Wcast-align -Wstrict-prototypes -Wstrict-overflow=5 \
		-Wstring-compare -Wwrite-strings -Waggregate-return -Wcast-qual \
		-Wswitch-default -Wswitch-enum -Wconversion -Wunreachable-code -flto
release: lexyacc-release libcalc

$(BIN_DIR):
	mkdir -p $@

$(LIB_DIR):
	mkdir -p $@

lexyacc: $(BIN_DIR)
	$(MAKE) -C $(LEXYACC_SRCDIR)
	mv $(LEXYACC_SRCDIR)/calc3a.exe $(BIN_DIR)/calc3a.exe
	mv $(LEXYACC_SRCDIR)/calc3b.exe $(BIN_DIR)/calc3b.exe
	mv $(LEXYACC_SRCDIR)/calc3i.exe $(BIN_DIR)/calc3i.exe
	mv $(LEXYACC_SRCDIR)/calc3g.exe $(BIN_DIR)/calc3g.exe
	mv $(LEXYACC_SRCDIR)/calc3c.exe $(BIN_DIR)/calc3c.exe

lexyacc-release: $(BIN_DIR)
	$(MAKE) -C $(LEXYACC_SRCDIR) release
	mv $(LEXYACC_SRCDIR)/calc3a.exe $(BIN_DIR)/calc3a.exe
	mv $(LEXYACC_SRCDIR)/calc3b.exe $(BIN_DIR)/calc3b.exe
	mv $(LEXYACC_SRCDIR)/calc3i.exe $(BIN_DIR)/calc3i.exe
	mv $(LEXYACC_SRCDIR)/calc3g.exe $(BIN_DIR)/calc3g.exe
	mv $(LEXYACC_SRCDIR)/calc3c.exe $(BIN_DIR)/calc3c.exe

libcalc: $(LIB_DIR) $(OBJS)
	$(AR) rcs $(STATIC_LIB) $(OBJS)
	$(LD) -shared $(LDFLAGS) -fPIC -o $(DYNAMIC_LIB) $(OBJS)

$(OBJS): $(ASM_FILES)
	$(CC) $(CFLAGS) -c $*.s -o $@

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

check: $(STATIC) $(SHARED) $(TEST_BINS)
	for test in $(TEST_BINS); do ./$$test; done

$(TEST_BINS): CFLAGS=-Wall -O2 -fPIC -I tests/
$(TEST_BINS): lexyacc libcalc $(TEST_FILES)
	$(CC) $(CFLAGS) $(TEST_FILES) $(STATIC_LIB) -o $@

clang-tidy:
	./utils/run-clang-tidy.py

clean:
	$(MAKE) -C $(LEXYACC_SRCDIR) clean
	rm -f $(OBJS)
	rm -f $(TEST_BINS)

mrproper: clean
	rm -rf $(BIN_DIR)
	rm -rf $(LIB_DIR)

.PHONY: all release lexyacc lexyacc-release libcalc check clang-tidy clean mrproper
