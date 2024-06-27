const express = require('express')
const router = express.Router()
const User = require('../../models/User')
const Appointment = require('../../models/Appointment')
const MedicalProfile = require('../../models/MedicalProfile')
const mongoose = require('mongoose')
const auth = require('../../middleware/auth')
const Invoice = require('../../models/Invoice')

// @route   GET /api/users/patients
// @desc    get Patient By Doctor Id
// @access  Private
router.get('/patients', auth, async (req, res) => {
  try {
    const medicalFilesWithPatient = await MedicalProfile.find({
      // patient: { $in: patientIds },
    }).populate('patient')
    // .populate('patient').populate('user')

    if (medicalFilesWithPatient.length > 0) {
      return res.json(medicalFilesWithPatient)
      // return res.json(users);
    } else {
      return res.status(404).json({ err: 'Not found' })
    }
  } catch (error) {
    console.error(error.message)
    res.status(500).send('Server error')
  }
})

// @route   GET /api/users/me
// @desc    Get current users profile
// @access  Private
router.get('/me', auth, (req, res) => {
  const errors = {}
  User.findOne({ _id: req.user.id })
    .populate({
      path: 'invoices',
      model: Invoice,
    })
    .populate({
      path: 'subscribers',
      populate: {
        path: 'user',
      },
    })
    .populate({
      path: 'reviews',
      populate: {
        path: 'user',
        populate: {
          path: 'user',
        },
      },
    })
    .then((profile) => {
      if (!profile) {
        errors.noprofile = 'There is no profile for this user'
        return res.status(404).json(errors)
      }
      res.json(profile)
    })
    .catch((err) => res.status(404).json(err))
})

// @route   GET /api/users/all
// @desc    Get all users
// @access  Private
router.get('/all', auth, async (req, res) => {
  try {
    const users = await User.find()

    res.json(users)
  } catch (error) {
    console.error(error.message)
    res.status(500).send('server error')
  }
})

router.get('/patients/:doctorId', auth, async (req, res) => {
  try {
    const doctorId = req.params.doctorId

    if (!mongoose.Types.ObjectId.isValid(doctorId)) {
      return res.status(400).json({ err: 'Invalid doctor ID format' })
    }

    // Trouver les rendez-vous avec ce docteur
    const appointments = await Appointment.find({
      doctor: doctorId,
    })

    if (!appointments.length) {
      return res
        .status(404)
        .json({ err: 'No appointments found for this doctor' })
    }

    // Extraire les IDs des patients
    const patientIds = appointments.map((appointment) => appointment.patient)

    // Récupérer les dossiers médicaux des patients
    const medicalFilesWithPatient = await MedicalProfile.find({
      patient: { $in: patientIds },
    }).populate('patient')

    const appointmentsByPatient = appointments.reduce((acc, appointment) => {
      const patientId = appointment.patient.toString()
      if (!acc[patientId]) {
        acc[patientId] = []
      }
      acc[patientId].push(appointment)
      return acc
    }, {})

    // Combiner les rendez-vous groupés avec les dossiers médicaux
    const combinedData = Object.entries(appointmentsByPatient)
      .map(([patientId, appointments]) => {
        const medicalFile = medicalFilesWithPatient.find((profile) =>
          profile.patient.equals(patientId),
        )

        return {
          appointments,
          medicalFile, // Contient les informations du dossier médical du patient
        }
      })
      .filter((item) => item.medicalFile)

    res.json(combinedData)
  } catch (error) {
    console.error(error.message)
    res.status(500).send('Server error')
  }
})

// @route   GET /api/users/:_id
// @desc    get User By Id
// @access  Private
router.get('/:id', auth, async (req, res) => {
  try {
    const users = await User.find({ _id: req.params.id })

    if (users) {
      return res.json(users)
    } else {
      return res.json({ err: 'Not found' })
    }
  } catch (error) {
    console.error(error.message)
    res.status(500).send('Server error')
  }
})

// @route   POST /api/users/save
// @desc    post User
// @access  Private
router.post('/save', auth, async (req, res) => {
  try {
    const id = req.body.id
    const userData = { ...req.body.user }
    delete userData._id // Supprimez l'_id pour éviter de le mettre à jour

    // Vérifiez si l'email existe déjà pour un autre utilisateur
    const existingUser = await User.findOne({ email: userData.email })

    // Si un utilisateur existe avec le même email et que ce n'est pas l'utilisateur en cours de mise à jour
    if (existingUser && existingUser._id.toString() !== id) {
      return res.status(400).json({ err: 'Email already in use' })
    }

    // Mettez à jour ou insérez le document utilisateur
    const user = await User.findByIdAndUpdate(
      id,
      { $set: userData },
      { new: true, upsert: true },
    )

    if (user) {
      return res.json(user)
    } else {
      return res.json({ err: 'Not found' })
    }
  } catch (error) {
    console.error(error.message)
    res.status(500).send('Server error')
  }
})

module.exports = router
