<template>
  <div id="app">
    <nav class="navbar is-fixed-top" role="navigation" aria-label="main navigation">
      <div class="navbar-brand">
        <router-link tag="a" to="/" class="navbar-item">
          /home
        </router-link>
        <router-link tag="a" to="/collection" class="navbar-item">
          /collection
        </router-link>
      </div>
    </nav>
    <div class="section" id="main-content">
      <router-view/>
    </div>
  </div>
</template>

<script>
import store from './store'
export default {
  name: 'App',
  store
}
</script>

<style>
/*
http://ethanschoonover.com/solarized
$base03:    #002b36;
$base02:    #073642;
$base01:    #586e75;
$base00:    #657b83;
$base0:     #839496;
$base1:     #93a1a1;
$base2:     #eee8d5;
$base3:     #fdf6e3;
$yellow:    #b58900;
$orange:    #cb4b16;
$red:       #dc322f;
$magenta:   #d33682;
$violet:    #6c71c4;
$blue:      #268bd2;
$cyan:      #2aa198;
$green:     #859900;
*/
  a,
  a.navbar-item{
    color: #2aa198;
  }
  a:hover,
  a.navbar-item:hover,
  a.navbar-item.navbar-active-link{
    color: #f2cd5a;
  }
  #main-content { margin-top: 40px; }
  input.input {
    background: linear-gradient(#222, #2f2f2f);
    color: #bcbcbc;
  }
</style>
