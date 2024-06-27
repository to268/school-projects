const mongoose = require('mongoose')

const DiseaseSchema = new mongoose.Schema({
  name: { type: String, required: true },
  diagnosisDate: { type: Date },
  details: { type: String },
})

const AllergySchema = new mongoose.Schema({
  allergy: { type: String, required: true },
  reaction: { type: String },
  severity: { type: Number },
})

const SurgerySchema = new mongoose.Schema({
  type: { type: String, required: true },
  date: { type: Date },
  details: { type: String },
})

const DiagnosticSchema = new mongoose.Schema({
  diagnostic: { type: String, required: true },
  date: { type: Date, default: Date.now },
  treatment: { type: String },
  notes: { type: String },
})

const MedicalProfileSchema = new mongoose.Schema({
  patient: {
    type: mongoose.Schema.Types.ObjectId,
    ref: 'user',
  },
  diseases: [DiseaseSchema],
  allergies: [AllergySchema],
  surgery: [SurgerySchema],
  diagnostics: [DiagnosticSchema],
  health: { type: Number, min: 0, max: 5 },
  notes: {
    type: String,
  },
  bloodType: {
    type: String,
    enum: ['O-', 'A-', 'B-', 'AB-', 'O+', 'A+', 'B+', 'AB+'],
  },
  weight: {
    type: Number, // in kg
  },
  height: {
    type: Number, // in cm
  },
  famillyHistory: {
    type: String,
  },
  lastUpdate: {
    type: Date,
    default: Date.now,
  },
})

module.exports = mongoose.model('MedicalProfile', MedicalProfileSchema)
