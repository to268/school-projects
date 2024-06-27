const mongoose = require('mongoose')

const OfficeSchema = mongoose.Schema({
  name: {
    type: String,
    required: true,
  },
  email: {
    type: String,
    required: true,
    unique: true,
  },
  address: {
    road: { type: String },
    city: { type: String },
    postalCode: { type: String },
    state: { type: String },
  },
  landline: {
    type: [String],
  },
})

const Office = mongoose.model('office', OfficeSchema)
module.exports = Office
