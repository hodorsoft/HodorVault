<template>
  <div class="discogs-artist">
    <div class="level is-marginless">
      <div class="level-left">
        <div class="level-item">
          <div class="media-left">
            <figure v-if="artist.thumb != ''" class="image is-48x48">
              <img :src="artist.thumb">
            </figure>
          </div>
          <div class="level-item">
            <a @click="showAlbums(artist.id)" class="title is-4">{{artist.title}}</a>
          </div>
        </div>
      </div>
      <div class="level-right">
        <div class="level-item">
          <a @click="getInfo(artist.id)" :class="{ 'is-loading': isLoadingInfo }" class="card-footer-item button">Info</a>
        </div>
        <div class="level-item">
          <a @click="showAlbums(artist.id)" :class="{ 'is-loading': isLoadingAlbums }" class="card-footer-item button">Albums</a>
        </div>
      </div>
    </div>
    <div v-show="visibility(artist.id) !== ''" :class="{ 'is-invisible': visibility(artist.id) === '' }">
      <div v-show="visibility(artist.id) === 'info'" class="has-text-centered">
        {{artistInfo(artist.id).profile}}
      </div>
      <ul v-show="visibility(artist.id) === 'albums'" class="albums">
        <li v-for="album in albums(artist.id)" :key="album.id">{{album.year}} - {{album.title}} <a @click="showAddToCollectionDialog(album)">add to collection</a></li>
      </ul>
      <AddToCollectionDialog
        v-show="showDialog"
        :artist="artist"
        :album="album"
        :isVisible="showDialog"
        v-on:closeDialog="showDialog=false" />
    </div>
  </div>
</template>

<script>
// import Vuex from 'vuex'
import AddToCollectionDialog from './AddToCollectionDialog.vue'
import { mapGetters } from 'vuex'

export default {
  name: 'DiscogsArtist',
  props: ['artist'],
  components: { AddToCollectionDialog },
  data () {
    return {
      album: null,
      showDialog: false,
      isLoadingInfo: false,
      isLoadingAlbums: false
    }
  },
  computed: {
    ...mapGetters([
      'albums',
      'artistInfo',
      'visibility'
    ])
  },
  methods: {
    showAddToCollectionDialog: function (album) {
      this.$data.album = album
      this.$data.showDialog = true
    },
    showAlbums: async function () {
      this.$data.isLoadingAlbums = true
      await this.$store.dispatch('getAlbums', this.$props.artist.id)
      this.$data.isLoadingAlbums = false
    },
    showInfo: async function () {
      this.$data.isLoadingInfo = true
      await this.$store.dispatch('getInfo', this.$props.artist.id)
      this.$data.isLoadingInfo = false
    }
  }
}
</script>

<style>
.discogs-artist {
  background-color: #37393c;
  margin-bottom: 15px;
  padding: 10px;
}
.albums {
  margin-top: 10px;
}
</style>
