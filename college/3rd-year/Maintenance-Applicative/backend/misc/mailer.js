const nodemailer = require('nodemailer')
const config = require('../config/mailer')

const transport = nodemailer.createTransport({
  host: 'sandbox.smtp.mailtrap.io',
  port: 2525,
  auth: {
    user: '',
    pass: '',
  },
})

module.exports = {
  sendMail(from, to, subject, html) {
    // Do not send a mail in test environment
    if (process.env.NODE_ENV === 'test') {
      return
    }

    return new Promise((resolve, reject) => {
      transport.sendMail({ from, subject, to, html }, (err, info) => {
        if (err) {
          reject(err)
        }
        resolve(info)
      })
    })
  },
}
