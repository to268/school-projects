const express = require('express')
const router = express.Router()
const Prescription = require('../../models/Prescription')
const auth = require('../../middleware/auth')
const User = require('../../models/User')
const MedicalProfile = require('../../models/MedicalProfile')
const ObjectId = require('mongoose').Types.ObjectId
const mailer = require('../../misc/mailer')

// Route POST pour créer une nouvelle prescription
router.post('/', auth, async (req, res) => {
  try {
    const rawPrescription = req.body

    patient = await User.findOne({
      _id: new ObjectId(rawPrescription.patientId),
    })

    doctor = await User.findOne({
      _id: new ObjectId(rawPrescription.doctorId),
    })

    const prescription = new Prescription({
      patient: patient._id,
      doctor: doctor._id,
      licenseNumber: '0654654181654165154',
      date: new Date(rawPrescription.date),
      phoneNumber: doctor.phone.mobile[0],
      signatureImage: __dirname + '/../../public/prescription/signature.png',
      medicines: rawPrescription.medicines.map((item) => ({
        medicine: item.medicine,
        dosage: item.dosage,
        frequency: item.frequency,
        duration: item.duration,
      })),
    })

    let result = await prescription.save()

    let prescriptionSave = new Prescription(
      await Prescription.findOne({ _id: result._id })
        .populate({
          path: 'patient',
        })
        .populate({
          path: 'doctor',
          populate: {
            path: 'office',
          },
        }),
    )

    createPrescription(prescriptionSave, 'prescription.pdf')

    const buff = fs.readFileSync('prescription.pdf')

    prescriptionSave.pdf = buff

    prescriptionSave.save()

    res.json({
      message: 'Prescription créée avec succès',
      prescription: prescriptionSave,
    })
  } catch (error) {
    console.error('Erreur lors de la création de la prescription:', error)
    res
      .status(500)
      .json({ error: 'Erreur lors de la création de la prescription' })
  }
})

// Route POST pour créer une nouvelle prescription
router.get('/all', auth, async (req, res) => {
  try {
    const prescription = await Prescription.find({})
      .populate({
        path: 'patient',
      })
      .populate({
        path: 'doctor',
      })

    res.json({
      message: 'Prescription récupéré avec succès',
      prescription: prescription,
    })
  } catch (error) {
    console.error('Erreur lors de la récupération de la prescription:', error)
    res
      .status(500)
      .json({ error: 'Erreur lors de la récupération de la prescription' })
  }
})

// @route   GET /api/prescription/getByUserId/:_id
// @desc    get prescription by user Id
// @access  Private
router.get('/getByUserId/:id', auth, async (req, res) => {
  try {
    const id = req.params.id
    const users = await User.findOne({ _id: req.params.id })
    let prescription = ''
    if (users.role === 'doctor') {
      prescription = await Prescription.find({ doctor: id })
    } else {
      prescription = await Prescription.find({ patient: id })
    }
    if (prescription) {
      res.json({
        message: 'Prescription récupéré avec succès',
        prescription: prescription,
      })
    } else {
      return res.json({ err: 'Not found' })
    }
  } catch (error) {
    console.error(error.message)
    res.status(500).send('Server error')
  }
})

// @route   GET /api/prescription/sendReminder
// @desc    send Reminder
// @access  Private
router.get('/sendReminder', async (req, res) => {
  try {
    const prescriptionWithPatient = await Prescription.find({}).populate({
      path: 'patient',
    })
    prescriptionWithPatient.forEach(async (prescription) => {
      let date = new Date(prescription.date)
      let email = prescription.patient.email
      for (let medicine of prescription.medicines) {
        let dateLimit = new Date(date)
        dateLimit.setDate(dateLimit.getDate() + medicine.duration)

        dateLimit.setHours(0, 0, 0, 0)

        // Calculate the difference in time (milliseconds)
        let timeDifference = dateLimit - new Date()

        // Convert time difference from milliseconds to days
        let daysRemaining = Math.ceil(timeDifference / (1000 * 60 * 60 * 24))
        if (daysRemaining <= 7) {
          await mailer.sendMail(
            'labes.assitance@gmail.com',
            email,
            `Rappel: date limite pour le médicament ${medicine.medicine}`,
            `<!DOCTYPE html>
              <html lang="fr">
              <head>
                  <meta charset="UTF-8">
                  <meta name="viewport" content="width=device-width, initial-scale=1.0">
                  <title>Rappel: date limite pour le médicament ${medicine.medicine}</title>
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
                      <h1>Bonjour,</h1>
                      <p>
                          Ceci est un rappel pour votre médicament <strong>${medicine.medicine}</strong>.
                          Il vous reste <strong>${daysRemaining} jour(s)</strong> avant la date limite.
                      </p>
                      <p>
                          <strong>Date limite :</strong> ${dateLimit.toISOString().split('T')[0]}
                      </p>
                      <p>
                          Cordialement,<br>
                          Votre équipe médicale
                      </p>
                  </div>
              </body>
              </html>`,
          )
        }
      }
    })
  } catch (error) {
    console.error(error.message)
    res.status(500).send('Server error')
  }

  return res.json('Emails sent successfully')
})

router.get('/:id/download', auth, async (req, res) => {
  try {
    const prescription = await Prescription.findById(req.params.id)

    if (!prescription || !prescription.pdf) {
      return res.status(404).json({ error: 'Prescription or PDF not found' })
    }

    res.setHeader('Content-Type', 'application/pdf')
    res.setHeader(
      'Content-Disposition',
      `attachment; filename=prescription_${prescription._id}.pdf`,
    )
    res.send(prescription.pdf)
  } catch (error) {
    console.error('Error downloading the PDF:', error)
    res.status(500).json({ error: 'Error downloading the PDF' })
  }
})

const PDFDocument = require('pdfkit')
const fs = require('fs')

function createPrescription(prescription, path) {
  let doc = new PDFDocument({ margin: 50 })

  doc.pipe(fs.createWriteStream(path))

  generateHeader(doc, prescription)
  generatePatientInformation(doc, prescription)
  generatePrescriptionTable(doc, prescription)
  generateFooter(doc, prescription)

  doc.end()
}

function generateHeader(doc, prescription) {
  console.log(prescription.doctor.office.address.city)
  doc
    .image(__dirname + '/../../public/invoice/doctor.jpg', 50, 45, {
      width: 50,
    })
    .fillColor('#444444')
    .fontSize(20)
    .text(prescription.doctor.office.name, 110, 57, {
      width: 150,
      align: 'left',
    })
    .fontSize(10)
    .text(prescription.doctor.office.name, 200, 50, { align: 'right' })
    .text(prescription.doctor.office.address.road, 200, 65, { align: 'right' })
    .text(prescription.doctor.office.address.city, 200, 80, { align: 'right' })
    .text(prescription.doctor.office.address.postalCode, 200, 95, {
      align: 'right',
    })
    .moveDown()
}

function generatePatientInformation(doc, prescription) {
  doc.fillColor('#444444').fontSize(20).text('Prescription Médicale', 50, 160)

  generateHr(doc, 185)

  const patientInformationTop = 200

  doc
    .fontSize(10)
    .text('Nom du Patient:', 50, patientInformationTop)
    .text(
      prescription.patient.firstName + ' ' + prescription.patient.lastName,
      150,
      patientInformationTop,
    )
    .text('Date:', 50, patientInformationTop + 15)
    .text(
      prescription.date.toLocaleDateString('en-GB'),
      150,
      patientInformationTop + 15,
    )

    .text('Nom du Médecin:', 300, patientInformationTop)
    .text(
      prescription.doctor.firstName + ' ' + prescription.doctor.lastName,
      450,
      patientInformationTop,
    )
    .text("N° de Permis d'Exercice:", 300, patientInformationTop + 15)
    .text(prescription.licenseNumber, 450, patientInformationTop + 15)
    .moveDown()

  generateHr(doc, 252)
}

function generatePrescriptionTable(doc, prescription) {
  let i
  const prescriptionTableTop = 280

  doc.font('Helvetica-Bold')
  generateTableRow(
    doc,
    prescriptionTableTop,
    'Médicament',
    'Dosage',
    'Fréquence',
    'Durée',
  )
  generateHr(doc, prescriptionTableTop + 20)
  doc.font('Helvetica')

  for (i = 0; i < prescription.medicines.length; i++) {
    const item = prescription.medicines[i]
    const position = prescriptionTableTop + (i + 1) * 30
    generateTableRow(
      doc,
      position,
      item.medicine,
      item.dosage,
      item.frequency,
      item.duration,
    )

    generateHr(doc, position + 20)
  }
}

function generateFooter(doc, prescription) {
  doc
    .fontSize(10)
    .text(
      'Merci de suivre les instructions de prescription. Pour toute question, contactez votre médecin.',
      50,
      720,
      { align: 'center', width: 500 },
    )

  generateHr(doc, 750)

  doc
    .text('Téléphone:', 50, 650)
    .text(prescription.phoneNumber, 120, 650)
    .text('Signature:', 400, 650)
    .image(prescription.signatureImage, 450, 600, { width: 100 })
}

function generateTableRow(doc, y, medicine, dosage, frequency, duration) {
  doc
    .fontSize(10)
    .text(medicine, 50, y)
    .text(dosage, 200, y)
    .text(frequency, 300, y)
    .text(duration, 400, y)
}

function generateHr(doc, y) {
  doc.strokeColor('#aaaaaa').lineWidth(1).moveTo(50, y).lineTo(550, y).stroke()
}

module.exports = router
