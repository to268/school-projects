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

auto blur(const Matrix& m, const unsigned radius) -> Matrix;
auto threshold(const Matrix& m) -> Matrix;

}

#endif
