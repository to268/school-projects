#!/usr/bin/env bash

blur_bin="../blur"
threshold_bin="../threshold"

check_requirements() {
    dot_path=$(command -v dot)
    opt_path=$(command -v opt)

    if [ -z $opt_path ]; then
        echo "Opt not found"
        exit 1
    fi

    if [ -z $dot_path ]; then
        echo "Dot not found"
        exit 1
    fi
}

llvm-ir-cfg-graphs() {
    demangled_file="../cfg_graphs/demangled.txt"

    echo "[*] Generating LLVM IR CFG graphs"

    rm -f $demangled_file

    for ir in ../*.o; do
        opt -disable-output -passes=dot-cfg $ir
    done

    for dot in .*.dot; do
        out="${dot#.}"
        out="${out%.*}"
        demangled="$(llvm-cxxfilt $out)"
        dot -Tpng $dot -o "../cfg_graphs/$out.png"
        echo "$demangled -> $out" >> $demangled_file
    done

    rm -f ".*.dot"

    echo "[*] Generation finished"
}

check_requirements
llvm-ir-cfg-graphs
