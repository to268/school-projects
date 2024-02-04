#!/usr/bin/env bash

old=$IFS
IFS=$'\n'

if [ $# -ne 1 ]; then
    echo "usage: $0 <input-file>"
    exit -1
fi

libcalc="lib/libcalc.a"
helpers="src/asm-helpers.c"
file=$1
out=$(basename ${file%%.*})
out_c="$out.c"
variables=($(cat $file | grep -E "[a-z](\s?)=(\s?).*;" | tr -d '\t| ' | cut -d'=' -f1 | sort | uniq))

cat <<EOF > $out_c
#include <stdio.h>
#include <stdlib.h>
#include <stdint.h>

extern void print(void);
extern double fact(double x);
extern double gcd(double a, double b);
extern double lntwo(double x);

static uint64_t stack[1024];
static uint64_t stackPointer = 0;

void push(uint64_t x) {
    if (stackPointer >= 1024) {
        fprintf(stderr, "undefined stack state, when trying to push to a full stack\n");
        exit(-1);
    }

    stack[stackPointer] = x;
    stackPointer++;
}

uint64_t pop(void) {
    if (stackPointer == 0) {
        fprintf(stderr, "undefined stack state, when trying to pop on an empty stack\n");
        exit(-1);
    }

    return stack[--stackPointer];
}

EOF

for i in "${variables[@]}"; do
    value=$(cat $file | grep -E "$i=[0-9]" | head -n1 | cut -d'=' -f2 | sed 's/;//')
    if [ -z $value ]; then
        value="0"
    fi
    echo -e "static uint64_t $i = $value;" >> $out_c
done
echo "" >> $out_c

echo "int main(void) {" >> $out_c

# Insert generated C code
./bin/calc3c.exe < $1 >> $out_c

echo "}" >> $out_c

# Compile, assemble and link
gcc -std=c2x $out_c $libcalc -o $out

IFS=$old
