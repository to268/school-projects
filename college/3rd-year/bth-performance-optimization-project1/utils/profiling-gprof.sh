#!/usr/bin/env bash

blur_bin="../blur"
threshold_bin="../threshold"

check_requirements() {
    gprof_path=$(command -v gprof)
    dot_path=$(command -v dot)

    if [ ! -f $blur_bin ]; then
        echo "Blur binary not found"
        exit 1
    fi
    if [ ! -f $threshold_bin ]; then
        echo "Threshold binary not found"
        exit 1
    fi

    if [ -z $gprof_path ]; then
        echo "Gprof not found"
        exit 1
    fi

    if [ -z $dot_path ]; then
        echo "Dot not found"
        exit 1
    fi
}

gpof_profiling() {
    blur_tmp=$(mktemp)
    threshold_tmp=$(mktemp)

    input="../data/im4.ppm"
    output_blur_gprof="../stats_out/blur-gprof"
    output_threshold_gprof="../stats_out/threshold-gprof"

    echo "[*] Starting GProf profiling"

    echo "[*] Profiling blur"
    ./$blur_bin 15 $input $blur_tmp
    gprof -flat-profile ./$blur_bin gmon.out > "$output_blur_gprof-flat.txt"
    gprof ./$blur_bin gmon.out | ./gprof2dot.py | dot -Tpng -o "$output_blur_gprof-graph.png"

    echo "[*] Profiling threshold"
    ./$threshold_bin $input $threshold_tmp
    gprof -flat-profile ./$threshold_bin gmon.out > "$output_threshold_gprof-flat.txt"
    gprof ./$threshold_bin gmon.out | ./gprof2dot.py | dot -Tpng -o "$output_threshold_gprof-graph.png"

    rm -f $blur_tmp
    rm -f $threshold_tmp
    rm -f gmon.out

    echo "[*] GProf profiling finished"
}

check_requirements
gpof_profiling
