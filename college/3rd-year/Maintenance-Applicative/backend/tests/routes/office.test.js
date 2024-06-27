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
  server.listen(2002)

  await request(server).get('/test/add/office').expect(201)

  const userAuthResponse = await request(server)
    .post('/api/auth')
    .send({ email: 'elisedumont@example.fr', password: 'EliseDumontTest' })
    .expect(200)

  testUserToken = userAuthResponse.body.token
}

const after = async () => {
  await request(server).get('/test/clean/office').expect(204)

  server.close()
  console.log = originalLog
}

describe('Office API', () => {
  beforeAll(async () => {
    await before()
  })

  afterAll(async () => {
    after()
  })

  it('Should get offices', async () => {
    const response = await request(server)
      .get('/api/office/all')
      .set('Authorization', `Bearer ${testUserToken}`)
      .expect(200)

    expect(response.body[0]._id).toEqual('667727b3ec439f14d1ce47af')
    expect(response.body[0].name).toEqual('Cabinet Médical des Champs-Élysées')
  })

  it('Should get office by id', async () => {
    const response = await request(server)
      .get('/api/office/667727b3ec439f14d1ce47af')
      .set('Authorization', `Bearer ${testUserToken}`)
      .expect(200)

    expect(response.body._id).toEqual('667727b3ec439f14d1ce47af')
  })
})
