<template>
    <div class="container">
        <div class="col-12 col-md-5 bg-white m-auto rounded shadow ">
            <div class="text-black text-center mt-5 mb-4 h4">
                <div class="pt-4">
                    Logowanie
                </div>
            </div>   
            <hr>
            <div class="p-3 pb-5">
                <label class="text-monospace" for="email" >Email: </label>
                <b-form-input id="email" type="email" placeholder="Podaj email" v-model="email" />
                <label class="text-monospace" for="email" >Hasło: </label>
                <b-form-input type="password" placeholder="Podaj hasło" v-model="password" />
                <div class="text-center">
                    <b-button pill  @click="login" class="mt-4" variant="outline-success">Zaloguj </b-button><br>
                </div>
                <h6 class="text-danger text-center mt-3"> {{ error }} </h6>
                <br>
                <div class="text-center">
                    Nie masz konta? <a class="cursor_pointer font-weight-bold" @click="signup">Zarejestruj się </a><br>
                </div>
            </div>
    </div>
    </div>
</template>
<script >
import axios from 'axios';
import VueJwtDecode from 'vue-jwt-decode';

export default{
    name: 'Login',
    data(){
        return{
        email: '',
        password: '',

        error: ''
        }
    },
    created(){
    //user is not authorized
    if(localStorage.getItem('token') !== null ){
        this.$router.push('/');
        this.$fire({
            title: "Odmowa",
            text: "Jesteś już zalogowany",
            type: "error",
            })
    }
        
    },
    methods:{
        login(){
            let user = {
                Email: this.email,
                Password: this.password
            }
            if(this.email == "" || this.password == "")
            {
                this.error = "Uzupełnij wszystkie pola!";
            }
            else{
                   this.error = "";
                axios.post('https://localhost:5001/identity/api/Account/sign-in',user)
                .then(res => {
                    //if successfull
                    if(res.status === 200){
                        // console.log(res);
                        localStorage.setItem('token',res.data.token);
                        let tokenDecoded = VueJwtDecode.decode(res.data.token);
                        //ROLA
                        localStorage.setItem('role',tokenDecoded.role);

                        // NAME
                        localStorage.setItem('myName', tokenDecoded.firstName + ' ' + tokenDecoded.lastName);
                        this.$router.push('/');
                        localStorage.setItem('alertMessSuccess','Zalogowano.');
                    }
                    console.log(res);
                    this.error = '';
                },err => {
                    console.log(err.response);
                    this.error = "Brak osoby o takich danych";
                })
            }
        },
        signup(){
        this.$router.push('/signup');
        }
    }
}
</script>
