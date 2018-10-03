<template>
  <div>

    <EntryDialog
          v-show="showDialog"
          :artist="artist"
          :album="album"
          :isVisible="showDialog"
          v-on:closeDialog="closeDialog"
          v-on:entryRemoved="entryRemoved" />

    <div id="collectionStats" class="level is-mobile">
      <div class="level-item has-text-centered">
        <div>
          <p class="heading">Albums</p>
          <p class="title">{{differentAlbums}}</p>
        </div>
      </div>
      <div class="level-item has-text-centered">
        <div>
          <p class="heading">Different artists</p>
          <p class="title">{{artists.length}}</p>
        </div>
      </div>
      <div class="level-item has-text-centered">
        <div>
          <p class="heading">Different countries</p>
          <p class="title">{{differentCountries}}</p>
        </div>
      </div>
    </div>

    <table id="collection" class="table is-fullwidth is-bordered is-hoverable">
      <thead>
        <tr>
          <th>Artist</th>
          <th>Country</th>
          <th>Album</th>
          <th>Type</th>
          <th>Released</th>
        </tr>
      </thead>
      <tbody v-for="artist in artists" :key="artist.id">
        <tr v-if="artist.albums.length === 0" @click="editEntry(artist)">
          <td>{{artist.name}}</td>
          <td>{{artist.country}}</td>
          <td></td>
          <td></td>
          <td></td>
        </tr>
        <tr v-if="artist.albums.length > 0" v-for="album in artist.albums" :key="album.id" @click="editEntry(artist, album)">
          <td>{{artist.name}}</td>
          <td>{{artist.country}}</td>
          <td>{{album.name}}</td>
          <td>{{album.type}}</td>
          <td>{{album.year}}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import EntryDialog from './EntryDialog.vue'
const hodorvault = require('../../api/hodorvault.js')
const _ = require('underscore')

export default {
  name: 'Collection',
  components: { EntryDialog },
  data () {
    return {
      artists: [],
      artist: {},
      album: {},
      showDialog: false
    }
  },
  computed: {
    differentAlbums: function () {
      // sum of albums
      return _.chain(this.$data.artists)
        .pluck('albums')
        .reduce(function (sum, albums) { return sum + albums.length }, 0)
        .value()
    },
    differentCountries: function () {
      // different countries (null not counted)
      return _.chain(this.$data.artists)
        .pluck('country')
        .reject(function (country) { return country == null })
        .uniq()
        .size()
        .value()
    }
  },
  async created () {
    this.$data.artists = await hodorvault.getCollection()
  },
  methods: {
    editEntry: async function (artist, album) {
      this.$data.artist = artist
      this.$data.album = album === undefined ? {} : album
      this.$data.showDialog = true
    },
    closeDialog: function () {
      this.$data.showDialog = false
      this.$data.artist = null
      this.$data.album = null
    },
    entryRemoved: function () {
      // this.$data.artists = _.without(
      //   this.$data.artists,
      //   _.findWhere(
      //     this.$data.artists,
      //     )
      // )
      // var a = this.$data.artist
      // var r = this.$data.album
      // this.$data.artists.forEach(function (j) {
      //   if (j.id === a.id) {
      //     if (r) {
      //     }
      //   }
      // })
    }
  }
}
</script>

<style>
  #collectionStats.level .level-item .title { color: #4dc9b4; }
  #collection tbody tr:hover { cursor: pointer; }
  table.table thead th { color: #d4b44f; font-weight: normal; text-transform: uppercase; }
</style>
