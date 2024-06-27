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
  server.listen(2004)

  await request(server).get('/test/add').expect(201)

  const userAuthResponse = await request(server)
    .post('/api/auth')
    .send({ email: 'verifiedupont@example.fr', password: 'JeanDupontTest' })
    .expect(200)

  testUserToken = userAuthResponse.body.token
}

const after = async () => {
  await request(server).get('/test/clean').expect(204)

  server.close()
  console.log = originalLog
}

describe('Users API', () => {
  beforeAll(async () => {
    await before()
  })

  afterAll(async () => {
    after()
  })

  it('Should get all users list', async () => {
    const response = await request(server)
      .get('/api/users/all')
      .set('Authorization', `Bearer ${testUserToken}`)
      .expect(200)

    expect(response.body[0]._id).toEqual('6669b2f4d28a2b67d5fde0c9')
    expect(response.body[1]._id).toEqual('666945a808886324b7168e72')
  })
})
