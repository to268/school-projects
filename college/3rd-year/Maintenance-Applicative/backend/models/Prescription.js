const mongoose = require('mongoose')

// Schéma pour les Médicaments
const MedicineSchema = new mongoose.Schema({
  medicine: {
    type: String,
    required: true,
  },
  dosage: {
    type: String,
    required: true,
  },
  frequency: {
    type: String,
    required: true,
  },
  duration: {
    // type: String,
    type: Number,
    required: true,
  },
})

// Schéma pour la Prescription
const PrescriptionSchema = new mongoose.Schema({
  patient: {
    type: mongoose.Schema.Types.ObjectId,
    ref: 'user',
    required: true,
  },
  doctor: {
    type: mongoose.Schema.Types.ObjectId,
    ref: 'user',
    required: true,
  },
  licenseNumber: {
    type: String,
    required: true,
  },
  date: {
    type: Date,
    default: Date.now,
  },
  phoneNumber: {
    type: String,
    required: true,
  },
  signatureImage: {
    type: String,
    required: true,
  },
  pdf: {
    type: Buffer,
  },
  medicines: [MedicineSchema], // Liste de médicaments
})

// Exportation du modèle Prescription
module.exports = mongoose.model('Prescription', PrescriptionSchema)
