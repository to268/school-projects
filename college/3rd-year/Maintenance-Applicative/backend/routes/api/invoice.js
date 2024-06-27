const express = require('express')
const router = express.Router()
const Invoice = require('../../models/Invoice')
const User = require('../../models/User')
const PDFDocument = require('pdfkit')
const fs = require('fs')
var ObjectId = require('mongoose').Types.ObjectId

const auth = require('../../middleware/auth')

const stripe = require('stripe')(
  'sk_test_51PNbyZP5Jlvk7SnFHSK0dPiI7RB500yL6eu7ExGPK9cpxYwF0I9yYgg17ruidBMUpEQlveEImjtdKPYOo95t97wp00PPRTWZff',
)

router.post('/payement', auth, async (req, res) => {
  try {
    const YOUR_DOMAIN = 'http://localhost:5173'

    const { invoiceId } = req.body

    const invoice = await Invoice.findOne({ _id: new ObjectId(invoiceId) })

    const priceId = await createProduct(
      'Facture du ' + new Date(invoice.createdAt).toLocaleDateString(),
      invoice.totalAmount * 100,
      'EUR',
    )

    const session = await stripe.checkout.sessions.create({
      line_items: [
        {
          // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
          price: priceId,
          quantity: 1,
        },
      ],
      mode: 'payment',
      success_url: `${YOUR_DOMAIN}/Success`,
      cancel_url: `${YOUR_DOMAIN}/Cancel`,
      metadata: { invoiceId: invoice._id.toString() },
    })

    res.json({ redirectUrl: session.url })
  } catch (error) {
    console.error('Erreur lors du paiement:', error)
    res.status(500).json({ error: 'Erreur lors paiement' })
  }
})

router.post('/', auth, async (req, res) => {
  try {
    // Extraire et transformer `req.body`
    const rawInvoice = req.body
    const doctorId = rawInvoice.doctorId
    const patientId = rawInvoice.patientId

    const user = await User.findOne({ _id: new ObjectId(patientId) })
    const doctor = await User.findOne({ _id: new ObjectId(doctorId) }).populate(
      {
        path: 'office',
      },
    )

    if (!user) {
      throw new Error('Profil non trouvé')
    }

    if (!Array.isArray(user.invoices)) {
      user.invoices = []
    }

    if (!rawInvoice.items || !Array.isArray(rawInvoice.items)) {
      return res.status(400).json({ error: 'Invalid items array' })
    }
    // Créer une nouvelle instance de `Invoice` avec Mongoose
    const invoice = new Invoice({
      patientId: rawInvoice.patientId,
      items: rawInvoice.items.map((item) => ({
        product: item.product,
        description: item.description,
        quantity: item.quantity,
        price: item.price,
        total: item.total,
      })),
      totalAmount: rawInvoice.totalAmount,
      status: rawInvoice.status,
      createdAt: new Date(rawInvoice.createdAt),
      updatedAt: new Date(rawInvoice.updatedAt),
    })

    // Ajouter la propriété `shipping`
    invoice.shipping = {
      name: user.firstName + ' ' + user.lastName, //'John Doe',
      address: user.address.road, //'1234 Main Street',
      city: user.address.city, //'San Francisco',
      state: user.address.state, //'CA',
      country: 'FR',
      postal_code: user.address.postalCode, //'94111',
    }

    createInvoice(invoice, 'invoice.pdf', doctor)

    const buff = fs.readFileSync('invoice.pdf')

    invoice.pdf = buff

    await invoice.save()

    user.invoices.push(invoice._id)
    doctor.invoices.push(invoice._id)

    await user.save()
    await doctor.save()

    res.json({
      message: 'Facture créée avec succès',
      invoice: invoice,
    })
  } catch (error) {
    console.error('Erreur lors de la création de la facture:', error)
    res.status(500).json({ error: 'Erreur lors de la création de la facture' })
  }
})

router.get('/:id/download', auth, async (req, res) => {
  try {
    const invoice = await Invoice.findById(req.params.id)

    if (!invoice || !invoice.pdf) {
      return res.status(404).json({ error: 'Facture ou PDF non trouvée' })
    }

    res.setHeader('Content-Type', 'application/pdf')
    res.setHeader(
      'Content-Disposition',
      `attachment; filename=invoice_${invoice._id}.pdf`,
    )
    res.send(invoice.pdf)
  } catch (error) {
    console.error('Erreur lors du téléchargement du PDF:', error)
    res.status(500).json({ error: 'Erreur lors du téléchargement du PDF' })
  }
})

async function createProduct(productName, unitAmount, currency) {
  const product = await stripe.products.create({
    name: productName,
  })

  const price = await stripe.prices.create({
    product: product.id,
    unit_amount: unitAmount,
    currency: currency,
  })

  return price.id
}

function createInvoice(invoice, path, doctor) {
  let doc = new PDFDocument({ margin: 50 })

  doc.pipe(fs.createWriteStream(path))

  generateHeader(doc, doctor)
  generateCustomerInformation(doc, invoice)
  generateInvoiceTable(doc, invoice)
  generateFooter(doc)

  doc.end()
}

function generateHeader(doc, doctor) {
  doc
    .image(__dirname + '/../../public/invoice/doctor.jpg', 50, 45, {
      width: 50,
    })
    .fillColor('#444444')
    .fontSize(20)
    .text(doctor.office.name, 110, 57, {
      width: 150,
      align: 'left',
    })
    .fontSize(10)
    .text(doctor.office.name, 200, 50, { align: 'right' })
    .text(doctor.office.address.road, 200, 65, { align: 'right' })
    .text(doctor.office.address.city, 200, 80, { align: 'right' })
    .text(doctor.office.address.postalCode, 200, 95, {
      align: 'right',
    })
    .moveDown()
}

function generateCustomerInformation(doc, invoice) {
  doc.fillColor('#444444').fontSize(20).text('Invoice', 50, 160)

  generateHr(doc, 185)

  const customerInformationTop = 200
  const subtotal = calculateSubtotal(invoice)

  doc
    .fontSize(10)
    .text('Invoice Number:', 50, customerInformationTop)
    .text(invoice.invoice_nr, 150, customerInformationTop)
    .text('Invoice Date:', 50, customerInformationTop + 15)
    .text(
      invoice.createdAt.toLocaleDateString('en-GB'),
      150,
      customerInformationTop + 15,
    )
    .text('Balance Due:', 50, customerInformationTop + 30)
    .text(subtotal /*- invoice.paid*/, 150, customerInformationTop + 30)

    .text('Customer Name:', 300, customerInformationTop)
    .text(invoice.shipping.name, 400, customerInformationTop)
    .text('Customer Address:', 300, customerInformationTop + 15)
    .text(invoice.shipping.address, 400, customerInformationTop + 15)
    .text(
      invoice.shipping.city +
        ', ' +
        invoice.shipping.state +
        ', ' +
        invoice.shipping.country,
      400,
      customerInformationTop + 30,
    )
    .moveDown()

  generateHr(doc, 252)
}

function generateInvoiceTable(doc, invoice) {
  let i
  const invoiceTableTop = 330

  doc.font('Helvetica-Bold')
  generateTableRow(
    doc,
    invoiceTableTop,
    'Item',
    'Description',
    'Unit Cost',
    'Quantity',
    'Line Total',
  )
  generateHr(doc, invoiceTableTop + 20)
  doc.font('Helvetica')

  for (i = 0; i < invoice.items.length; i++) {
    const item = invoice.items[i]
    const position = invoiceTableTop + (i + 1) * 30
    generateTableRow(
      doc,
      position,
      item.product,
      item.description,
      item.price,
      item.quantity,
      item.price * item.quantity,
    )

    generateHr(doc, position + 20)
  }

  const subtotal = calculateSubtotal(invoice)
  const subtotalPosition = invoiceTableTop + (i + 1) * 30
  generateTableRow(doc, subtotalPosition, '', '', 'Subtotal', '', subtotal)

  const paidToDatePosition = subtotalPosition + 20
  generateTableRow(
    doc,
    paidToDatePosition,
    '',
    '',
    'Paid To Date',
    '',
    0, //invoice.paid
  )

  const duePosition = paidToDatePosition + 25
  doc.font('Helvetica-Bold')
  generateTableRow(
    doc,
    duePosition,
    '',
    '',
    'Balance Due',
    '',
    subtotal, //- invoice.paid
  )
  doc.font('Helvetica')
}

function generateFooter(doc) {
  doc
    .fontSize(10)
    .text(
      'Payment is due within 15 days. Thank you for your business.',
      50,
      720,
      { align: 'center', width: 500 },
    )
}

function generateTableRow(
  doc,
  y,
  item,
  description,
  unitCost,
  quantity,
  lineTotal,
) {
  doc
    .fontSize(10)
    .text(item, 50, y)
    .text(description, 150, y)
    .text(unitCost, 280, y, { width: 90, align: 'right' })
    .text(quantity, 370, y, { width: 90, align: 'right' })
    .text(lineTotal, 0, y, { align: 'right' })
}

function generateHr(doc, y) {
  doc.strokeColor('#aaaaaa').lineWidth(1).moveTo(50, y).lineTo(550, y).stroke()
}

function calculateSubtotal(invoice) {
  subtotal = 0
  invoice.items.forEach((item) => {
    subtotal += item.price * item.quantity
  })

  return subtotal
}

module.exports = router
