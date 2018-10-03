<template>
  <div>
    <div class="field has-addons">
      <div class="control discogs-search">
        <input
          @keyup.enter="search"
          v-model="artistQuery"
          class="input" type="text" placeholder="search discogs" title="search discogs" />
      </div>
      <div class="control">
        <a @click="search" :class="{ 'is-loading': isLoading }" class="button" >
          <i class="fa fa-search" aria-hidden="true"></i>
        </a>
      </div>
    </div>
    <DiscogsArtist v-for="artist in artists" :key="artist.id" :artist="artist" />
  </div>
</template>

<script>
import DiscogsArtist from './discogs/DiscogsArtist.vue'
import { mapGetters } from 'vuex'
const discogs = require('../api/discogs')

export default {
  name: 'Home',
  components: {
    DiscogsArtist
  },
  data () {
    return {
      isLoading: false,
      artistQuery: null,
      info: ''
    }
  },
  computed: mapGetters({
    artists: 'artists'
  }),
  methods: {
    search: async function (event) {
      var self = this.$data
      self.isLoading = true
      try {
        var searchResults = await discogs.searchArtists(self.artistQuery)
        this.$store.commit('storeArtists', searchResults)
      } catch (e) {
        self.info = 'Something went wrong: ' + e
      }
      self.isLoading = false
    }
  }
}
</script>

<style>
  .discogs-search{
    width: 100%;
  }
</style>
