<template>
  <div>
    <div class="container">
      {{album.year}} - {{album.title}} <a @click="showDialog = true">add to collection</a>
    </div>
  </div>
</template>

<script>
import AddToCollectionDialog from './AddToCollectionDialog.vue'

export default {
  name: 'DiscogsAlbum',
  props: ['artist', 'album'],
  components: { AddToCollectionDialog },
  data () {
    return {
      isLoading: false,
      info: '',
      showDialog: false,
      albums: []
    }
  },
  methods: {
    // showDialog: async function (artist, album) {
    //   alert('will now show dialog')
    // }
  }
}
</script>

<style>

</style>
