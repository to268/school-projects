const PaymentSchema = new mongoose.Schema({
  invoiceId: {
    type: mongoose.Schema.Types.ObjectId,
    ref: 'Invoice',
    required: true,
  },
  amount: { type: Number, required: true },
  method: {
    type: String,
    enum: ['Credit Card', 'Bank Transfer', 'Other'],
    required: true,
  },
  status: {
    type: String,
    enum: ['Pending', 'Completed', 'Failed'],
    default: 'Pending',
  },
  transactionId: { type: String },
  createdAt: { type: Date, default: Date.now },
})

module.exports = mongoose.model('Payment', PaymentSchema)
