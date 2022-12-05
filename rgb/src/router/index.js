import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Login from '../views/Login.vue'
import Signup from '../views/Signup.vue';
import AddApplication from '../views/User/AddApplication.vue';
import MyApplication from '../views/User/MyApplication.vue';
import AllApplications from '../views/Admin/AllAppliactions.vue';


Vue.use(VueRouter)

const routes = [
  {path: '/', name: 'Home', component: Home},
  {path: '/login', name: 'Login', component: Login},
  {path: '/signup', name: 'Signup', component: Signup},
  {path: '/addApplication', name: 'AddApplication', component: AddApplication},
  {path: '/myApplication', name: 'MyApplication', component: MyApplication},
  {path: '/allApplications', name: 'AllApplications', component: AllApplications},



  // {
  //   path: '/about',
  //   name: 'About',
  //   // route level code-splitting
  //   // this generates a separate chunk (about.[hash].js) for this route
  //   // which is lazy-loaded when the route is visited.
  //   component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
  // }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
