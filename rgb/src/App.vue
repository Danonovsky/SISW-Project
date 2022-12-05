<template>
    <div class="MainDiv"> 
        <div :is="NavLogged">
        
        </div>
        <b-alert v-model="showDismissibleAlert" variant="danger"  dismissible></b-alert>
        <b-alert v-model="dismissCountDown" dismissible variant="success">
          <h4 id="alertSuccess"> </h4>
          <b-progress variant="success" :max="dismissSecs" :value="dismissCountDown" height="7px"></b-progress>
        </b-alert>
        <router-view> </router-view>
      </div>
</template>
<script>  
import LoggedNav from './views/LoggedNav.vue';
import UnLoggedNav from './views/UnLoggedNav.vue';
export default{
  name: 'App',
  data(){
    return{
      NavLogged: '',
      showDismissibleAlert:false,
      dismissCountDown: 0,
      dismissSecs: 5,
      alertMessSu: '',
    }
  },
  updated(){
       this.Logged();
      this.AlertM();
    },
    created(){
      this.Logged();
  },
  methods:{
      
      Logged(){
          if(localStorage.getItem('token') === null){
              this.NavLogged = UnLoggedNav;
          }
          else{
              this.NavLogged = LoggedNav;
          }
      },
          AlertM(){
            this.alertMessSu = localStorage.getItem('alertMessSuccess');
            if(this.alertMessSu != null)
            {
                this.showAlert()
                //  this.showDismissibleAlert = true;
                this.$nextTick(() => {
                    document.getElementById('alertSuccess').innerHTML = this.alertMessSu;
                   localStorage.removeItem('alertMessSuccess'); 
                   
               });                
                             
            } 
        },
        showAlert() { 
            this.dismissCountDown = this.dismissSecs;
        },

    }
}
</script>
<style>
/* #app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #42b983;
} */
</style>
