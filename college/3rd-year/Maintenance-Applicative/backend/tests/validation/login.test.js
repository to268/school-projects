const validator = require('../../validation/login')

test('Correctly formatted email and password should be valid', () => {
  expect(
    validator({ email: 'test@example.com', password: 'password' }),
  ).toStrictEqual({ errors: {}, isValid: true })
})

test('Badly formatted email should be invalid', () => {
  expect(validator({ email: 'test@', password: 'password' })).toStrictEqual({
    errors: { email: 'Email is invalid' },
    isValid: false,
  })
})

test('Missing email should be invalid', () => {
  expect(validator({ email: '', password: 'password' })).toStrictEqual({
    errors: { email: 'Email field is required' },
    isValid: false,
  })
})

test('Missing password should be invalid', () => {
  expect(validator({ email: 'test@example.com', password: '' })).toStrictEqual({
    errors: { password: 'Password field is required' },
    isValid: false,
  })
})
