# Author: David Holmqvist <daae19@student.bth.se>

CXX?=g++
CXXFLAGS= \
	-std=c++17 \
	-O3 \
	-Wall \
	-Wextra \
	-Wunused \
	-Wcast-align \
	-Wcast-qual \
	-Werror=old-style-cast \
	-Werror=unused-result \
	-Werror=vla \
	-Wfloat-conversion \
	-Wmisleading-indentation \
	-Wmissing-declarations \
	-Wnull-dereference \
	-Wpointer-arith \
	-flto=auto \
	-Wno-sign-compare

STATS_DIR=stats_out
CFG_GRAPHS_DIR=cfg_graphs
CXX_OPT_LEVEL=-O3

all: blur threshold baseline blur_par threshold_par

debug: CXXFLAGS=-std=c++17 -Og -g3
debug: blur threshold blur_par threshold_par

threshold: matrix ppm filters threshold.cpp
	$(CXX) $(CXXFLAGS) $(LDFLAGS) threshold.cpp matrix.o ppm.o filters.o -o threshold

blur: matrix ppm filters blur.cpp
	$(CXX) $(CXXFLAGS) $(LDFLAGS) blur.cpp matrix.o ppm.o filters.o -o blur

blur_par: matrix ppm filters_par blur_par.cpp
	$(CXX) $(CXXFLAGS) $(LDFLAGS) -fopenmp blur_par.cpp matrix.o ppm.o filters_par.o -o blur_par

threshold_par: matrix ppm filters_par threshold_par.cpp
	$(CXX) $(CXXFLAGS) $(LDFLAGS) -fopenmp threshold_par.cpp matrix.o ppm.o filters_par.o -o threshold_par

filters_par: matrix filters_par.hpp filters_par.cpp
	$(CXX) $(CXXFLAGS) -fopenmp -c filters_par.cpp -o filters_par.o

filters: matrix filters.hpp filters.cpp
	$(CXX) $(CXXFLAGS) -c filters.cpp -o filters.o

matrix: matrix.hpp matrix.cpp
	$(CXX) $(CXXFLAGS) -c matrix.cpp -o matrix.o

ppm: ppm.hpp ppm.cpp
	$(CXX) $(CXXFLAGS) -c ppm.cpp -o ppm.o

baseline:
	$(MAKE) -C baseline

$(STATS_DIR):
	mkdir $@

gather-stats: benchmark profiling

benchmark: CXXFLAGS=-std=c++17 -flto=auto $(CXX_OPT_LEVEL)
benchmark: clean $(STATS_DIR) blur_par threshold
	cd utils/ && bash benchmark.sh

profiling: profiling-perf profiling-valgrind profiling-gprof

profiling-gprof: CXXFLAGS=-std=c++17 -flto=auto -pg $(CXX_OPT_LEVEL)
profiling-gprof: clean $(STATS_DIR) blur threshold
	cd utils/ && bash profiling-gprof.sh

profiling-perf: CXXFLAGS=-std=c++17 -flto=auto $(CXX_OPT_LEVEL)
profiling-perf: clean $(STATS_DIR) blur threshold
	cd utils/ && bash profiling-perf.sh

profiling-valgrind: CXXFLAGS=-std=c++17 -g3 -flto=auto $(CXX_OPT_LEVEL)
profiling-valgrind: clean $(STATS_DIR) blur threshold
	cd utils/ && bash profiling-valgrind.sh

$(CFG_GRAPHS_DIR):
	mkdir $@

llvm-ir-cfg-graphs: CXX=clang++
llvm-ir-cfg-graphs: CXXFLAGS=-std=c++17 -S -emit-llvm -flto=auto $(CXX_OPT_LEVEL)
llvm-ir-cfg-graphs: $(CFG_GRAPHS_DIR) matrix ppm filters
	cd utils/ && bash llvm-ir-cfg-graphs.sh

verify: CXXFLAGS=-std=c++17 $(CXX_OPT_LEVEL)
verify: clean baseline blur_par threshold_par
	bash verify.sh ./blur_par ./threshold_par

clean:
	rm -rf blur threshold *.ppm *.o *.dSYM 2> /dev/null
	$(MAKE) -C baseline clean

clean-stats:
	rm -rf $(STATS_DIR)

clean-cfg-graphs:
	rm -rf $(CFG_GRAPHS_DIR)

mrproper: clean clean-stats clean-cfg-graphs

.ONESHELL:
.PHONY: filters matrix ppm baseline gather-stats benchmark profiling profiling-gprof profiling-valgrind profiling-perf clean clean-stats clean-cfg-graphs
