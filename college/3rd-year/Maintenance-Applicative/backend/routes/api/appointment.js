const express = require('express')
const router = express.Router()
const auth = require('../../middleware/auth')
const User = require('../../models/User')
const Appointment = require('../../models/Appointment')
const { Status } = require('../../models/Appointment')
const mailer = require('../../misc/mailer')

router.get('/sendReminder', async (req, res) => {
  try {
    // Crée une nouvelle date pour le début de demain
    let startOfTomorrow = new Date()
    startOfTomorrow.setHours(24, 0, 0, 0)

    // Crée une nouvelle date pour la fin de demain
    let endOfTomorrow = new Date()
    endOfTomorrow.setDate(endOfTomorrow.getDate() + 1)
    endOfTomorrow.setHours(23, 59, 59, 999)

    // Trouver les rendez-vous avec ce docteur
    // const appointments = await Appointment.find({ doctor: doctorId })
    try {
      const appointments = await Appointment.find({
        startDate: {
          $gte: startOfTomorrow,
          $lt: endOfTomorrow,
        },
      })
        .populate('patient')
        .populate('doctor')

      if (!appointments.length) {
        return res
          .status(404)
          .json({ err: 'No appointments found for tomorrow' })
      }

      // Boucle sur les rendez-vous pour envoyer des mails
      for (const appointment of appointments) {
        try {
          await mailer.sendMail(
            'labes.assitance@gmail.com',
            appointment.patient.email,
            'Rappel de rendez-vous',
            `<!DOCTYPE html>
              <html lang="fr">
              <head>
                  <meta charset="UTF-8">
                  <meta name="viewport" content="width=device-width, initial-scale=1.0">
                  <title>Rappel de Rendez-vous</title>
                  <style>
                      body {
                          font-family: Arial, sans-serif;
                          background-color: #f9f9f9;
                          color: #333;
                          padding: 20px;
                      }
                      .container {
                          background-color: #fff;
                          padding: 20px;
                          border-radius: 8px;
                          box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                          max-width: 600px;
                          margin: 0 auto;
                      }
                      h1 {
                          color: #007BFF;
                      }
                      p {
                          line-height: 1.6;
                      }
                  </style>
              </head>
              <body>
                  <div class="container">
                      <h1>Bonjour ${appointment.patient.firstName},</h1>
                      <p>
                          Ceci est un rappel pour votre rendez-vous avec le Dr ${appointment.doctor.firstName} ${appointment.doctor.lastName}, demain à ${appointment.startDate.toLocaleTimeString('fr-FR')}.
                      </p>
                      <p>
                          <strong>Motif :</strong> ${appointment.reason}
                      </p>
                      <p>
                          Cordialement,<br>
                          Votre équipe médicale
                          Dr ${appointment.doctor.firstName} ${appointment.doctor.lastName}
                      </p>
                  </div>
              </body>
              </html>`,
          )
        } catch (error) {
          res.status(400).json({ message: error })
        }
      }

      console.error('Emails sent successfully')
      res.status(200).json({ message: 'Emails sent successfully' })
    } catch (err) {
      console.error('Error fetching appointments or sending emails:', err)
      res.status(500).json({ err: 'An error occurred' })
    }
  } catch (error) {
    console.error(error.message)
    res.status(500).send('Server error')
  }
})

// @route    GET /api/appointment/:id
// @desc     Get appointment by id
// @access   Private
router.get('/:id', auth, async (req, res) => {
  const errors = {}
  await Appointment.findOne({ _id: req.params.id })
    .populate({
      path: 'doctor',
      populate: {
        path: 'user',
      },
    })
    .populate({
      path: 'patient',
      populate: {
        path: 'user',
      },
    })
    .then((appointment) => {
      if (!appointment) {
        errors.noappointment = 'There is no appointments for this user'
        return res.status(404).json(errors)
      }
      res.json(appointment)
    })
    .catch((err) => res.status(404).json(err))
})

// @route    GET /api/appointment/getByPatientId/:id
// @desc     Get appointment by patient id
// @access   Private
router.get('/getByPatientId/:id', auth, (req, res) => {
  const errors = {}

  Appointment.find({ patient: req.params.id })
    .populate('patient')
    .populate('doctor')
    .then((appointment) => {
      if (!appointment) {
        errors.noappointment = 'There is no appointments for this user'
        return res.status(404).json(errors)
      }
      res.json(appointment)
    })
    .catch((err) => res.status(404).json(err))
})

// @route    GET /api/appointment/getByDoctorId/:id
// @desc     Get appointment by doctor id
// @access   Private
router.get('/getByDoctorId/:id', auth, (req, res) => {
  const errors = {}

  Appointment.find({ doctor: req.params.id })
    .populate('patient')
    .populate('doctor')
    .then((appointment) => {
      if (!appointment) {
        errors.noappointment = 'There is no appointments for this user'
        return res.status(404).json(errors)
      }
      res.json(appointment)
    })
    .catch((err) => res.status(404).json(err))
})

// @route    POST /api/appointment
// @desc     Add a new appointment
// @access   Private
router.post(
  '/',
  auth,

  async (req, res) => {
    let filepost = null

    const appointmentFields = {}

    if (req.body.doctor) appointmentFields.doctor = req.body.doctor
    if (req.body.patient) appointmentFields.patient = req.body.patient
    if (req.body.startDate) appointmentFields.startDate = req.body.startDate
    if (req.body.endDate) appointmentFields.endDate = req.body.endDate
    if (req.body.id) appointmentFields.idevent = req.body.id
    appointmentFields.FichierJoint = filepost
    if (req.body.typeAppointment)
      appointmentFields.typeAppointment = req.body.typeAppointment
    if (req.body.description)
      appointmentFields.description = req.body.description
    appointmentFields.state = Status.Free
    if (req.body.price) appointmentFields.price = req.body.price
    if (req.body.color) appointmentFields.color = req.body.color
    if (req.body.reason) appointmentFields.reason = req.body.reason

    new Appointment(appointmentFields)
      .save()
      .then((appointment) => res.json(appointment))
  },
)

// @route    PUT /api/appointment/:id
// @desc     Add or modify an appointment
// @access   Private
router.put('/:id', auth, async (req, res) => {
  const appointmentFields = {}

  if (req.body.pro) appointmentFields.doctor = req.body.pro
  if (req.body.patient) appointmentFields.patient = req.body.patient
  if (req.body.startDate) appointmentFields.startDate = req.body.startDate
  if (req.body.endDate) appointmentFields.endDate = req.body.endDate
  if (req.body.typeAppointment)
    appointmentFields.typeAppointment = req.body.typeAppointment
  if (req.body.description) appointmentFields.description = req.body.description
  appointmentFields.status = Status.Free
  appointmentFields.prix = 0

  let appointment = await Appointment.findOne({ _id: req.params.id })
  if (appointment) {
    appointment = await Appointment.findOneAndUpdate(
      {
        _id: req.params.id,
      },
      {
        $set: appointmentFields,
      },
      { new: true },
    )
    return res.json(appointment)
  }
})

// @route   DELETE /api/appointment/:id
// @desc    Delete an appointment by ID
// @access  Private
router.delete('/:id', auth, async (req, res) => {
  try {
    const appointment = await Appointment.deleteOne({ _id: req.params.id })

    if (!appointment) {
      return res.status(404).json({ msg: 'Appointment not found' })
    }

    res.json({ success: true, msg: 'Appointment removed' })
  } catch (err) {
    console.error('Error deleting appointment:', err.message)
    res.status(500).send('Server error')
  }
})

module.exports = router
