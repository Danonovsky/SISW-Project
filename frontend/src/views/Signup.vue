<template>
    <div class="container">
        <div class="col-12 col-md-5 bg-white m-auto rounded shadow ">
            <div class="text-black text-center mt-5 mb-4 h4">
                <div class="pt-4">
                    Rejestracja
                </div>
            </div>   
            <hr>
            <div class="p-3 pb-5">
                <label class="text-monospace" for="name" >Imię: </label>
                <b-form-input type="text" id="name" v-model="firstName" placeholder="Podaj imię" required/>
                <label class="text-monospace" for="lName" >Nazwisko: </label>
                <b-form-input type="text" id="lName" v-model="lastName" placeholder="Podaj nazwisko" required/>
                <label class="text-monospace" for="email" >Email: </label>
                <b-form-input type="email" v-model="email" id="email" placeholder="Podaj email" required/>
                <label class="text-monospace" for="name" >Hasło: </label>
                <b-form-input type="password" v-model="password" id="password" placeholder="Podaj hasło" required/>
                <label class="text-monospace" for="confirmPassword" >Powtórz hasło: </label>
                <b-form-input type="password" v-model="confirmPassword" id="confirmPassword" placeholder="Podaj hasło" required/>
                <label class="text-monospace" for="role" >Rola: </label>
                <div class="custom-control custom-switch" id="role">
                    <input type="checkbox" v-model="selectedRole" class="custom-control-input" id="switcher">
                    <label class="custom-control-label" for="switcher" v-if="selectedRole===false"> User </label>
                    <label class="custom-control-label" for="switcher" v-else> Admin </label>
                </div>
                <div class="text-center">
                    <b-button pill @click="signup" class="mt-4" variant="outline-success">Zarejestruj się</b-button>
                </div>  
                <h6 class="text-danger text-center mt-1">
                    {{ error }} <br>
                </h6> 
                <div class="text-center">
                    Masz już konto? <a class="cursor_pointer font-weight-bold" @click="LogUp">Zaloguj się </a><br>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import axios from 'axios';


export default {
    name: 'Signup',
    data(){
        return{
            firstName: '',
            lastName: '',
            email: '',
            password: '',
            confirmPassword: '',
            selectedRole: false, //false - user / true - Admin
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
    methods: {
        signup(){
            this.error = '';
            let newUser = {
                email: this.email,
                password: this.password,
                passwordConfirm: this.confirmPassword,
                firstName: this.firstName,            
                lastName: this.lastName,
                role: this.selectedRole? "Admin" : "User",
            }
            if(this.firstName == "" || this.lastName == "" || this.email == "" || this.password == "" || this.confirmPassword == ""){
                this.error = "Uzupełnij wszystkie pola";
            }
            else if (this.firstName.length < 3) this.error = "Imię za krótkie [min: 3 znaki]";
            else if (this.lastName.length < 3) this.error = "Nazwisko za krótkie [min: 3 znaki]";
            else if (this.email.length < 3) this.error = "Email za krótki [min: 3 znaki]";
            else if (this.password.length < 3) this.error = "Hasło za krótkie [min: 3 znaki]";
            else if (this.confirmPassword.length < 3) this.error = "Hasło za krótkie [min: 3 znaki]";
            else if (this.confirmPassword !== this.password) this.error = "Hasła się różnią";
            else{
                axios.post('https://localhost:5001/identity/api/Account/sign-up',newUser)
                .then(res => {
                    console.log(res);
                    this.$router.push('/login');
                    localStorage.setItem('alertMessSuccess',"Rejestracja przebiegła pomyślnie. ");
                }, err => {
                    console.log("error",err.response);   
                    this.error = err.response.data;                 
                })
            }
           
        },
        LogUp(){
            this.$router.push('/login');
        },

    }
}
</script>
