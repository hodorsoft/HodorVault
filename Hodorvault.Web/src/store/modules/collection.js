const state = {
  entries: []
}

const getters = {
  entries: state => state.entries
}

const actions = {
  storeEntries ({ commit, state }, entries) {
    commit('storeEntries', entries.payload)
  }
}

const mutations = {
  storeArtists (state, entries) {
    state.entries = entries
  }
}

export default {
  state,
  getters,
  actions,
  mutations
}
