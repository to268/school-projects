/*
Author: David Holmqvist <daae19@student.bth.se>
*/

#include "ppm.hpp"
#include <fstream>
#include <iostream>
#include <stdexcept>
#include <string_view>
#include <tuple>

namespace PPM {

    void Reader::fill(const std::string& filename)
    {
        std::ifstream f {};

        f.open(filename);

        if (!f) {
            stream.setstate(std::ios::failbit);
            return;
        }

        stream << f.rdbuf();
        f.close();
    }

    auto Reader::get_magic_number() -> std::string
    {
        std::string magic {};

        std::getline(stream, magic);

        return magic;
    }

    auto Reader::get_dimensions() -> std::pair<unsigned, unsigned>
    {
        std::string line {};

        while (std::getline(stream, line) && line[0] == '#')
            ;

        size_t pos = line.find(' ');
        std::string x = line.substr(0, pos);
        std::string y = line.substr(pos + 1);

        return { std::stoul(x), std::stoul(y) };
    }

    auto Reader::get_color_max() -> unsigned
    {
        std::string line {};

        std::getline(stream, line);

        return std::stoul(line);
    }

    auto Reader::get_data(unsigned x_size, unsigned y_size) -> std::tuple<unsigned char*, unsigned char*, unsigned char*>
    {
        auto size = Matrix::get_size(x_size, y_size);
        auto R { new char[size] }, G { new char[size] }, B { new char[size] };
        auto prev_pos = stream.tellg();
        stream.seekg(0, std::ios::end);
        unsigned const streamsize = stream.tellg()-prev_pos;
        // std::cout << streamsize << "\n";
        if (streamsize%3)  return { nullptr, nullptr, nullptr };

        auto interleaved_arr = new char[streamsize];
        stream.seekg(prev_pos);
        stream.read(interleaved_arr, streamsize);

        for (int i {0},j {0}; i<streamsize; i+=3,j++){
            R[j] = interleaved_arr[i];
            G[j] = interleaved_arr[i+1];
            B[j] = interleaved_arr[i+2];
        }
        /*for (long i { 0 }, read { 0 }; i < size; i++, read = 0) {
            R[i] = stream.get();
            read += stream.gcount();
            G[i] = stream.get();
            read += stream.gcount();
            B[i] = stream.get();
            read += stream.gcount();

            if (read != 3) {
                delete[] R;
                delete[] G;
                delete[] B;
                return { nullptr, nullptr, nullptr };
            }
        }*/
        delete[] interleaved_arr;
        return { reinterpret_cast<unsigned char*>(R), reinterpret_cast<unsigned char*>(G), reinterpret_cast<unsigned char*>(B) };
    }

    auto Reader::operator()(const std::string& filename) -> Matrix
    {
        try {
            fill(filename);

            if (stream.fail()) {
                throw std::runtime_error { "couldn't open file " + filename };
            }

            auto magic { get_magic_number() };

            if (magic != magic_number) {
                throw std::runtime_error { "incorrect magic number: " + magic };
            }

            auto [x_size, y_size] { get_dimensions() };

            if (x_size == 0 || y_size == 0) {
                throw std::runtime_error { "couldn't read dimensions" };
            }

            auto total_size = Matrix::get_size(x_size, y_size);

            if (total_size > max_pixels) {
                throw std::runtime_error { "image size is too big: " + std::to_string(total_size) };
            }

            auto color_max { get_color_max() };

            if (color_max == 0) {
                throw std::runtime_error { "couldn't read color max" };
            }

            auto [R, G, B] { get_data(x_size, y_size) };

            if (!R || !G || !B) {
                throw std::runtime_error { "couldn't read image data" };
            }

            stream.clear();
            return Matrix { R, G, B, x_size, y_size, color_max };
        } catch (const std::runtime_error& e) {
            error("reading", e.what());
            stream.clear();
            return Matrix {};
        }
    }

    void error(std::string_view op, std::string_view what)
    {
        std::cerr << "Encountered PPM error during "
                  << op << ": " << what << '\n';
    }

    void Writer::operator()(const Matrix& m, const std::string& filename)
    {
        try {
            std::ofstream f {};

            f.open(filename);

            if (!f) {
                throw std::runtime_error { "failed to open " + filename };
            }

            f << magic_number << '\n';

            f << m.get_x_size() << " " << m.get_y_size() << '\n';
            f << m.get_color_max() << '\n';

            auto size = m.get_size();
            // auto R { m.get_R() }, G { m.get_G()}, B { m.get_B()};
            auto R { reinterpret_cast<const char*>(m.get_R()) }, G { reinterpret_cast<const char*>(m.get_G())}, B { reinterpret_cast<const char*>(m.get_B())};
            auto interleaved_arr = new char[size+size+size];

            auto end {3*size};
            for (unsigned i {0}; i < end; i+=3){
                interleaved_arr[i] = *R++;  // remember *R++
                interleaved_arr[i+1] = *G++;
                interleaved_arr[i+2] = *B++;
            }

            f.write(interleaved_arr, size+size+size);
            delete[] interleaved_arr;
            f.close();
        } catch (std::runtime_error& e) {
            error("writing", e.what());
        }
    }

}
