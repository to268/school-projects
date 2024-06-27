const Validator = require('validator')
const isEmpty = require('./is-empty')

module.exports = function validateAddressInput(data) {
  let errors = {}

  data.road = !isEmpty(data.road) ? data.road : ''
  data.city = !isEmpty(data.city) ? data.city : ''
  data.postalCode = !isEmpty(data.postalCode) ? data.postalCode : ''
  data.state = !isEmpty(data.state) ? data.state : ''

  if (Validator.isEmpty(data.road)) {
    errors.road = 'road field is required'
  }

  if (Validator.isEmpty(data.city)) {
    errors.city = 'city field is required'
  }

  if (Validator.isEmpty(data.postalCode)) {
    errors.postalCode = 'postalCode field is required'
  }

  if (Validator.isEmpty(data.state)) {
    errors.state = 'state field is required'
  }

  return {
    errors,
    isValid: isEmpty(errors),
  }
}
