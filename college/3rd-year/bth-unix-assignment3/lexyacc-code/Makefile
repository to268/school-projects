CC?=gcc
CFLAGS=-std=gnu2x -Og -g3 -pipe

all: calc

release: CFLAGS=-std=gnu2x -O2 -DNDEBUG -fpie -flto
release: calc

calc:
	bison -y -d calc3.y
	flex calc3.l
	$(CC) $(CFLAGS) -c y.tab.c lex.yy.c
	$(CC) $(CFLAGS) -c calc3i.c -o calc3i.o
	$(CC) $(CFLAGS) y.tab.o lex.yy.o calc3a.c -o calc3a.exe
	$(CC) $(CFLAGS) y.tab.o lex.yy.o calc3b.c -o calc3b.exe
	$(CC) $(CFLAGS) y.tab.o lex.yy.o calc3i.o -o calc3i.exe
	$(CC) $(CFLAGS) y.tab.o lex.yy.o calc3g.c -o calc3g.exe
	$(CC) $(CFLAGS) y.tab.o lex.yy.o calc3c.c -o calc3c.exe

clean:
	rm -f *.o
	rm -f lex.yy*
	rm -f y.tab*

.PHONY: calc clean
