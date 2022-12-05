<template>
<div>
  <b-navbar toggleable="lg" type="dark" variant="dark">
    <router-link to="/" > <b-navbar-brand>Home </b-navbar-brand></router-link>
    <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

    <b-collapse id="nav-collapse" is-nav>
      
      <b-navbar-nav>
        <!-- <b-nav-item-dropdown class="text-light" text="Dodaj" right v-if="isAdmin"> -->
        <!-- </b-nav-item-dropdown> -->

        <b-nav-item v-if="isAdmin" ><router-link to="/allApplications" class="text-light"> Wnioski </router-link></b-nav-item>
        <b-nav-item v-if="!isAdmin" ><router-link to="/addApplication" class="text-light"> Zarejestruj samochód </router-link></b-nav-item>
        <b-nav-item v-if="!isAdmin" ><router-link to="/myApplication" class="text-light"> Moje wnioski </router-link></b-nav-item>
        
      </b-navbar-nav>

      <!-- Right aligned nav items -->
      <b-navbar-nav class="ml-auto">

        <b-nav-item-dropdown right>
          <!-- Using 'button-content' slot -->
          <template #button-content>
            <em>Witaj: {{myName}} </em>
          </template>
          <!-- <b-dropdown-item > <router-link to="/myProfile" class="text-dark"> Profile </router-link></b-dropdown-item> -->
          <b-dropdown-item @click="logout">Wyloguj się</b-dropdown-item>
        </b-nav-item-dropdown>
      </b-navbar-nav>
    </b-collapse>
  </b-navbar>
</div>
</template>
<script > 
//import axios from 'axios';

export default{
    data(){
        return{
            name: '',
            email: '',
            isAdmin: false,
            myName: '',
            role: '',
        }
    },
    created(){
        //user is not authorized
        this.role = localStorage.getItem('role');
        if (this.role === "Admin"){
            this.isAdmin = true; 
        }
        else {
            this.isAdmin = false; 
        }

        this.myName = localStorage.getItem('myName');

    },
    mounted(){
        // this.name = localStorage.getItem('myLogin');
        // this.email = this.$store.state.mail;
        // console.log(this.$store.state.nick)
    },
    methods: {
        logout(){
            localStorage.clear();
            this.isAdmin = false;
            this.$router.push('/login');
        },
        pokaz(){
          console.log(this.isAdmin, localStorage.getItem('tokenAdmin'));
        }
    }
}
</script>