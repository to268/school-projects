const mongoose = require('mongoose')

const Status = Object.freeze({
  Free: 'free',
  Occupied: 'occupied',
  Confirmed: 'confirmed',
})

const AppointmentSchema = mongoose.Schema({
  doctor: {
    type: mongoose.Schema.Types.ObjectId,
    ref: 'user',
    required: true,
  },
  patient: {
    type: mongoose.Schema.Types.ObjectId,
    ref: 'user',
    required: true,
  },
  startDate: {
    type: Date,
    required: true,
  },
  endDate: {
    type: Date,
    required: true,
  },
  state: {
    type: String,
    enum: Object.values(Status),
    required: true,
  },
  reason: {
    type: String,
    required: true,
  },
  description: {
    type: String,
  },
  price: {
    type: Number,
    required: true,
  },
  eventId: {
    type: mongoose.Types.ObjectId,
  },
  color: {
    type: String,
  },
  attachedFiles: {
    type: [String],
  },
})
Object.assign(AppointmentSchema.statics, {
  Status,
})

module.exports = Appointment = mongoose.model('Appointment', AppointmentSchema)
