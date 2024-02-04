#!/usr/bin/env bash

blur_bin="../blur"
threshold_bin="../threshold"

check_requirements() {
    perf_path=$(command -v perf)
    perl_path=$(command -v perl)

    if [ ! -f $blur_bin ]; then
        echo "Blur binary not found"
        exit 1
    fi
    if [ ! -f $threshold_bin ]; then
        echo "Threshold binary not found"
        exit 1
    fi

    if [ -z $perf_path ]; then
        echo "Perf not found"
        exit 1
    fi

    if [ -z $perl_path ]; then
        echo "Perl not found"
        exit 1
    fi
}

perf_profiling() {
    blur_tmp=$(mktemp)
    threshold_tmp=$(mktemp)

    input="../data/im4.ppm"
    output_blur_perf="../stats_out/blur-perf"
    output_threshold_perf="../stats_out/threshold-perf"

    echo "[*] Starting Perf profiling"

    echo "[*] Profiling blur"
    perf stat -o "$output_blur_perf-stats.txt" -- ./$blur_bin 15 $input $blur_tmp
    perf record -F 99 -g -- ./$blur_bin 15 $input $blur_tmp
    perf script | ./stackcollapse-perf.pl | ./flamegraph.pl - > "$output_blur_perf-flamegraph.svg"

    echo "[*] Profiling threshold"
    perf stat -o "$output_threshold_perf-stats.txt" -- ./$threshold_bin $input $threshold_tmp
    perf record -F 99 -g -- ./$threshold_bin $input $threshold_tmp
    perf script | ./stackcollapse-perf.pl | ./flamegraph.pl - > "$output_threshold_perf-flamegraph.svg"

    rm -f $blur_tmp
    rm -f $threshold_tmp
    rm -f perf.data*

    echo "[*] Perf profiling finished"
}

check_requirements
perf_profiling
