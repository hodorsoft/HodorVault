'use strict'
const dotenv = require('dotenv').config()

module.exports = {
  NODE_ENV: '"production"',
  DISCOGS_API: JSON.stringify(process.env.DISCOGS_API),
  DISCOGS_TOKEN: JSON.stringify(process.env.DISCOGS_TOKEN),
  VAULT_API: JSON.stringify(process.env.VAULT_API)
}
