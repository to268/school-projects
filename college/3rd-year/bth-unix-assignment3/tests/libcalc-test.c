#include "utest.h"

extern double fact(double x);
extern double gcd(double a, double b);
extern double lntwo(double x);

UTEST_MAIN();

struct libcalc {};

UTEST_F_SETUP(libcalc) {}

UTEST_F_TEARDOWN(libcalc) {}

UTEST_F(libcalc, fact_test) {
    ASSERT_EQ(fact(0), 1);
    ASSERT_EQ(fact(1), 1);
    ASSERT_EQ(fact(2), 2);
    ASSERT_EQ(fact(3), 6);
    ASSERT_EQ(fact(4), 24);
    ASSERT_EQ(fact(5), 120);
    ASSERT_EQ(fact(6), 720);
    ASSERT_EQ(fact(7), 5040);
    ASSERT_EQ(fact(8), 40320);
    ASSERT_EQ(fact(9), 362880);
    ASSERT_EQ(fact(10), 3628800);
}

UTEST_F(libcalc, lntwo_test) {
    // Test for log2(0), which is undefined
    ASSERT_EQ(lntwo(0), 0.0);

    ASSERT_EQ(lntwo(1), 0.0);
    ASSERT_EQ(lntwo(2), 1.0);
    ASSERT_EQ(lntwo(4), 2.0);
    ASSERT_EQ(lntwo(8), 3.0);
    ASSERT_EQ(lntwo(16), 4.0);
}

UTEST_F(libcalc, gcd_test) {
    // Zeroes edge cases
    ASSERT_EQ(gcd(0, 0), 0);
    ASSERT_EQ(gcd(0, 1), 1);
    ASSERT_EQ(gcd(1, 0), 1);

    ASSERT_EQ(gcd(12, 18), 6);
    ASSERT_EQ(gcd(35, 42), 7);
    ASSERT_EQ(gcd(99, 121), 11);
    ASSERT_EQ(gcd(17, 23), 1);
}
