const Validator = require('validator')
const isEmpty = require('./is-empty')

const hasRequiredAge = (date) => {
  const today = new Date()
  const birthDate = new Date(date)
  const ageLimit = 150
  const minimumAge = 18

  const age = today.getFullYear() - birthDate.getFullYear()
  const isPastDate = birthDate <= today
  const isWithinAgeLimit = age <= ageLimit && age >= minimumAge

  return isPastDate && isWithinAgeLimit
}

module.exports = function validateRegisterInput(data) {
  let errors = {}

  data.firstName = !isEmpty(data.firstName) ? data.firstName : ''
  data.lastName = !isEmpty(data.lastName) ? data.lastName : ''
  data.sex = !isEmpty(data.sex) ? data.sex : ''
  data.birthDate = !isEmpty(data.birthDate) ? data.birthDate : ''

  data.email = !isEmpty(data.email) ? data.email : ''
  data.password = !isEmpty(data.password) ? data.password : ''
  data.password2 = !isEmpty(data.password2) ? data.password2 : ''
  data.role = !isEmpty(data.role) ? data.role : ''

  if (!Validator.isLength(data.firstName, { min: 2, max: 30 })) {
    errors.firstName = 'firstName must be between 2 and 30 characters'
  }

  if (Validator.isEmpty(data.firstName)) {
    errors.firstName = 'Name field is required'
  }
  if (!Validator.isLength(data.lastName, { min: 2, max: 30 })) {
    errors.lastName = 'lastName must be between 2 and 30 characters'
  }

  if (Validator.isEmpty(data.lastName)) {
    errors.lastName = 'Name field is required'
  }

  if (Validator.isEmpty(data.email)) {
    errors.email = 'Email field is required'
  }

  if (!Validator.isEmail(data.email)) {
    errors.email = 'Email is invalid'
  }

  if (Validator.isEmpty(data.password)) {
    errors.password = 'Password field is required'
  }

  if (!Validator.isLength(data.password, { min: 12, max: 30 })) {
    errors.password = 'Password must be at least 12 characters'
  }

  if (Validator.isEmpty(data.password2)) {
    errors.password2 = 'Confirm Password field is required'
  }

  if (!Validator.equals(data.password, data.password2)) {
    errors.password2 = 'Passwords must match'
  }

  if (
    !Validator.equals(data.role, 'Professionel') &&
    !Validator.equals(data.role, 'Client') &&
    !Validator.equals(data.role, 'admin')
  ) {
    errors.role = 'Wrong role'
  }

  if (
    !Validator.equals(data.sex, 'Homme') &&
    !Validator.equals(data.sex, 'Femme') &&
    !Validator.equals(data.sex, 'Autre')
  ) {
    errors.sex = 'Wrong sex'
  }

  if (!hasRequiredAge(data.birthDate)) {
    errors.birthDate = 'The user should have at least 18 years old'
  }

  return {
    errors,
    isValid: isEmpty(errors),
  }
}
