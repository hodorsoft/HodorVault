import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/components/Home'
import Collection from '@/components/collection/Collection'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Home',
      component: Home
    },
    {
      path: '/collection',
      name: 'Collection',
      component: Collection
    }
  ],
  linkExactActiveClass: 'navbar-active-link'
})
