const discogs = require('../../api/discogs.js')

const state = {
  artists: []
}

const getters = {
  albums: (state) => (artistId) => {
    const artist = state.artists.find(j => j.id === artistId)
    return artist ? artist.albums : []
  },
  artistInfo: (state) => (artistId) => {
    const artist = state.artists.find(j => j.id === artistId)
    return artist && artist.info ? artist.info : { profile: '' }
  },
  visibility: (state) => (artistId) => {
    const artist = state.artists.find(j => j.id === artistId)
    return artist ? artist.visibility : ''
  }
}

const actions = {
  async getAlbums ({ commit, state }, artistId) {
    const artist = state.artists.find(j => j.id === artistId)

    // get albums from discogs in case none was store previously
    if (!artist || !artist.albums) {
      var albums = await discogs.getArtistReleases(artistId)
      commit('storeAlbums', { artistId: artistId, albums: albums })
    }
    commit('setVisibility', { artistId: artistId, visibility: 'albums' })
  },
  async getInfo ({ commit, state }, artistId) {
    const artist = state.artists.find(j => j.id === artistId)
    if (!artist || !artist.info) {
      var info = await discogs.getArtistInfo(artistId)
      commit('storeInfo', { artistId: artistId, info: info })
    } else {
      console.log('found artist.info already, will set visibility to info', artist.info)
    }
    commit('setVisibility', { artistId: artistId, visibility: 'info' })
  }
}

const mutations = {
  storeAlbums (state, o) {
    const artist = state.artists.find(j => j.id === o.artistId)
    if (artist) {
      artist.albums = o.albums
    } else {
      state.artists = [...state.artists, { id: o.artistId, albums: o.albums }]
    }
  },
  storeInfo (state, o) {
    const artist = state.artists.find(j => j.id === o.artistId)
    if (artist) {
      console.log('storeInfo found artist, will update info, profile is: ', o.info)
      artist.info = o.info
    } else {
      console.log('storeInfo DIT NOT find artist, but will add ID and INFO, profile is: ', o.info)
      state.artists = [...state.artists, { id: o.artistId, info: o.info }]
    }
  },
  setVisibility (state, o) {
    const artist = state.artists.find(j => j.id === o.artistId)
    artist.visibility = o.visibility
  }
}

export default {
  state,
  getters,
  actions,
  mutations
}
