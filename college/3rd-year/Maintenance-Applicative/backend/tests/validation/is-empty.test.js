const validator = require('../../validation/is-empty')

test('Integer should not be empty', () => {
  expect(validator(12)).toStrictEqual(false)
})

test('String with data should not be empty', () => {
  expect(validator('Test data')).toStrictEqual(false)
})

test('null should be empty', () => {
  expect(validator(null)).toStrictEqual(true)
})

test('undefined should be empty', () => {
  expect(validator(undefined)).toStrictEqual(true)
})

test('Empty object should be empty', () => {
  expect(validator({})).toStrictEqual(true)
})

test('Empty string should be empty', () => {
  expect(validator('')).toStrictEqual(true)
})
