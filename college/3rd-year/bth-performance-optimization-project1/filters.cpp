/*
Author: David Holmqvist <daae19@student.bth.se>
*/

#include "filters.hpp"
#include "matrix.hpp"
#include "ppm.hpp"
#include <cmath>

namespace Filter {

    namespace Gauss {
        void get_weights(unsigned int n, double* weights_out)
        {
            for (auto i { 0 }; i <= n; i++) {
                double x { static_cast<double>(i) * max_x / n };
                weights_out[i] = exp(-x * x * pi);
            }
        }
    }

    auto blur(const Matrix& m, const unsigned int radius) -> Matrix
    {
        Matrix scratch { PPM::max_dimension };
        auto dst { m };

        double w[Gauss::max_radius] {};
        Gauss::get_weights(radius, w);
        unsigned const xsize = dst.get_x_size();
        unsigned const ysize = dst.get_y_size();
        auto actual_radius = radius > Gauss::max_radius? Gauss::max_radius : radius;
        double weight_sum {w[0]};
        for (auto i {1}; i<=actual_radius; i++){
            weight_sum += w[i];
            weight_sum += w[i];
        }
        //printf("Sum of Gaussian Weights (Before): %.20f\n", weight_sum);
        //weight_sum += 1e-15;
        //double const weight_sum2 = weight_sum-1e-15;
        //printf("Sum of Gaussian Weights (After): %.20f\n", weight_sum);
        auto w0 = w[0];


        unsigned char rLine[PPM::max_dimension] {};
        unsigned char gLine[PPM::max_dimension] {};
        unsigned char bLine[PPM::max_dimension] {};

        for (unsigned y { 0 }; y < ysize; y++) {
            for (unsigned x { 0 }; x < xsize; x++) {
                rLine[x] = dst.r(x, y);
                gLine[x] = dst.g(x, y);
                bLine[x] = dst.b(x, y);
            }

            for (unsigned x { 0 }; x < xsize; x++) {
                auto r { w0 * rLine[x] }, g { w0 * gLine[x] }, b { w0 * bLine[x] }, n { w0 };

                if (x<radius || x+radius >= xsize) {
                    auto x1 {x}, x2 {x};
                    for (auto wi{1}; wi <= radius; wi++) {
                        auto wc{w[wi]};
                        x1--;
                        if (x >= wi) {
                            r += wc * rLine[x1];
                            g += wc * gLine[x1];
                            b += wc * bLine[x1];
                            n += wc;
                        }
                        x2++;
                        if (x2 < xsize) {
                            r += wc * rLine[x2];
                            g += wc * gLine[x2];
                            b += wc * bLine[x2];
                            n += wc;
                        }
                    }
                }
                else {
                    auto x1 {x}, x2 {x};
                    for (auto wi{1}; wi <= radius; wi++) {
                        auto wc{w[wi]};
                        x1--;
                        r += wc * rLine[x1];
                        g += wc * gLine[x1];
                        b += wc * bLine[x1];
                        x2++;
                        r += wc * rLine[x2];
                        g += wc * gLine[x2];
                        b += wc * bLine[x2];
                    }
                    n = weight_sum;
                }

                // this cannot be correct (to cast to char in between the two steps)
                scratch.r(x, y) = static_cast<u_int8_t>(r / n);
                scratch.g(x, y) = static_cast<u_int8_t>(g/ n);
                scratch.b(x, y) = static_cast<u_int8_t>(b/ n);
            }
        }

        for (unsigned x { 0 }; x < xsize; x++) {
            for (unsigned y { 0 }; y < ysize; y++) {
                rLine[y] = scratch.r(x, y);
                gLine[y] = scratch.g(x, y);
                bLine[y] = scratch.b(x, y);
            }

            for (unsigned y { 0 }; y < ysize; y++) {

                auto r { w0 * rLine[y] }, g { w0 * gLine[y] }, b { w0 * bLine[y]  }, n { w0 };

                if (y<radius || y+radius >= ysize) {
                    auto y1 {y}, y2 {y};
                    for (auto wi{1}; wi <= radius; wi++) {
                        auto wc{w[wi]};
                        y1--;
                        if (y >= wi) {
                            r += wc * rLine[y1];
                            g += wc * gLine[y1];
                            b += wc * bLine[y1];
                            n += wc;
                        }
                        y2++;
                        if (y2 < ysize) {
                            r += wc * rLine[y2];
                            g += wc * gLine[y2];
                            b += wc * bLine[y2];
                            n += wc;
                        }
                    }
                }
                else {
                    auto y1 {y}, y2 {y};
                    for (auto wi{1}; wi <= radius; wi++) {
                        auto wc{w[wi]};
                        y1--;
                        r += wc * rLine[y1];
                        g += wc * gLine[y1];
                        b += wc * bLine[y1];
                        y2++;
                        r += wc * rLine[y2];
                        g += wc * gLine[y2];
                        b += wc * bLine[y2];
                    }
                    n = weight_sum;
                }
                dst.r(x, y) = static_cast<u_int8_t>(r / n);
                dst.g(x, y) = static_cast<u_int8_t>(g / n);
                dst.b(x, y) = static_cast<u_int8_t>(b / n);
            }
        }

        return dst;
    }

auto threshold(const Matrix& m) -> Matrix
{
    unsigned sum {0}, nump { m.get_size() };

    uint16_t* sumArr {new uint16_t[nump]};

    for (unsigned i { 0 }; i < nump; i++) {
        uint16_t const t = m.r(i, 0) + m.g(i, 0) + m.b(i, 0);
        sumArr[i] = t;
        sum += t;
    }
    sum /= nump;
    uint8_t* monochrome {new uint8_t[nump]};

    for (unsigned i { 0 }; i < nump; i++) {
        if (sum > sumArr[i]) {
            monochrome[i] = 0;
        } else {
            monochrome[i] = 255;
        }
    }
    delete[] sumArr;
    return Matrix{monochrome, monochrome, monochrome, m.get_x_size(), m.get_y_size(), 255};;
}

}
