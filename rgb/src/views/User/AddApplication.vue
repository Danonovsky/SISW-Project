<template>
    <div class="container">
        <div class="col-12 col-md-5 bg-white m-auto rounded shadow ">
            <div class="text-black text-center mt-5 mb-4 h4">
                <div class="pt-4">
                    Wniosek rejestracyjny
                </div>
            </div>   
            <hr>
            <label class="text-monospace" for="make" >Marka: </label>
            <b-form-input id="make" type="text" placeholder="Podaj markę" v-model="make" />

            <label class="text-monospace" for="model" >Model: </label>
            <b-form-input id="model" type="text" placeholder="Podaj model" v-model="model" />
            
            <label class="text-monospace" for="vin" >VIN: </label>
            <b-form-input id="vin" type="text" placeholder="Podaj VIN" v-model="vin" />

            <label class="text-monospace" for="registrationNumber" >Numer rejestracyjny: </label>
            <b-form-input id="registrationNumber" type="text" placeholder="Podaj nr rejestracyjny" v-model="registrationNumber" />

            <label class="text-monospace" for="engineCapacity" >Pojemność silnika [cm<sup>3</sup>]: </label>
            <b-form-input id="engineCapacity" type="number" min="1" max="10000" placeholder="Podaj poj. silnika" v-model="engineCapacity" />
            
            <label class="text-monospace" for="desiredRegistrationNumber" >Żądany numer rejestracyjny:* </label>
            <b-form-input id="desiredRegistrationNumber" type="text" placeholder="Podaj żądany nr rejestracyjny" v-model="desiredRegistrationNumber" />
            
            <h6 class="mt-4">*  Pole opcjonalne</h6>

             <div class="text-center ">
                <b-button pill  @click="save" class="mt-4" variant="outline-success">Zapisz </b-button><br>
                <!-- <b-button pill  @click="goBack()" class="mt-4" variant="outline-warning">Wróc </b-button><br> -->
            </div>
            <h6 class="text-danger text-center mt-1 pb-4"> {{ error }} </h6>   
        </div>
    </div>
</template>
<script >
import axios from 'axios';

export default{
    name: 'AddApplication',
    data(){
        return{
        id: 0,
        make: '',
        model: '',
        vin: '',
        registrationNumber: '',
        desiredRegistrationNumber: null,
        engineCapacity: null,

        error: ''
        }
    },        
    created(){
        //user is not authorized
        if(localStorage.getItem('token') === null){
            this.$router.push('/login');
            this.$fire({
                title: "Odmowa",
                text: "Dostęp tylko dla autoryzowanych użytkowników",
                type: "error",
                })
        }
        
    },
    methods:{
        save(){
            if(this.make == '' || this.model == '' || this.vin == '' || this.registrationNumber == ''){
                this.error = "Uzupełnij wszystkie pola!";
            }
            else if (this.make.length < 2 || this.make.length > 30)
                    this.error = "Marka powinna mieć od 2 do 30 znaków!";
            else if (this.model.length < 2 || this.model.length > 50)
                     this.error = "Model powinien mieć od 2 do 30 znaków!";
            else if (this.vin.length != 1)
                     this.error = "VIN musi mieć 17 znaków!";
            else if (this.engineCapacity < 500 || this.engineCapacity > 100000)
                    this.error = "Pojemność silnika  musi mieścić się od 500 do 100000!";
            else if (this.registrationNumber.length < 3 || this.registrationNumber.length > 8)
                     this.error = "Nr rejestracyjny powinien się składać od 3 do 8 znaków";
            else{
                 let car = {
                    vehicle: {
                    make: this.make,
                    model: this.model,
                    vin: this.vin,
                    registrationNumber: this.registrationNumber,
                    engineCapacity: this.engineCapacity,
                    },
                    desiredRegistrationNumber: this.desiredRegistrationNumber
                }
                axios.post('https://localhost:5001/app/api/Application/add',car, {headers: {
                    "Authorization": "Bearer " + localStorage.getItem('token'),
                    }} ).then(res => {
                        //if successfull
                        if(res.status === 200){
                            console.log(res);
                            this.$router.push('/myApplication');
                            localStorage.setItem('alertMessSuccess',"Pomyślnie Dodano.");
                        }
                        console.log(res);
                        this.error = '';
                    },err => {
                        console.log(err.response);
                    })
            }
        },
        goBack(){
            this.$router.push('/');
        },
    }
}
</script>
