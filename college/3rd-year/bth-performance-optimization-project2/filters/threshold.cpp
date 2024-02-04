/*
Author: David Holmqvist <daae19@student.bth.se>
*/

#include "filters.hpp"
#include "matrix.hpp"
#include "ppm.hpp"
#include <cstdlib>
#include <iostream>

auto main(int argc, char const* argv[]) -> int
{
    if (argc != 3) {
        std::cerr << "Usage: " << argv[0] << " [infile] [outfile]" << '\n';
        std::exit(1);
    }

    PPM::Reader reader {};
    PPM::Writer writer {};

    auto m { reader(argv[1]) };
    auto thresholded { Filter::threshold(m) };

    writer(thresholded, argv[2]);

    return 0;
}
