const express = require('express')
const router = express.Router()
const auth = require('../../middleware/auth')
const User = require('../../models/User')
const jwt = require('jsonwebtoken')
const crypto = require('crypto')
const config = require('config')
const bcrypt = require('bcryptjs')
const { check, validationResult } = require('express-validator')
const randomstring = require('randomstring')
const mailer = require('../../misc/mailer')
const ObjectId = require('mongoose').Types.ObjectId

// @route    POST /api/auth
// @desc     Authenticate user
// @access   Public
router.post(
  '/',
  [
    check('email', 'Please include a valid email').isEmail(),
    check('password', 'Password required').exists(),
  ],
  async (req, res) => {
    const errors = validationResult(req)
    if (!errors.isEmpty()) {
      return res.status(400).json({ errors: errors.array() })
    }

    const { email, password } = req.body

    try {
      let user = await User.findOne({ email })

      if (!user) {
        return res
          .status(400)
          .json({ errors: [{ msg: 'Invalid credentials' }] })
      }

      if (user.activated === false) {
        return res.status(400).json({ errors: [{ msg: 'Account disabled' }] })
      }

      const isMatch = await bcrypt.compare(password, user.password)
      if (!isMatch) {
        return res.status(400).json({ errors: [{ msg: 'Incorrect password' }] })
      }

      const payload = {
        user: {
          id: user._id,
          email: user.email,
          role: user.role,
        },
      }

      jwt.sign(
        payload,
        config.get('jwtSecret'),
        { expiresIn: 360000 },
        (err, token) => {
          if (err) throw err
          res.json({ token: token, user: user })
        },
      )
    } catch (err) {
      console.error(err.message)
      res.status(500).send('Server error')
    }
  },
)

// @route    POST /api/auth/logout
// @desc     Logout user
// @access   Private
router.post('/logout', auth, async (req, res) => {
  try {
    if (req.user) {
      req.user = null
    }

    res.json({ updated: true, Message: 'Logout successful' })
  } catch (err) {
    console.error(err.message)
    res.status(500).send('Server Error')
  }
})

// @route    PUT /api/auth/updatePassword
// @desc     Change password
// @access   Private
router.put('/updatePassword', auth, async (req, res) => {
  const { newPass, oldPass } = req.body
  let user = await User.findById(req.user.id)

  if (!user) {
    return res.status(400).json({ errors: [{ msg: 'Invalid credentials' }] })
  }

  if (user.activated === false) {
    return res.status(400).json({ errors: [{ msg: 'Account disabled' }] })
  }

  const isMatch = await bcrypt.compare(oldPass, user.password)
  if (!isMatch) {
    return res.status(400).json({ errors: [{ msg: 'Incorrect password' }] })
  }

  if (newPass.length < 12) {
    return res.status(400).json({
      errors: [{ msg: 'Password should contains at least 12 characters' }],
    })
  }

  const salt = await bcrypt.genSalt(10)
  user.password = await bcrypt.hash(newPass, salt)
  await user.save()

  res.json({ updated: true, Message: 'Password updated succesfully' })
})

// @route    PUT /api/auth/resetPassword
// @desc     Reset password
// @access   Public
router.put('/resetPassword', async (req, res) => {
  const { email } = req.body
  const user = await User.findOne({ email })

  if (!user) {
    return res.status(404).json({ errors: [{ msg: 'User not found' }] })
  }

  const newPass = randomstring.generate(12)
  const salt = await bcrypt.genSalt(10)
  user.password = await bcrypt.hash(newPass, salt)
  await user.save()

  const html = `Bonjour,
    <br/>
    Voici votre nouveau mot de passe : ${newPass}
    <br/>
    Cordialement.
    `
  await mailer.sendMail(
    'labes.assitance@gmail.com',
    user.email,
    'Reset password',
    html,
  )

  res.json({ updated: true, Message: 'Email sent' })
})

const isValidBirthday = (date) => {
  const today = new Date()
  const birthDate = new Date(date)
  const ageLimit = 150
  const minimumAge = 18

  const age = today.getFullYear() - birthDate.getFullYear()
  const isPastDate = birthDate <= today
  const isWithinAgeLimit = age <= ageLimit && age >= minimumAge

  return isPastDate && isWithinAgeLimit
}

const generateRegistrationToken = () => {
  if (process.env.NODE_ENV === 'test') {
    return 'd4d2c3bdaf4b50ed30171847388ba85ac0f79df7'
  } else {
    return crypto.randomBytes(20).toString('hex')
  }
}

const sendVerificationEmail = async (userEmail, token) => {
  const verificationUrl = `http://localhost:5000/api/auth/verify/${token}`
  const html = `
<!DOCTYPE html>
<html lang="fr">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Veuillez vérifier votre compte</title>
  <style>
    body {
      font-family: Arial, sans-serif;
      background-color: #f9f9f9;
      color: #333;
      padding: 20px;
      margin: 0;
    }
    .container {
      background-color: #fff;
      padding: 20px;
      border-radius: 8px;
      box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
      max-width: 600px;
      margin: 0 auto;
    }
    .content h2 {
      color: #007BFF;
      font-size: 22px;
      margin-top: 0;
      text-align: center;
    }
    .content p {
      line-height: 1.6;
      text-align: center;
    }
    .button {
      display: flex;
      padding: 10px 20px;
      margin-top: 20px;
      font-size: 18px;
      color: #fff;
      background-color: #e05443;
      text-decoration: none;
      border-radius: 5px;
      text-align: center;
      align-items: center;
      justify-content: center;
    }
    @media only screen and (max-width: 550px) {
      .container {
        padding: 10px;
      }
    }
  </style>
</head>
<body>
  <div class="container">
    <div class="content">
      <h2>Merci de vérifier votre compte !</h2>
      <p>
        Veuillez cliquer sur le bouton ci-dessous pour vérifier votre compte.
      </p>
      <a href="${verificationUrl}" class="button">Verify</a>
    </div>
  </div>
</body>
</html>
`

  await mailer.sendMail(
    'labes.assitance@gmail.com',
    userEmail,
    'Veuillez vérifier votre compte',
    html,
  )
}

// @route    POST /api/auth/register
// @desc     Register user
// @access   Public
router.post(
  '/register',
  [
    check('firstName', 'First name is required').not().isEmpty(),
    check('lastName', 'Last name is required').not().isEmpty(),
    check('email', 'Please include a valid email').isEmail(),
    check(
      'password',
      'Please enter a password with 12 or more characters',
    ).isLength({ min: 12 }),
    check('birthDate', 'Birth date is required and should be a valid date')
      .isDate()
      .custom((value) => isValidBirthday(value)),
    check('role', 'Role is required').not().isEmpty(),
    check('sex', 'Sex is required').not().isEmpty(),
  ],
  async (req, res) => {
    const errors = validationResult(req)
    if (!errors.isEmpty()) {
      return res.status(400).json({ errors: errors.array() })
    }

    const { firstName, lastName, email, password, birthDate, role, sex } =
      req.body

    try {
      let user = await User.findOne({ email })

      if (user) {
        return res
          .status(400)
          .json({ errors: [{ msg: 'User already exists' }] })
      }

      const secretToken = generateRegistrationToken()

      user = new User({
        _id: new ObjectId(),
        firstName,
        lastName,
        email,
        password,
        birthDate,
        role,
        sex,
        activated: false,
        secretToken: secretToken,
      })

      await sendVerificationEmail(email, secretToken)

      const salt = await bcrypt.genSalt(10)
      user.password = await bcrypt.hash(password, salt)
      await user.save()

      res.status(200).json({
        msg: 'Registration successful. Please check your email to verify your account.',
      })
    } catch (err) {
      console.error(err.message)
      res.status(500).send('Server error')
    }
  },
)

// @route    GET /api/auth/verify/:token
// @desc     Verify user account
// @access   Public
router.get('/verify/:token', async (req, res) => {
  try {
    const { token } = req.params

    const user = await User.findOne({ secretToken: token })

    if (!user) {
      return res
        .status(400)
        .json({ errors: [{ msg: 'Invalid or expired verification token' }] })
    }

    user.activated = true
    user.secretToken = ''
    await user.save()

    res
      .status(200)
      .json({ msg: 'Account verified successfully. You can now log in.' })
  } catch (err) {
    console.error(err.message)
    res.status(500).send('Server error')
  }
})

module.exports = router
