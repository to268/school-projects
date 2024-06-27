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
  server.listen(2003)

  await request(server).get('/test/add/prescription').expect(201)

  const userAuthResponse = await request(server)
    .post('/api/auth')
    .send({ email: 'elisegims@example.fr', password: 'EliseGimsTest' })
    .expect(200)

  testUserToken = userAuthResponse.body.token
}

const after = async () => {
  await request(server).get('/test/clean/prescription').expect(204)

  server.close()
  console.log = originalLog
}

describe('Prescription API', () => {
  beforeAll(async () => {
    await before()
  })

  afterAll(async () => {
    after()
  })

  it('Should get all prescriptions', async () => {
    const response = await request(server)
      .get('/api/prescription/all')
      .set('Authorization', `Bearer ${testUserToken}`)
      .expect(200)

    expect(response.body.message).toEqual('Prescription récupéré avec succès')
  })

  it('Should get prescription by user id', async () => {
    const response = await request(server)
      .get('/api/prescription/getByUserId/666945a8a8c8f314b716f83a')
      .set('Authorization', `Bearer ${testUserToken}`)
      .expect(200)

    expect(response.body.message).toEqual('Prescription récupéré avec succès')
  })
})
