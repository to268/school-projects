const express = require('express')
const connectDB = require('./config/db')
const fileUpload = require('express-fileupload')
const cors = require('cors')
const bodyParser = require('body-parser')
const morgan = require('morgan')
const _ = require('lodash')
const path = require('path')
const cron = require('node-cron')

const app = express()
app.use(
  fileUpload({
    createParentPath: true,
  }),
)
const http = require('http')

// Configuration de express pour json et urlencoded
app.use(express.json({ limit: '50mb' }))
app.use(express.urlencoded({ limit: '50mb', extended: true }))

app.use(morgan('dev'))
connectDB()

app.use(cors())
app.use('/public', express.static(__dirname + '/public'))
app.use(express.json({ extended: false }))

app.use('/api/users', require('./routes/api/users'))
app.use('/api/auth', require('./routes/api/auth'))
app.use('/api/appointment', require('./routes/api/appointment'))
app.use('/api/invoice', require('./routes/api/invoice'))
app.use('/api/prescription', require('./routes/api/prescription'))
app.use('/api/medicalprofile', require('./routes/api/medicalprofile'))
app.use('/api/office', require('./routes/api/office'))
// app.use('/api/medicalprofile', require('./routes/api/medicalprofile'))
// app.use('/api/webhook', require('./routes/api/webhook'))

const server = http.Server(app)

if (process.env.NODE_ENV === 'test') {
  app.use('/test', require('./routes/test-data'))
}

if (process.env.NODE_ENV !== 'test') {
  const server = http.Server(app)
  const PORT = process.env.PORT || 5000

  server.listen(PORT, () => console.log(`Server started on port ${PORT}`))
}

// Planifie la tâche cron pour 8h tous les jours
cron.schedule(
  '0 8 * * *',
  async () => {
    try {
      // Appelle l'API pour envoyer les rappels
      const response = await fetch(
        'http://localhost:5000/api/appointment/sendReminder',
      )
      const data = await response.json()
    } catch (error) {
      console.error('Error calling API:', error.message)
    }
  },
  {
    scheduled: true,
    timezone: 'Europe/Paris', // fuseau horaire de paris
  },
)

// Planifie la tâche cron pour le 15 du mois a 8h
cron.schedule(
  '0 8 15 * *',
  async () => {
    try {
      // Appelle l'API pour envoyer les rappels
      const response = await fetch(
        'http://localhost:5000/api/medicalprofile/sendReminder',
      )
      const data = await response.json()
    } catch (error) {
      console.error('Error calling API:', error.message)
    }
  },
  {
    scheduled: true,
    timezone: 'Europe/Paris', // fuseau horaire de paris
  },
)

// Planifie la tâche cron pour tout les lundi a 8h
cron.schedule(
  '0 8 * * 1',
  async () => {
    try {
      // Appelle l'API pour envoyer les rappels
      const response = await fetch(
        'http://localhost:5000/api/prescription/sendReminder',
      )
      const data = await response.json()
    } catch (error) {
      console.error('Error calling API:', error.message)
    }
  },
  {
    scheduled: true,
    timezone: 'Europe/Paris', // fuseau horaire de paris
  },
)

module.exports = app
