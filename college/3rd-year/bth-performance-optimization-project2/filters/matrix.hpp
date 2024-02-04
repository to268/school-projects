/*
Author: David Holmqvist <daae19@student.bth.se>
*/

#include <cstdint>

#if !defined(MATRIX_HPP)
#define MATRIX_HPP

class Matrix {
private:
    uint8_t* R;
    uint8_t* G;
    uint8_t* B;

    unsigned x_size;
    unsigned y_size;
    unsigned color_max;
    unsigned size = x_size * y_size;

public:
    Matrix();
    Matrix(unsigned dimension);
    Matrix(unsigned xsize, unsigned ysize);
    Matrix(const Matrix& other);
    Matrix(uint8_t* R, uint8_t* G, uint8_t* B, unsigned x_size, unsigned y_size, unsigned color_max);
    auto operator=(const Matrix other) -> Matrix&;
    ~Matrix();

    [[nodiscard]] constexpr auto get_x_size() const -> unsigned
    {
        return x_size;
    }

    [[nodiscard]] constexpr auto get_y_size() const -> unsigned
    {
        return y_size;
    }

    [[nodiscard]] constexpr auto get_color_max() const -> unsigned
    {
        return color_max;
    }

    [[nodiscard]] constexpr auto get_R() const -> uint8_t const*
    {
        return R;
    }

    [[nodiscard]] constexpr auto get_G() const -> uint8_t const*
    {
        return G;
    }

    [[nodiscard]] constexpr auto get_B() const -> uint8_t const*
    {
        return B;
    }

    // TODO: Clean get_size() calls
    [[nodiscard]] constexpr auto get_size() const -> unsigned
    {
        return size;
    }

    [[nodiscard]] static constexpr auto get_size(unsigned x_size, unsigned y_size) -> unsigned
    {
        return x_size * y_size;
    }

    [[nodiscard]] constexpr auto r(unsigned x, unsigned y) const -> uint8_t
    {
        return R[y * x_size + x];
    }

    [[nodiscard]] constexpr auto g(unsigned x, unsigned y) const -> uint8_t
    {
        return G[y * x_size + x];
    }

    [[nodiscard]] constexpr auto b(unsigned x, unsigned y) const -> uint8_t
    {
        return B[y * x_size + x];
    }

    constexpr auto r(unsigned x, unsigned y) -> uint8_t&
    {
        return R[y * x_size + x];
    }

    constexpr auto g(unsigned x, unsigned y) -> uint8_t&
    {
        return G[y * x_size + x];
    }

    constexpr auto b(unsigned x, unsigned y) -> uint8_t&
    {
        return B[y * x_size + x];
    }
};

#endif
