const validator = require('../../validation/address')

test('Address with valid data should return no errors', () => {
  expect(
    validator({
      road: '42 rue de Groussay',
      city: 'Rodez',
      postalCode: '12000',
      state: 'France',
    }),
  ).toStrictEqual({
    errors: {},
    isValid: true,
  })
})

test('Address with empty road should be invalid', () => {
  expect(
    validator({
      road: '',
      city: 'Rodez',
      postalCode: '12000',
      state: 'France',
    }),
  ).toStrictEqual({
    errors: { road: 'road field is required' },
    isValid: false,
  })
})

test('Address with empty city should be invalid', () => {
  expect(
    validator({
      road: '42 rue de Groussay',
      city: '',
      postalCode: '12000',
      state: 'France',
    }),
  ).toStrictEqual({
    errors: { city: 'city field is required' },
    isValid: false,
  })
})

test('Address with empty postal code should be invalid', () => {
  expect(
    validator({
      road: '42 rue de Groussay',
      city: 'Rodez',
      postalCode: '',
      state: 'France',
    }),
  ).toStrictEqual({
    errors: { postalCode: 'postalCode field is required' },
    isValid: false,
  })
})

test('Address with empty state should be invalid', () => {
  expect(
    validator({
      road: '42 rue de Groussay',
      city: 'Rodez',
      postalCode: '12000',
      state: '',
    }),
  ).toStrictEqual({
    errors: { state: 'state field is required' },
    isValid: false,
  })
})
