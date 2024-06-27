const mongoose = require('mongoose')

const MedicationSchema = new mongoose.Schema({
  name: { type: String, required: true },
  genericName: { type: String, required: true },
  medicationCode: { type: String, required: true, unique: true },
  form: { type: String, required: true },
  dosage: { type: String, required: true },
  frequency: { type: String, required: true },
  duration: { type: String, required: true },
  administrationInstructions: { type: String },
  sideEffects: { type: String },
  contraindications: { type: String },
  manufacturer: { type: String },
  lotNumber: { type: String },
  expirationDate: { type: Date },
  therapeuticClass: { type: String },
  stockAvailable: { type: Number },
  price: { type: Number },
  additionalNotes: { type: String },
})

module.exports = mongoose.model('Medication', MedicationSchema)
