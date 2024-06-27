const validator = require('../../validation/profile')

test('Mobile phone number should be valid', () => {
  expect(validator({ mobile: '0769696969' })).toStrictEqual({
    errors: {},
    isValid: true,
  })
})

test('Missing mobile phone number should be invalid', () => {
  expect(validator({ mobile: '' })).toStrictEqual({
    errors: { mobile: 'Mobile field is required' },
    isValid: false,
  })
})
