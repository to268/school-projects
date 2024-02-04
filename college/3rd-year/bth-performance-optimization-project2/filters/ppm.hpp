/*
Author: David Holmqvist <daae19@student.bth.se>
*/

#include "matrix.hpp"
#include <sstream>

#if !defined(PPM_READER_HPP)
#define PPM_READER_HPP

namespace PPM {

void error(std::string_view op, std::string_view what);

constexpr unsigned max_dimension { 3000 };
constexpr unsigned max_pixels { max_dimension * max_dimension };
constexpr char const* magic_number { "P6" };

class Reader {
private:
    std::stringstream stream;

    [[nodiscard]] auto get_magic_number() -> std::string;
    [[nodiscard]] auto get_dimensions() -> std::pair<unsigned, unsigned>;
    [[nodiscard]] auto get_data(unsigned x_size, unsigned y_size) -> std::tuple<unsigned char*, unsigned char*, unsigned char*>;
    [[nodiscard]] auto get_color_max() -> unsigned;
    void fill(const std::string& filename);

public:
    auto operator()(const std::string& filename) -> Matrix;
};

class Writer {
public:
    void operator()(const Matrix& m, const std::string& filename);
};

}

#endif
