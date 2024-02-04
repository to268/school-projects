/*
Author: David Holmqvist <daae19@student.bth.se>
*/

#include "matrix.hpp"

#if !defined(FILTERS_HPP)
#define FILTERS_HPP

namespace Filter {

namespace Gauss {
    constexpr unsigned max_radius { 1000 };
    constexpr float max_x { 1.33f };
    constexpr float pi { 3.14159f };

    void get_weights(const unsigned n, double* weights_out);
}

auto blur(Matrix& m, const unsigned radius, unsigned num_threads) -> Matrix;
auto threshold(Matrix& m, unsigned num_threads) -> Matrix;

}

#endif
