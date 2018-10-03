'use strict'
const merge = require('webpack-merge')
const devEnv = require('./dev.env')

module.exports = merge(devEnv, {
  NODE_ENV: '"testing"',
  DISCOGS_API: JSON.stringify(process.env.DISCOGS_API),
  DISCOGS_TOKEN: JSON.stringify(process.env.DISCOGS_TOKEN),
  VAULT_API: JSON.stringify(process.env.VAULT_API)
})
