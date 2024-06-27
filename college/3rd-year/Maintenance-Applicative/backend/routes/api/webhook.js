const express = require('express')
const router = express.Router()
const Invoice = require('../../models/Invoice')
var ObjectId = require('mongoose').Types.ObjectId

// Match the raw body to content type application/json
// If you are using Express v4 - v4.16 you need to use body-parser, not express, to retrieve the request body
router.post(
  '/webhook',
  express.json({ type: 'application/json' }),
  (request, response) => {
    const event = request.body

    let invoiceId = null

    let invoice = null

    // Handle the event
    switch (event.type) {
      case 'payment_intent.succeeded':
        const paymentIntent = event.data.object

        invoiceId = paymentIntent.metadata.invoiceId

        invoice = Invoice.findOne({ _id: new ObjectId(invoiceId) })

        invoice.status = 'Paid'
        // Then define and call a method to handle the successful payment intent.
        // handlePaymentIntentSucceeded(paymentIntent);
        break
      case 'payment_method.payment_failed':
        const paymentMethod = event.data.object

        invoiceId = paymentIntent.metadata.invoiceId

        invoice = Invoice.findOne({ _id: new ObjectId(invoiceId) })

        invoice.status = 'Cancel'
        // Then define and call a method to handle the successful attachment of a PaymentMethod.
        // handlePaymentMethodAttached(paymentMethod);
        break
      // ... handle other event types
      default:
    }

    // Return a response to acknowledge receipt of the event
    response.json({ received: true })
  },
)
