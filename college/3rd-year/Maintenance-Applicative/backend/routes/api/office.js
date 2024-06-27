const express = require('express')
const router = express.Router()
const Office = require('../../models/Office')
const User = require('../../models/User')
const mongoose = require('mongoose')
const auth = require('../../middleware/auth')

// @route   GET /api/office/all
// @desc    Get all offices
// @access  Private
router.get('/all', auth, async (req, res) => {
  try {
    const offices = await Office.find()

    res.json(offices)
  } catch (error) {
    console.error(error.message)
    res.status(500).send('server error')
  }
})

// @route   GET /api/office/:_id
// @desc    get Office By Id
// @access  Private
router.get('/getDoctorByOffice/:id', auth, async (req, res) => {
  const officeId = req.params.id
  if (!mongoose.Types.ObjectId.isValid(officeId)) {
    return res.status(400).json({ err: 'Invalid office ID format' })
  }
  const office = await Office.findOne({ _id: officeId })

  try {
    const doctors = await User.find({ office: officeId })
    if (!doctors || doctors.length === 0) {
      return res.status(404).json({ err: 'No doctors found for this office' })
    }

    // Extraire les IDs des docteurs
    const doctorIds = doctors.map((doctor) => doctor._id)

    // Récupérer tous les rendez-vous de ces docteurs
    const appointments = await Appointment.find({ doctor: { $in: doctorIds } })

    // Combiner les rendez-vous avec les dossiers médicaux des patients
    const combinedData = appointments.map((appointment) => {
      // Trouver le dossier médical correspondant à ce rendez-vous
      const doctor = doctors.find((doctor) =>
        doctor._id.equals(appointment.doctor),
      )

      // Retourner une combinaison des rendez-vous et des dossiers médicaux
      return {
        appointment,
        doctor,
      }
    })

    res.json({ office: office, appointments: combinedData })
  } catch (error) {
    console.error(error.message)
    res.status(500).send('Server error')
  }
})

// @route   GET /api/office/:_id
// @desc    get Office By Id
// @access  Private
router.get('/:id', auth, async (req, res) => {
  try {
    const offices = await Office.findOne({ _id: req.params.id })

    if (offices) {
      return res.json(offices)
    } else {
      return res.json({ err: 'Not found' })
    }
  } catch (error) {
    console.error(error.message)
    res.status(500).send('Server error')
  }
})

module.exports = router
