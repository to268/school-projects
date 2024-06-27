const mongoose = require('mongoose')

const QuoteSchema = mongoose.Schema({
  doctor: {
    type: mongoose.Schema.Types.ObjectId,
    ref: 'user',
  },
  patient: {
    type: mongoose.Schema.Types.ObjectId,
    ref: 'user',
  },
  appointment: {
    type: mongoose.Schema.Types.ObjectId,
    ref: 'Appointment',
  },
  price: {
    type: Number,
  },
  tax: {
    type: Number,
  },
  priceWithTax: {
    type: Number,
  },
})
module.exports = Quote = mongoose.model('Quote', QuoteSchema)
