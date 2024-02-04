/*
Author: David Holmqvist <daae19@student.bth.se>
*/

#include "filters_par.hpp"
#include "matrix.hpp"
#include "ppm.hpp"
#include <cmath>
#include <omp.h>

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

    auto blur(Matrix& m, const unsigned int radius, unsigned num_threads) -> Matrix
    {
        //Setting Thread number
        omp_set_num_threads(static_cast<int>(num_threads));
		
		// Retrieving image size constants  and allocating output image
        unsigned const xsize = m.get_x_size();
        unsigned const ysize = m.get_y_size();
		Matrix scratch { xsize, ysize};
		
        // Note: The size of the array should be radius+1 as the center element is not counted. Thus, radius=Gauss:max_radius would be an invalid argument
        auto actual_radius = radius >= Gauss::max_radius? Gauss::max_radius-1 : radius;
        auto maxDimSize = std::max(xsize, ysize);
		
		// Allocating and filling gaussian filter
        auto w = new double[actual_radius+1];
        Gauss::get_weights(radius, w);
		
        // Computes normalization factor should the entire filter be applied
		double weight_sum {w[0]};
        for (auto i {1}; i<=actual_radius; i++){
            weight_sum += w[i];
            weight_sum += w[i];
        }
		
		//shared array of normalization factors
        auto w0 = w[0];
        auto nLine = new double[maxDimSize];
		
		//Single parallel block
        #pragma omp parallel
		{
        // private cache for a line (row/column) of each color
        auto rLine = new unsigned char[maxDimSize];
        auto gLine = new unsigned char[maxDimSize];
        auto bLine = new unsigned char[maxDimSize];

		// parallel computation of the normalization factors (for the row)
        #pragma omp for
            for (int x=0; x < xsize; x++){

                if (x<radius || x+radius >= xsize) {
                    double n {w0};
                    auto x1 {x}, x2 {x};
                    for (auto wi{1}; wi <= radius; wi++) {
                        auto wc{w[wi]};
                        x1--;
                        if (x >= wi) {
                            n += wc;
                        }
                        x2++;
                        if (x2 < xsize) {
                            n += wc;
                        }
                    }
                    nLine[x] = n;
                }
                else nLine[x] = weight_sum;
            }
		// Parallel of the Gaussian Filter in a row-wise manner (split into blocks by a number of rows)
        #pragma omp for 
            for (unsigned y =0; y < ysize; y++) {
				// Caching row values of all colors
                for (unsigned x { 0 }; x < xsize; x++) {
                    rLine[x] = m.r(x, y);
                    gLine[x] = m.g(x, y);
                    bLine[x] = m.b(x, y);
                }
				
				// Computing Gaussian-weighted average of pixel valus in the surrounding area (row)
                for (unsigned x { 0 }; x < xsize; x++) {
                    auto r { w0 * rLine[x] }, g { w0 * gLine[x] }, b { w0 * bLine[x] };

                    if (x<radius || x+radius >= xsize) {
                        auto x1 {x}, x2 {x};
                        for (auto wi{1}; wi <= radius; wi++) {
                            auto wc{w[wi]};
                            x1--;
                            if (x >= wi) {
                                r += wc * rLine[x1];
                                g += wc * gLine[x1];
                                b += wc * bLine[x1];
                            }
                            x2++;
                            if (x2 < xsize) {
                                r += wc * rLine[x2];
                                g += wc * gLine[x2];
                                b += wc * bLine[x2];
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
                    }
					// normalization constant retrieved from cache
                    double const n {nLine[x]};
                    scratch.r(x, y) = static_cast<u_int8_t>(r/n);
                    scratch.g(x, y) = static_cast<u_int8_t>(g/ n);
                    scratch.b(x, y) = static_cast<u_int8_t>(b/ n);
                }
            }
			
		// Finished applying in x-dimension
		// the following is analogous to the previous but applied in y-dimension
		
		// Normalization constants for a column
        #pragma omp for
            for (int y=0; y < ysize; y++){

                if (y < radius || y + radius >= ysize) {
                    double n {w0};
                    auto x1 {y}, x2 {y};
                    for (auto wi{1}; wi <= radius; wi++) {
                        auto wc{w[wi]};
                        x1--;
                        if (y >= wi) {
                            n += wc;
                        }
                        x2++;
                        if (x2 < ysize) {
                            n += wc;
                        }
                    }
                    nLine[y] = n;
                }
                else nLine[y] = weight_sum;
            }
		
		//Applying filter in column-wise manner
        #pragma omp for
            for (unsigned x =0; x < xsize; x++) {
                for (unsigned y { 0 }; y < ysize; y++) {
                    rLine[y] = scratch.r(x, y);
                    gLine[y] = scratch.g(x, y);
                    bLine[y] = scratch.b(x, y);
                }

                for (unsigned y { 0 }; y < ysize; y++) {

                    auto r { w0 * rLine[y] }, g { w0 * gLine[y] }, b { w0 * bLine[y]  };

                    if (y<radius || y+radius >= ysize) {
                        auto y1 {y}, y2 {y};
                        for (auto wi{1}; wi <= radius; wi++) {
                            auto wc{w[wi]};
                            y1--;
                            if (y >= wi) {
                                r += wc * rLine[y1];
                                g += wc * gLine[y1];
                                b += wc * bLine[y1];
                            }
                            y2++;
                            if (y2 < ysize) {
                                r += wc * rLine[y2];
                                g += wc * gLine[y2];
                                b += wc * bLine[y2];
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
                    }
                    double n {nLine[y]};
                    m.r(x, y) = static_cast<u_int8_t>(r / n);
                    m.g(x, y) = static_cast<u_int8_t>(g / n);
                    m.b(x, y) = static_cast<u_int8_t>(b / n);
                }
            }
		// de-allocating private memory
        delete[] rLine;
        delete[] gLine;
        delete[] bLine;
		}
		// de-allocating shared memory
        delete[] w;
        delete[] nLine;
        return m;
    }

    auto threshold(Matrix& m, unsigned num_threads) -> Matrix
    {
        unsigned sum {0}, nump { m.get_size() };
		omp_set_num_threads(static_cast<int>(num_threads));
		// Allocating shared memory for caching intensity and monochromatic output
        uint16_t* sumArr {new uint16_t[nump]};
        uint8_t* monochrome {new uint8_t[nump]};
		
		//One parallel block for the filter with synchronization of sum in the middle
        #pragma omp parallel
        {
		
		// parallelized for-loop computing the sum of intensities and caching the result for each pixel
		// with local copies of sum that are automatically aggregated in the end
        #pragma omp for reduction (+:sum)
            for (unsigned i =0; i < nump; i++) {
                uint16_t const t = m.r(i, 0) + m.g(i, 0) + m.b(i, 0);
                sumArr[i] = t;
                sum += t;
            }
		// Division needs to be executed by a single thread as the sum is shared between threads
        #pragma omp single
            sum /= nump;

		// parallelized for-loop writing comparing the average intensity against the cached values
		// and storing either black or white (if the pixel's value is below or above respectively)
        #pragma omp for
            for (unsigned i =0; i < nump; i++) {
                if (sum > sumArr[i]) {
                    monochrome[i] = 0;
                } else {
                    monochrome[i] = 255;
                }
            }
        }

        delete[] sumArr;
        return Matrix{monochrome, monochrome, monochrome, m.get_x_size(), m.get_y_size(), 255};;
    }

}
