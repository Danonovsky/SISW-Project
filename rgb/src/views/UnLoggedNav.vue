<template>
<div>
  <b-navbar toggleable="lg" type="dark" variant="dark">
    <b-navbar-brand ><router-link to="/"  class="text-light" >Home </router-link></b-navbar-brand>

    <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

    <b-collapse id="nav-collapse" is-nav>

      <!-- Right aligned nav items -->
      <b-navbar-nav class="ml-auto">

      <b-navbar-nav >
        <b-nav-item> <router-link to="/login" class="text-light"> Logowanie </router-link></b-nav-item>
        <b-nav-item> <router-link to="/signup" class="text-light"> Rejestracja </router-link></b-nav-item>
        
      </b-navbar-nav>
      </b-navbar-nav>
    </b-collapse>
  </b-navbar>
</div>
</template>
