import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import VueJwtDecode from 'vue-jwt-decode'

Vue.use(VueJwtDecode)

import BootstrapVue from 'bootstrap-vue';
Vue.use(BootstrapVue)
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue.css';

import VueSimpleAlert from "vue-simple-alert";
Vue.use(VueSimpleAlert);

import '@/assets/style.css';
Vue.config.productionTip = false

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
