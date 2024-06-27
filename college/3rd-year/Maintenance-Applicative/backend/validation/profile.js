const Validator = require('validator')
const isEmpty = require('./is-empty')

module.exports = function validateProfileInput(data) {
  let errors = {}

  data.mobile = !isEmpty(data.mobile) ? data.mobile : ''

  if (Validator.isEmpty(data.mobile)) {
    errors.mobile = 'Mobile field is required'
  }

  return {
    errors,
    isValid: isEmpty(errors),
  }
}
