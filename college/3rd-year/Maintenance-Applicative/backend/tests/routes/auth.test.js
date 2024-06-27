const request = require('supertest')
const app = require('../../server')
const http = require('http')

const originalLog = console.log
let server
let testUserToken

jest.setTimeout(10000)

const before = async () => {
  console.log = jest.fn()
  server = http.createServer(app)
  server.listen(2001)

  await request(server).get('/test/add').expect(201)
}

const after = async () => {
  await request(server).get('/test/clean').expect(204)

  server.close()
  console.log = originalLog
}

describe('Auth API', () => {
  beforeAll(async () => {
    await before()
  })

  afterAll(async () => {
    after()
  })

  it('Should authentificate verified user', async () => {
    const response = await request(server)
      .post('/api/auth')
      .send({ email: 'verifiedupont@example.fr', password: 'JeanDupontTest' })
      .expect(200)

    testUserToken = response.body.token
    expect(response.body.user._id).toEqual('6669b2f4d28a2b67d5fde0c9')
    expect(response.body.user.firstName).toEqual('Verified')
    expect(response.body.user.lastName).toEqual('Dupont')
    expect(response.body.user.email).toEqual('verifiedupont@example.fr')
    expect(response.body.user.birthDate).toEqual('1999-02-06T00:00:00.000Z')
    expect(response.body.user.activated).toEqual(true)
    expect(response.body.user.role).toEqual('user')
    expect(response.body.user.sex).toEqual('Homme')
  })

  it('Should deny unverified user', async () => {
    const response = await request(server)
      .post('/api/auth')
      .send({ email: 'jeandupont@example.fr', password: 'JeanDupontTest' })
      .expect(400)

    expect(response.body).toStrictEqual({
      errors: [{ msg: 'Account disabled' }],
    })
  })

  it('Should return a 400 if password is invalid', async () => {
    const response = await request(server)
      .post('/api/auth')
      .send({ email: 'verifiedupont@example.fr', password: 'JeanDupont1234' })
      .expect(400)

    expect(response.body).toStrictEqual({
      errors: [{ msg: 'Incorrect password' }],
    })
  })

  it('Should register a new user', async () => {
    const response = await request(server)
      .post('/api/auth/register')
      .send({
        firstName: 'New',
        lastName: 'User',
        email: 'newuser@example.fr',
        password: 'NewUserP@ssw0r$',
        birthDate: '2002-12-21',
        role: 'user',
        sex: 'Femme',
      })
      .expect(200)

    expect(response.body).toStrictEqual({
      msg: 'Registration successful. Please check your email to verify your account.',
    })
  })

  it('Should not register a new user', async () => {
    const response = await request(server)
      .post('/api/auth/register')
      .send({
        firstName: 'N',
        lastName: 'UUUUUUUSSSSSSSSSEEEEEEEEUUUUUUUUUURRRRRRRR',
        email: 'newuser@',
        password: 'newuser',
        birthDate: '2024-04-12',
        role: 'server',
        sex: 'Test',
      })
      .expect(400)

    expect(response.body).toStrictEqual({
      errors: [
        {
          location: 'body',
          msg: 'Please include a valid email',
          path: 'email',
          type: 'field',
          value: 'newuser@',
        },
        {
          location: 'body',
          msg: 'Please enter a password with 12 or more characters',
          path: 'password',
          type: 'field',
          value: 'newuser',
        },
        {
          location: 'body',
          msg: 'Birth date is required and should be a valid date',
          path: 'birthDate',
          type: 'field',
          value: '2024-04-12',
        },
      ],
    })
  })

  it('Should verify a new user', async () => {
    const response = await request(server)
      .get('/api/auth/verify/d4d2c3bdaf4b50ed30171847388ba85ac0f79df7')
      .expect(200)

    expect(response.body).toStrictEqual({
      msg: 'Account verified successfully. You can now log in.',
    })
  })

  it('Should not verify a new user', async () => {
    const response = await request(server)
      .get('/api/auth/verify/98e81012fd1276323af428b3fecc1117ccb0dd50')
      .expect(400)

    expect(response.body).toStrictEqual({
      errors: [
        {
          msg: 'Invalid or expired verification token',
        },
      ],
    })
  })

  it('Should change the password of the user', async () => {
    const response = await request(server)
      .put('/api/auth/updatePassword')
      .set('Authorization', `Bearer ${testUserToken}`)
      .send({ oldPass: 'JeanDupontTest', newPass: 'JeanDupontTestPass' })
      .expect(200)

    expect(response.body).toStrictEqual({
      updated: true,
      Message: 'Password updated succesfully',
    })
  })

  it('Should not change the password when using wrong oldPass', async () => {
    const response = await request(server)
      .put('/api/auth/updatePassword')
      .set('Authorization', `Bearer ${testUserToken}`)
      .send({ oldPass: 'JeanDupontTest', newPass: 'JeanDupontTest' })
      .expect(400)

    expect(response.body).toStrictEqual({
      errors: [
        {
          msg: 'Incorrect password',
        },
      ],
    })
  })

  it('Should not change the password when using non valid new password', async () => {
    const response = await request(server)
      .put('/api/auth/updatePassword')
      .set('Authorization', `Bearer ${testUserToken}`)
      .send({ oldPass: 'JeanDupontTestPass', newPass: 'Pass' })
      .expect(400)

    expect(response.body).toStrictEqual({
      errors: [
        {
          msg: 'Password should contains at least 12 characters',
        },
      ],
    })
  })

  it('Should not change the password without a bearer token', async () => {
    const response = await request(server)
      .put('/api/auth/updatePassword')
      .send({ oldPass: 'JeanDupontTestPass', newPass: 'Pass' })
      .expect(401)

    expect(response.body).toStrictEqual({
      msg: 'No token, authorization denied',
    })
  })

  it('Should reset the password of the user', async () => {
    const response = await request(server)
      .put('/api/auth/resetPassword')
      .send({ email: 'verifiedupont@example.fr' })
      .expect(200)

    expect(response.body).toStrictEqual({
      updated: true,
      Message: 'Email sent',
    })
  })

  it('Should reset the password of the user', async () => {
    const response = await request(server)
      .put('/api/auth/resetPassword')
      .send({ email: 'invalid@example.fr' })
      .expect(404)

    expect(response.body).toStrictEqual({
      errors: [
        {
          msg: 'User not found',
        },
      ],
    })
  })

  it('Should logout the user', async () => {
    const response = await request(server)
      .post('/api/auth/logout')
      .set('Authorization', `Bearer ${testUserToken}`)
      .expect(200)

    expect(response.body).toStrictEqual({
      updated: true,
      Message: 'Logout successful',
    })
  })

  it('Should not logout the user with invalid', async () => {
    const response = await request(server)
      .post('/api/auth/logout')
      .set('Authorization', `Bearer 3ae76`)
      .expect(401)

    expect(response.body).toStrictEqual({
      msg: 'Token is not valid',
    })
  })
})
