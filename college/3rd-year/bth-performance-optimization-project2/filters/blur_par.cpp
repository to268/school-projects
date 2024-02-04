/*
Author: David Holmqvist <daae19@student.bth.se>
*/

#include "matrix.hpp"
#include "ppm.hpp"
#include "filters_par.hpp"
#include <cstdlib>
#include <iostream>

auto main(int argc, char const* argv[]) -> int
{
    if (argc != 5) {
        std::cerr << "Usage: " << argv[0] << " [radius] [infile] [outfile] [num_threads]" << '\n';
        std::exit(1);
    }

    PPM::Reader reader {};
    PPM::Writer writer {};

    auto m { reader(argv[2]) };
    auto radius { static_cast<unsigned>(std::stoul(argv[1])) };

    auto blurred { Filter::blur(m, radius, static_cast<unsigned>(std::stoul(argv[4]))) };
    writer(blurred, argv[3]);

    return 0;
}
