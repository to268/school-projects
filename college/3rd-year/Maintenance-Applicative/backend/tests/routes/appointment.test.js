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
  server.listen(2000)

  await request(server).get('/test/add/appointment').expect(201)

  const userAuthResponse = await request(server)
    .post('/api/auth')
    .send({ email: 'bernardmarchant@example.fr', password: 'BernardMarchant' })
    .expect(200)

  testUserToken = userAuthResponse.body.token
}

const after = async () => {
  await request(server).get('/test/clean/appointment').expect(204)

  server.close()
  console.log = originalLog
}

describe('Appointment API', () => {
  beforeAll(async () => {
    await before()
  })

  afterAll(async () => {
    after()
  })

  it('Should filter appointments by patient', async () => {
    const response = await request(server)
      .get('/api/appointment/getByPatientId/666945a808886324b7168e76')
      .set('Authorization', `Bearer ${testUserToken}`)
      .expect(200)

    expect(response.body[0]._id).toEqual('6669456508886324b7168e22')
    expect(response.body[0].doctor._id).toEqual('667131f6aaa89d3618442fbd')
    expect(response.body[0].patient._id).toEqual('666945a808886324b7168e76')
    expect(response.body[0].state).toEqual('confirmed')
    expect(response.body[0].description).toEqual('Examen de routine')
    expect(response.body[0].price).toEqual(35)
  })

  it('Should filter appointments by doctor', async () => {
    const response = await request(server)
      .get('/api/appointment/getByDoctorId/667131f6aaa89d3618442fbd')
      .set('Authorization', `Bearer ${testUserToken}`)
      .expect(200)

    expect(response.body[0]._id).toEqual('6669456508886324b7168e22')
    expect(response.body[0].doctor._id).toEqual('667131f6aaa89d3618442fbd')
    expect(response.body[0].patient._id).toEqual('666945a808886324b7168e76')
    expect(response.body[0].state).toEqual('confirmed')
    expect(response.body[0].description).toEqual('Examen de routine')
    expect(response.body[0].price).toEqual(35)
  })

  it('Should add a new appointment', async () => {
    const appointment = {
      doctor: '667131f6aaa89d3618442fbd',
      patient: '666945a808886324b7168e76',
      startDate: '2024-08-23T16:30:00.000Z',
      endDate: '2024-08-20T02:17:00.000Z',
      reason: 'Consultation médicale',
      description: 'Examen de la thyroïde',
      price: 40,
    }

    await request(server)
      .post('/api/appointment')
      .set('Authorization', `Bearer ${testUserToken}`)
      .send(appointment)
      .expect(200)
  })

  it('Should modify an appointment', async () => {
    const appointment = {
      doctor: '667131f6aaa89d3618442fbd',
      patient: '666945a808886324b7168e76',
      startDate: '2024-06-23T17:30:00.000Z',
      endDate: '2024-06-20T03:17:00.000Z',
      state: 'confirmed',
      reason: 'Consultation médicale',
      description: 'Examen de routine',
      price: 45,
    }

    await request(server)
      .put('/api/appointment/6669456508886324b7168e22')
      .set('Authorization', `Bearer ${testUserToken}`)
      .send(appointment)
      .expect(200)
  })

  it('Should delete an appointment', async () => {
    await request(server)
      .delete('/api/appointment/6669456508886324b7168e22')
      .set('Authorization', `Bearer ${testUserToken}`)
      .expect(200)
  })
})
