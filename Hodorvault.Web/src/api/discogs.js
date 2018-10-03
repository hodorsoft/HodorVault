const request = require('request-promise')
var discogsAuthHeaders = { 'Authorization': 'Discogs token=' + process.env.DISCOGS_TOKEN }
const discogsUrl = process.env.DISCOGS_API

// search artist from discogs api
module.exports.searchArtists = async function (q) {
  var results = []

  await request({
    url: discogsUrl + '/database/search',
    qs: {
      q: q,
      type: 'artist'
    },
    headers: discogsAuthHeaders,
    json: true
  })
    .then(function (response) {
      results = response.results
    })

  return results
}

module.exports.getArtistInfo = async function (artistId) {
  var info = null

  await request({
    url: discogsUrl + '/artists/' + artistId,
    headers: discogsAuthHeaders,
    json: true
  })
    .then(function (response) {
      info = response
    })

  return info
}

// get all releases of a specific artist
module.exports.getArtistReleases = async function (artistId) {
  var albums = []

  await request({
    url: discogsUrl + '/artists/' + artistId + '/releases',
    qs: {
      sort: 'year',
      sort_order: 'asc'
    },
    headers: discogsAuthHeaders,
    json: true
  })
    .then(function (response) {
      albums = response.releases
    })

  return albums
}
