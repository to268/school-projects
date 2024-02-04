#!/usr/bin/env bash

old=$IFS
IFS=$'\n'

if [ $# -ne 1 ]; then
    echo "usage: $0 <input-file>"
    exit -1
fi

# Variables
libcalc="lib/libcalc.a"
file=$1
out=$(basename ${file%%.*})
out_asm="$out.s"
out_obj="$out.o"
variables=($(cat $file | grep -E "[a-z](\s?)=(\s?).*;" | tr -d '\t| ' | cut -d'=' -f1 | sort | uniq))

# Remove output file if exists
[ -f $out_asm ] && rm $out_asm

# Add text segment
echo ".global main" >> $out_asm
echo -e ".text\n" >> $out_asm
echo "main:" >> $out_asm

# Insert generated assembly
./bin/calc3i.exe < $1 >> $out_asm

# Add exit label
echo "_exit:" >> $out_asm
echo -e "\tmovq\t\$0x3c, %rax" >> $out_asm
echo -e "\txor\t%rdi, %rdi" >> $out_asm
echo -e "\tsyscall" >> $out_asm

# Add variable definitions
echo -e "\n.data" >> $out_asm
for i in "${variables[@]}"; do
    value=$(cat $file | grep -E "$i=[0-9]" | head -n1 | cut -d'=' -f2 | sed 's/;//')
    if [ -z $value ]; then
        value="0"
    fi
    echo -e "$i:\t.quad\t$value" >> $out_asm
done

# Assemble and link
gcc -no-pie $out_asm $libcalc -o $out

IFS=$old
