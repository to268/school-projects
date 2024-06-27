const jwt = require('jsonwebtoken')
const config = require('config')

const auth = (req, res, next) => {
  // Obtenir le token du header
  const token = req.header('Authorization')

  // Vérifier si le token n'est pas présent
  if (!token) {
    return res.status(401).json({ msg: 'No token, authorization denied' })
  }

  try {
    // Vérifier et décoder le token
    const decoded = jwt.verify(token.split(' ')[1], config.get('jwtSecret'))

    // Ajouter l'utilisateur au req
    req.user = decoded.user

    next()
  } catch (err) {
    res.status(401).json({ msg: 'Token is not valid' })
  }
}
module.exports = auth
