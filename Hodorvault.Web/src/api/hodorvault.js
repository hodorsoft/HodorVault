const request = require('request-promise')
const vaultUrl = process.env.VAULT_API

module.exports.getCollection = async function () {
  var result = []

  await request.get({
    url: vaultUrl + '/vault',
    json: true
  })
    .then(function (response) {
      result = response
    })

  return result
}

// add entry to vault
module.exports.addToCollection = async function (artist, album) {
  var info = null

  await request.post({
    url: vaultUrl + '/vault',
    headers: {
      'Content-Type': 'application/json'
    },
    body: {
      artist: {
        id: artist.id,
        name: artist.title,
        country: artist.country
      },
      album: {
        id: album.id,
        name: album.title,
        type: album.type,
        year: album.year
      }
    },
    json: true
  })
    .then(function (response) {
      info = response
    })

  return info
}

module.exports.updateEntry = async function (artist, album) {
  var info = null

  await request.put({
    url: vaultUrl + '/vault',
    headers: {
      'Content-Type': 'application/json'
    },
    body: {
      artist: {
        id: artist.id,
        name: artist.name,
        country: artist.country
      },
      album: {
        id: album.id,
        name: album.name,
        type: album.type,
        year: album.year
      }
    },
    json: true
  })
    .then(function (response) {
      info = response
    })

  return info
}

module.exports.deleteEntry = async function (artist, album) {
  var result = {}
  var url = vaultUrl

  console.log('artist.id', artist.id, 'album.id', album.id)

  if (artist.id && album.id) url = url + '/albums/' + album.id
  else if (artist.id && !album.id) url = url + '/artists/' + artist.id
  else if (!artist.id && album.id) url = url + '/albums/' + album.id

  console.log('sending to', url)

  await request.delete({
    url: url,
    json: true
  })
    .then(function (response) {
      result = response
    })
    .catch(function (e) {
      result = { success: false, error: e }
    })

  return result
}
