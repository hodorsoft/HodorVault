const state = {
  artists: []
}

const getters = {
  artists: state => state.artists
}

const actions = {
  storeArtists ({ commit, state }, artists) {
    commit('storeArtists', artists.payload)
  }
}

const mutations = {
  storeArtists (state, artists) {
    state.artists = artists
  }
}

export default {
  state,
  getters,
  actions,
  mutations
}
