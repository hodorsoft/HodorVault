import Vue from 'vue'
import Vuex from 'vuex'
import home from './modules/home.js'
import discogsArtists from './modules/discogsArtist.js'

Vue.use(Vuex)

export default new Vuex.Store({
  modules: {
    home,
    discogsArtists
  }
})
