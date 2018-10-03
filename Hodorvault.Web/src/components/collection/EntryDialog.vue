<template>
  <div v-if="isVisible" :class="{'is-active': isVisible}" class="modal">
    <div class="modal-background"></div>
    <div class="modal-card">
      <header class="modal-card-head">
        <p class="modal-card-title">Edit entry</p>
        <button @click="$emit('closeDialog')" class="delete" aria-label="close"></button>
      </header>
      <section class="modal-card-body">
        <div class="field">
          <label class="label">Artist</label>
          <div class="control">
            <input v-model="artist.name" class="input" type="text" placeholder="Text input">
          </div>
        </div>
        <div class="field">
          <label class="label">Artist country</label>
          <div class="control">
            <input v-model="artist.country" class="input" type="text" placeholder="Text input">
          </div>
        </div>
        <div class="field">
          <label class="label">Album</label>
          <div class="control">
            <input v-model="album.name" class="input" type="text" placeholder="Text input">
          </div>
        </div>
        <div class="field">
          <label class="label">Released</label>
          <div class="control">
            <input v-model="album.year" class="input" type="text" placeholder="Text input">
          </div>
        </div>
        <div class="field">
          <label class="label">Type</label>
          <div class="control">
            <input v-model="album.type" class="input" type="text" placeholder="Text input">
          </div>
        </div>
        <div class="field">
          <label class="label">Comment</label>
          <div class="control">
            <input v-model="album.comment" class="input" type="text" placeholder="Text input">
          </div>
        </div>
      </section>
      <footer class="modal-card-foot columns">
        <button @click="update" :class="{'is-loading': isUpdating}" class="button is-success">Update</button>
        <button @click="remove" :class="{'is-loading': isRemoving}" class="button is-danger">Remove</button>
        <button @click="$emit('closeDialog')" class="button is-primary">Cancel</button>
      </footer>
    </div>
  </div>
</template>

<script>
const hodorvault = require('../../api/hodorvault.js')

export default {
  name: 'EntryDialog',
  props: {
    artist: { type: Object },
    album: { type: Object },
    isVisible: { type: Boolean }
  },
  data () {
    return {
      isUpdating: false,
      isRemoving: false,
      info: ''
    }
  },
  methods: {
    update: async function () {
      this.$data.isUpdating = true
      try {
        var result = await hodorvault.updateEntry(this.$props.artist, this.$props.album)
        alert(result)
      } catch (e) {
        alert(e)
      }
      this.$data.isUpdating = false
    },
    remove: async function () {
      this.$data.isRemoving = true
      var result = await hodorvault.deleteEntry(this.$props.artist, this.$props.album)
      this.$data.isRemoving = false
      if (result.success) {
        this.$emit('closeDialog')
        console.log(result)
      } else {
        alert(JSON.stringify(result))
      }
    },
    addToCollection: async function () {
      var self = this.$data
      self.isLoading = true
      try {
        await hodorvault.addToCollection(this.$props.artist, this.$props.album)
      } catch (e) {

      }
      self.isLoading = false
      this.$props.isVisible = false
    }
  }
}
</script>

<style>

</style>
