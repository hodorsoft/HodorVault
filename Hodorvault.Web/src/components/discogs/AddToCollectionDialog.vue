<template>
  <div v-if="artist != null && album != null" :class="{'is-active': isVisible}" class="modal">
    <div class="modal-background"></div>
    <div class="modal-card">
      <header class="modal-card-head">
        <p class="modal-card-title">Add to collection</p>
        <button @click="$emit('closeDialog')" class="delete" aria-label="close"></button>
      </header>
      <section class="modal-card-body">
        <div class="field">
          <label class="label">Artist</label>
          <div class="control">
            <input v-model="artist.title" class="input" type="text" placeholder="Artist/band name">
          </div>
        </div>
        <div class="field">
          <label class="label">Artist country</label>
          <div class="control">
            <input v-model="artist.country" class="input" type="text" placeholder="Country of origin">
          </div>
        </div>
        <div class="field">
          <label class="label">Album</label>
          <div class="control">
            <input v-model="album.title" class="input" type="text" placeholder="Album name">
          </div>
        </div>
        <div class="field">
          <label class="label">Released</label>
          <div class="control">
            <input v-model="album.year" class="input" type="text" placeholder="Release year">
          </div>
        </div>
        <div class="field">
          <label class="label">Type</label>
          <div class="control">
            <input v-model="album.type" class="input" type="text" placeholder="Album type">
          </div>
        </div>
        <div class="field">
          <label class="label">Comment</label>
          <div class="control">
            <input v-model="album.comment" class="input" type="text" placeholder="Comments etc">
          </div>
        </div>
      </section>
      <footer class="modal-card-foot">
        <button @click="addToCollection" :class="{'is-loading': isLoading}" class="button is-success">Add</button>
        <button @click="$emit('closeDialog')" class="button">Cancel</button>
      </footer>
    </div>
  </div>
</template>

<script>
const hodorvault = require('../../api/hodorvault.js')

export default {
  name: 'AddToCollectionDialog',
  props: {
    artist: { type: Object },
    album: { type: Object },
    isVisible: { type: Boolean }
  },
  data () {
    return {
      isLoading: false,
      info: ''
    }
  },
  methods: {
    addToCollection: async function () {
      var self = this.$data
      self.isLoading = true
      try {
        await hodorvault.addToCollection(this.$props.artist, this.$props.album)
      } catch (e) {

      }
      self.isLoading = false
      this.$emit('closeDialog')
    }
  }
}
</script>

<style>

</style>
