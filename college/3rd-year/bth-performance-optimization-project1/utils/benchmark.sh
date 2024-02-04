#!/usr/bin/env bash

blur_bin="../blur"
threshold_bin="../threshold"

check_requirements() {
    hyperfine_path=$(command -v hyperfine)
    sar_path=$(command -v sar)

    if [ ! -f $blur_bin ]; then
        echo "Blur binary not found"
        exit 1
    fi
    if [ ! -f $threshold_bin ]; then
        echo "Threshold binary not found"
        exit 1
    fi

    if [ -z $hyperfine_path ]; then
        echo "Hyperfine not found"
        exit 1
    fi

    if [ -z $sar_path ]; then
        echo "Sar not found"
        exit 1
    fi
}


hyperfine_benchmark() {
    blur_tmp=$(mktemp)
    threshold_tmp=$(mktemp)

    output_blur_stats="../stats_out/blur-hyperfine.txt"
    output_threshold_stats="../stats_out/threshold-hyperfine.txt"

    echo "[*] Starting hyperfine benchmarking"

    rm -f $output_blur_stats
    rm -f $output_threshold_stats

    for input in ../data/*.ppm; do
        echo "[*] Benchmarking blur ($input)"
        hyperfine --warmup 10 --output=pipe "./$blur_bin 15 $input $blur_tmp" >> $output_blur_stats

        echo "[*] Benchmarking threshold ($input)"
        hyperfine --warmup 40 --output=pipe "./$threshold_bin $input $threshold_tmp" >> $output_threshold_stats
    done

    rm -f $blur_tmp
    rm -f $threshold_tmp

    echo "[*] Hyperfine benchmarking finished"
}

io_benchmark() {
    blur_tmp=$(mktemp)
    threshold_tmp=$(mktemp)

    output_blur_io="../stats_out/blur-io.txt"
    output_threshold_io="../stats_out/threshold-io.txt"

    echo "[*] Starting I/O benchmarking"

    rm -f $output_blur_io
    rm -f $output_threshold_io

    for input in ../data/*.ppm; do
        echo "[*] Benchmarking blur ($input)"
        sar -b 1 >> $output_blur_io &
        echo "$input:" >> $output_blur_io
        sleep 1
        for i in {1..10}; do
            ./$blur_bin 15 $input $blur_tmp
        done
        sleep 1
        pkill sar

        echo "[*] Benchmarking threshold ($input)"
        sar -b 1 >> $output_threshold_io &
        echo "$input:" >> $output_threshold_io
        sleep 1
        for i in {1..40}; do
            ./$threshold_bin $input $threshold_tmp
        done
        sleep 1
        pkill sar
    done

    rm -f $blur_tmp
    rm -f $threshold_tmp

    echo "[*] I/O benchmarking finished"
}

check_requirements
hyperfine_benchmark
io_benchmark
