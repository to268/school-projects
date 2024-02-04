#!/usr/bin/env bash

blur_bin="../blur_par"
threshold_bin="../threshold_par"
threads=4

check_requirements() {
    valgrind_path=$(command -v valgrind)

    if [ ! -f $blur_bin ]; then
        echo "Blur binary not found"
        exit 1
    fi
    if [ ! -f $threshold_bin ]; then
        echo "Threshold binary not found"
        exit 1
    fi

    if [ -z $valgrind_path ]; then
        echo "Valgrind not found"
        exit 1
    fi
}

valgrind_profiling() {
    blur_tmp=$(mktemp)
    threshold_tmp=$(mktemp)

    input="../data/im2.ppm"
    output_blur_valgrind="../stats_out/blur-valgrind.callgrind.out"
    output_threshold_valgrind="../stats_out/threshold-valgrind.callgrind.out"

    echo "[*] Starting Valgrind profiling"

    echo "[*] Profiling blur"
    valgrind --tool=callgrind -- ./$blur_bin 15 $input $blur_tmp $threads
    mv callgrind.out* $output_blur_valgrind

    echo "[*] Profiling threshold"
    valgrind --tool=callgrind -- ./$threshold_bin $input $threshold_tmp $threads
    mv callgrind.out* $output_threshold_valgrind

    rm -f $blur_tmp
    rm -f $threshold_tmp

    echo "[*] Valgrind profiling finished"
}

check_requirements
valgrind_profiling
