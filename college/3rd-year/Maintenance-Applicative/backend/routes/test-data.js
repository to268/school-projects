const express = require('express')
const router = express.Router()
const bcrypt = require('bcryptjs')
const User = require('../models/User')
const Appointment = require('../models/Appointment')
const Office = require('../models/Office')
const Prescription = require('../models/Prescription')
const ObjectId = require('mongoose').Types.ObjectId

router.get('/add', async (req, res) => {
  let user = new User({
    _id: new ObjectId('6669b2f4d28a2b67d5fde0c9'),
    firstName: 'Verified',
    lastName: 'Dupont',
    email: 'verifiedupont@example.fr',
    birthDate: '1999-02-06',
    role: 'user',
    sex: 'Homme',
    activated: true,
  })
  user.password = await bcrypt.hash('JeanDupontTest', await bcrypt.genSalt(10))
  await user.save()

  user = new User({
    _id: new ObjectId('666945a808886324b7168e72'),
    firstName: 'Jean',
    lastName: 'Dupont',
    email: 'jeandupont@example.fr',
    birthDate: '1999-02-06',
    role: 'user',
    sex: 'Homme',
    activated: false,
  })
  user.password = await bcrypt.hash('JeanDupontTest', await bcrypt.genSalt(10))
  await user.save()

  res.status(201).send(user)
})

router.get('/add/appointment', async (req, res) => {
  const doctor = new User({
    _id: new ObjectId('667131f6aaa89d3618442fbd'),
    firstName: 'Bernard',
    lastName: 'Marchant',
    email: 'bernardmarchant@example.fr',
    birthDate: '1995-08-13',
    role: 'doctor',
    sex: 'Homme',
    activated: true,
  })
  doctor.password = await bcrypt.hash(
    'BernardMarchant',
    await bcrypt.genSalt(10),
  )
  await doctor.save()

  const patient = new User({
    _id: new ObjectId('666945a808886324b7168e76'),
    firstName: 'Emma',
    lastName: 'Lefebvre',
    email: 'emmalefebrve@example.fr',
    birthDate: '2000-05-19',
    role: 'user',
    sex: 'Femme',
    activated: false,
  })
  patient.password = await bcrypt.hash('EmmaLefebvre', await bcrypt.genSalt(10))
  await patient.save()

  const appointment = new Appointment({
    _id: new ObjectId('6669456508886324b7168e22'),
    doctor: new ObjectId('667131f6aaa89d3618442fbd'),
    patient: new ObjectId('666945a808886324b7168e76'),
    startDate: '2024-06-23T16:30:00.000Z',
    endDate: '2024-06-20T02:17:00.000Z',
    state: 'confirmed',
    reason: 'Consultation médicale',
    description: 'Examen de routine',
    price: 35,
  })
  await appointment.save()

  res.status(201).send(appointment)
})

router.get('/add/office', async (req, res) => {
  const user = new User({
    _id: new ObjectId('6669b2d4a45a2b69d6fce0d9'),
    firstName: 'Élise',
    lastName: 'Dumont',
    email: 'elisedumont@example.fr',
    birthDate: '1992-02-13',
    role: 'doctor',
    sex: 'Femme',
    activated: true,
  })
  user.password = await bcrypt.hash('EliseDumontTest', await bcrypt.genSalt(10))
  await user.save()

  const office = new Office({
    _id: new ObjectId('667727b3ec439f14d1ce47af'),
    name: 'Cabinet Médical des Champs-Élysées',
    email: 'contact@cabinetchampselysees.fr',
    address: {
      road: '50 Avenue des Champs-Élysées',
      city: 'Paris',
      postalCode: '75008',
      state: 'Île-de-France',
    },
    landline: ['+33142567890'],
  })
  await office.save()

  res.status(201).send(office)
})

router.get('/add/prescription', async (req, res) => {
  let user = new User({
    _id: new ObjectId('667727b3ec438f15f1ae23ab'),
    firstName: 'Élise',
    lastName: 'Gims',
    email: 'elisegims@example.fr',
    birthDate: '1993-07-03',
    role: 'doctor',
    sex: 'Femme',
    activated: true,
  })
  user.password = await bcrypt.hash('EliseGimsTest', await bcrypt.genSalt(10))
  await user.save()

  user = new User({
    _id: new ObjectId('666945a8a8c8f314b716f83a'),
    firstName: 'Jean',
    lastName: 'Marchant',
    email: 'jeanmarchant@example.fr',
    birthDate: '1999-10-12',
    role: 'user',
    sex: 'Homme',
    activated: false,
  })
  user.password = await bcrypt.hash(
    'JeanMarchantTest',
    await bcrypt.genSalt(10),
  )
  await user.save()

  const prescription = new Prescription({
    doctor: new ObjectId('667727b3ec438f15f1ae23ab'),
    patient: new ObjectId('666945a8a8c8f314b716f83a'),
    licenseNumber: '0654654181654165154',
    phoneNumber: '0769696969',
    signatureImage: 'image',
  })
  await prescription.save()

  res.status(201).send(prescription)
})

router.get('/clean', async (req, res) => {
  await User.deleteMany({})
  res.status(204)
})

router.get('/clean/appointment', async (req, res) => {
  await User.deleteMany({})
  await Appointment.deleteMany({})
  res.status(204)
})

router.get('/clean/office', async (req, res) => {
  await User.deleteMany({})
  await Office.deleteMany({})
  res.status(204)
})

router.get('/clean/prescription', async (req, res) => {
  await User.deleteMany({})
  await Prescription.deleteMany({})
  res.status(204)
})

module.exports = router
