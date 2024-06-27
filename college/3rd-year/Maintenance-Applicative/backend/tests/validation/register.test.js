const validator = require('../../validation/register')

test('Registering new client should be valid', () => {
  expect(
    validator({
      firstName: 'Jean',
      lastName: 'Dupond',
      sex: 'Homme',
      birthDate: '1990-03-11',
      email: 'jean@dupond.fr',
      password: 'JeanDup12$10',
      password2: 'JeanDup12$10',
      role: 'Client',
    }),
  ).toStrictEqual({
    errors: {},
    isValid: true,
  })
})

test('Registering new professional should be valid', () => {
  expect(
    validator({
      firstName: 'Valerie',
      lastName: 'Massot',
      sex: 'Femme',
      birthDate: '1998-08-23',
      email: 'valerie@massot.fr',
      password: 'ValMassot38?30',
      password2: 'ValMassot38?30',
      role: 'Professionel',
    }),
  ).toStrictEqual({
    errors: {},
    isValid: true,
  })
})

test('Registering an admin should be valid', () => {
  expect(
    validator({
      firstName: 'Michel',
      lastName: 'Hollande',
      sex: 'Autre',
      birthDate: '2001-06-15',
      email: 'michel@hollande.fr',
      password: 'MichHollande96',
      password2: 'MichHollande96',
      role: 'admin',
    }),
  ).toStrictEqual({
    errors: {},
    isValid: true,
  })
})

test('Registering an invalid client should be invalid', () => {
  expect(
    validator({
      firstName: 'X',
      lastName: 'AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA',
      sex: 'Something',
      birthDate: '2024-01-18',
      email: 'bruh@',
      password: 'MDP',
      password2: 'PPPPPPPAAAAASSSWWWWWOOOORRDDDD',
      role: 'Manager',
    }),
  ).toStrictEqual({
    errors: {
      birthDate: 'The user should have at least 18 years old',
      email: 'Email is invalid',
      firstName: 'firstName must be between 2 and 30 characters',
      lastName: 'lastName must be between 2 and 30 characters',
      password: 'Password must be at least 12 characters',
      password2: 'Passwords must match',
      role: 'Wrong role',
      sex: 'Wrong sex',
    },
    isValid: false,
  })
})
