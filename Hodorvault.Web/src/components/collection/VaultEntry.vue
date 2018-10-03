<template>
  <div class="section" id="artist">
    <section class="hero">
      <div class="hero-body">
        <div class="container">
          <h1 class="title">
            {{name}}
          </h1>
          <h2 class="subtitle">
            {{msg}}
          </h2>
        </div>
      </div>
    </section>
  </div>
</template>

<script>
export default {
  name: 'VaultEntry',
  data () {
    return {
      name: 'artistin nimi',
      msg: 'olen yksittäinen artisti'
    }
  }
}
</script>

<style>

</style>
