/*
Author: David Holmqvist <daae19@student.bth.se>
*/

#include "matrix.hpp"
#include "ppm.hpp"
#include <cstdint>

Matrix::Matrix(uint8_t* R, uint8_t* G, uint8_t* B, unsigned x_size, unsigned y_size, unsigned color_max)
    : R { R }
    , G { G }
    , B { B }
    , x_size { x_size }
    , y_size { y_size }
    , color_max { color_max }
{
}

Matrix::Matrix()
    : Matrix {
        nullptr,
        nullptr,
        nullptr,
        0,
        0,
        0,
    }
{
}

Matrix::Matrix(unsigned dimension)
    : R { new uint8_t[static_cast<size_t>(dimension * dimension)] }
    , G { new uint8_t[static_cast<size_t>(dimension * dimension)] }
    , B { new uint8_t[static_cast<size_t>(dimension * dimension)] }
    , x_size { dimension }
    , y_size { dimension }
    , color_max { 0 }
{
}

Matrix::Matrix(unsigned int xsize, unsigned int ysize)
        : R { new uint8_t[static_cast<size_t>(xsize * ysize)] }
        , G { new uint8_t[static_cast<size_t>( xsize * ysize)] }
        , B { new uint8_t[static_cast<size_t>(xsize * ysize)] }
        , x_size { xsize }
        , y_size { ysize }
        , color_max { 0 }
{
}


Matrix::Matrix(const Matrix& other)
    : R { new uint8_t[static_cast<size_t>(other.x_size * other.y_size)] }
    , G { new uint8_t[static_cast<size_t>(other.x_size * other.y_size)] }
    , B { new uint8_t[static_cast<size_t>(other.x_size * other.y_size)] }
    , x_size { other.x_size }
    , y_size { other.y_size }
    , color_max { other.color_max }
{
    for (unsigned x { 0 }; x < x_size; x++) {
        for (unsigned y { 0 }; y < y_size; y++) {
            auto &r_val { r(x, y) }, &g_val { g(x, y) }, &b_val { b(x, y) };
            auto other_r_val { other.r(x, y) }, other_g_val { other.g(x, y) }, other_b_val { other.b(x, y) };

            r_val = other_r_val;
            g_val = other_g_val;
            b_val = other_b_val;
        }
    }
}

auto Matrix::operator=(const Matrix other) -> Matrix&
{
    if (this == &other) {
        return *this;
    }

    this->~Matrix();

    auto size = other.x_size * other.y_size;
    R = new uint8_t[size];
    G = new uint8_t[size];
    B = new uint8_t[size];

    x_size = other.x_size;
    y_size = other.y_size;
    color_max = other.color_max;

    for (unsigned x { 0 }; x < x_size; x++) {
        for (unsigned y { 0 }; y < y_size; y++) {
            auto &r_val { r(x, y) }, &g_val { g(x, y) }, &b_val { b(x, y) };
            auto other_r_val { other.r(x, y) }, other_g_val { other.g(x, y) }, other_b_val { other.b(x, y) };

            r_val = other_r_val;
            g_val = other_g_val;
            b_val = other_b_val;
        }
    }

    return *this;
}

Matrix::~Matrix()
{
	// check if monochromatic array was used
    if (R == G && G == B){
        delete[] R;
        R = G = B = nullptr;
    }
    else {
        if (R) {
            delete[] R;
            R = nullptr;
        }
        if (G) {
            delete[] G;
            G = nullptr;
        }
        if (B) {
            delete[] B;
            B = nullptr;
        }
    }

    x_size = y_size = color_max = 0;
}
