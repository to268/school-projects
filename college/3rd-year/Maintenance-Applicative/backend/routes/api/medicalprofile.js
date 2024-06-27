const express = require('express')
const router = express.Router()
const MedicalProfile = require('../../models/MedicalProfile')
const mailer = require('../../misc/mailer')
const User = require('../../models/User')
const auth = require('../../middleware/auth')
const ObjectId = require('mongoose').Types.ObjectId

// @route   GET /api/medicalprofile/sendReminder
// @desc    send Reminder
// @access  Private
router.get('/sendReminder', async (req, res) => {
  try {
    const medicalFilesWithPatients = await MedicalProfile.find({}).populate({
      path: 'patient',
    })

    medicalFilesWithPatients.forEach(async (patient) => {
      let mois = []
      switch (patient.health) {
        case 1: //1mois
          mois = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
          break
        case 2: //3mois
          mois = [1, 4, 7, 10]
          break
        case 3: //6mois
          mois = [1, 7]
          break
        case 4: //7mois
          mois = [1, 8]
          break
        case 5: //12mois
          mois = [1]
          break
      }
      const d = new Date()
      let currentMonth = d.getMonth() + 1

      if (mois.indexOf(currentMonth) != -1) {
        try {
          await mailer.sendMail(
            'labes.assitance@gmail.com',
            // Patient.user.email,
            'dimitri.molina@etu.univ-smb.fr',
            'Recommandation examens',
            `<!DOCTYPE html>
            <html lang="fr">
            <head>
                <meta charset="UTF-8">
                <meta name="viewport" content="width=device-width, initial-scale=1.0">
                <title>Recommandation examens</title>
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
                    <h1>Bonjour ${patient.patient.firstName},</h1>
                    <p>
                        Il est recommandé de passer des examens médicaux ce mois-ci. Veuillez prendre rendez-vous avec votre médecin le pour un suivi de votre état de santé.
                    </p>
                    <p>
                        Cordialement,<br>
                        Votre équipe médicale
                    </p>
                </div>
            </body>
            </html>`,
          )
        } catch (error) {
          res.status(400).json({ message: error })
        }
      }
    })
  } catch (error) {
    console.error(error.message)
    res.status(500).send('Server error')
  }

  return res.json('Emails sent successfully')
})

// @route   GET /api/medical/:id
// @desc    Get current users profile
// @access  Private
router.get('/:id', auth, (req, res) => {
  const errors = {}

  MedicalProfile.findOne({ patient: req.params.id })
    .populate('patient')
    .then((medicalProfile) => {
      if (!medicalProfile) {
        errors.nomedicalProfile = 'There is no medical Profile for this user'
        return res.status(404).json(errors)
      }
      res.json(medicalProfile)
    })
    .catch((err) => res.status(404).json(err))
})

router.post('/save', auth, async (req, res) => {
  try {
    // Extraire et transformer `req.body`
    const rawInvoice = req.body

    const rawMedicalProfile = new MedicalProfile(rawInvoice.medicalProfile)
    const rawUser = new User(rawInvoice.medicalProfile._patient)

    const user = await User.findByIdAndUpdate(rawUser._id, rawUser, {
      new: true,
      upsert: true,
    })

    const medicalProfile = await MedicalProfile.findByIdAndUpdate(
      rawMedicalProfile._id,
      rawMedicalProfile,
      { new: true, upsert: true },
    )

    res.json({
      message: 'Profil sauvegarder avec succès',
      invoice: medicalProfile,
    })
  } catch (error) {
    console.error('Erreur lors de la sauvegarde du profil:', error)
    res.status(500).json({ error: 'Erreur lors de la sauvegarde du profil' })
  }
})

module.exports = router
