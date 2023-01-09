<template>
    <div class="container">
        <div class="col-12 col-md-12 bg-white m-auto rounded shadow ">
            <div class="text-black text-center mt-5 mb-4 h4">
                <div class="pt-4">
                    Wnioski
                </div>
            </div>   
            <hr>
            <div>
                <b-tabs content-class="mt-3" justified>
                    <b-tab title="Oczekujące" active>
                    <!--  -->
                    <div class="container bg-white p-2 mb-5 text-center">
                        <b-row>

                        <b-col lg="12" class="my-1">
                            <b-row>
                            <b-col lg="6" offset="3"  class="my-1">   
                                <b-form-group
                                label="Filtr"
                                label-for="filter-input"
                                label-cols-sm="3"
                                label-align-sm="right"
                                label-size="sm"
                                class="mb-0"
                                >
                                <b-input-group size="sm">
                                    <b-form-input
                                    id="filter-input"
                                    v-model="filterWaiting"
                                    type="search"
                                    placeholder="Wyszukaj"
                                    ></b-form-input>

                                    <b-input-group-append>
                                    <b-button :disabled="!filterWaiting" @click="filterWaiting = ''">Wyczyść</b-button>
                                    </b-input-group-append>
                                </b-input-group>
                                </b-form-group>
                            </b-col>
                            </b-row>
                        </b-col>

                        </b-row>
                        <b-table
                            class="mt-3"
                            :items="applicationWaiting"
                            :fields="fieldsWaiting"
                            :current-page="currentPageWaiting"
                            :per-page="perPage"
                            :striped="striped"
                            :bordered="bordered"
                            :fixed="fixed"
                            :filter="filterWaiting"
                            :hover="hover"
                            :sort-by.sync="sortByWaiting"
                            :sort-desc.sync="sortDescWaiting"
                            :head-variant="headVariant"
                            empty-text="Brak wniosków"
                            show-empty
                        >  
                            <template #cell(details)="row"> 
                            <b-button-group>
                                <b-button  variant="outline-info" size="sm" @click="DetailsApplication(row.item.id )" >
                                    Szczegóły
                                </b-button>
                                <b-button class="mr-2 ml-2" variant="outline-success" size="sm" @click="AcceptApplication(row.item.id )" >
                                    Akcpetuj
                                </b-button>
                                <b-button  variant="outline-danger" size="sm" @click="RejectApplication(row.item.id )" >
                                    Odrzuć
                                </b-button>
                                </b-button-group>            
                            </template>

                        </b-table>
                        <b-row>
                            <b-col offset="3" lg="6" sm="6" class="my-2">
                                <b-pagination
                                v-model="currentPageWaiting"
                                :total-rows="totalRowsWaiting"
                                :per-page="perPage"
                                align="fill"
                                size="sm"
                                class="my-0"
                                ></b-pagination>
                            </b-col>
                        </b-row>
                        </div>
                    <!--  -->
                    </b-tab>
                    <b-tab title="Zaakceptowane">
                        <div class="container bg-white p-2 mb-5 text-center">
                        <b-row>

                        <b-col lg="12" class="my-1">
                            <b-row>
                            <b-col lg="6" offset="3"  class="my-1">   
                                <b-form-group
                                label="Filtr"
                                label-for="filter-input"
                                label-cols-sm="3"
                                label-align-sm="right"
                                label-size="sm"
                                class="mb-0"
                                >
                                <b-input-group size="sm">
                                    <b-form-input
                                    id="filter-input"
                                    v-model="filterAccept"
                                    type="search"
                                    placeholder="Wyszukaj"
                                    ></b-form-input>

                                    <b-input-group-append>
                                    <b-button :disabled="!filterAccept" @click="filterAccept = ''">Wyczyść</b-button>
                                    </b-input-group-append>
                                </b-input-group>
                                </b-form-group>
                            </b-col>
                            </b-row>
                        </b-col>

                        </b-row>
                        <b-table
                            class="mt-3"
                            :items="applicationAccepted"
                            :fields="fieldsAccepted"
                            :current-page="currentPageAccepted"
                            :per-page="perPage"
                            :striped="striped"
                            :bordered="bordered"
                            :fixed="fixed"
                            :filter="filterAccept"
                            :hover="hover"
                            :sort-by.sync="sortByAccepted"
                            :sort-desc.sync="sortDescAccepted"
                            :head-variant="headVariant"
                            empty-text="Brak wniosków"
                            show-empty
                        >  
                            <template #cell(details)="row"> 
                            <b-button-group>
                                <b-button  variant="outline-info" size="sm" @click="DetailsApplication(row.item.id )" >
                                    Szczegóły
                                </b-button>
                                </b-button-group>            
                            </template>

                        </b-table>
                        <b-row>
                            <b-col offset="3" lg="6" sm="6" class="my-2">
                                <b-pagination
                                v-model="currentPageAccepted"
                                :total-rows="totalRowsAccepted"
                                :per-page="perPage"
                                align="fill"
                                size="sm"
                                class="my-0"
                                ></b-pagination>
                            </b-col>
                        </b-row>
                        </div>
                    </b-tab>
                    <b-tab title="Odrzucone">

                    <div class="container bg-white p-2 mb-5 text-center">
                        <b-row>

                        <b-col lg="12" class="my-1">
                            <b-row>
                            <b-col lg="6" offset="3"  class="my-1">   
                                <b-form-group
                                label="Filtr"
                                label-for="filter-input"
                                label-cols-sm="3"
                                label-align-sm="right"
                                label-size="sm"
                                class="mb-0"
                                >
                                <b-input-group size="sm">
                                    <b-form-input
                                    id="filter-input"
                                    v-model="filterReject"
                                    type="search"
                                    placeholder="Wyszukaj"
                                    ></b-form-input>

                                    <b-input-group-append>
                                    <b-button :disabled="!filterReject" @click="filterReject = ''">Wyczyść</b-button>
                                    </b-input-group-append>
                                </b-input-group>
                                </b-form-group>
                            </b-col>
                            </b-row>
                        </b-col>

                        </b-row>
                        <b-table
                            class="mt-3"
                            :items="applicationRejected"
                            :fields="fieldsRejected"
                            :current-page="currentPageRejected"
                            :per-page="perPage"
                            :striped="striped"
                            :bordered="bordered"
                            :fixed="fixed"
                            :filter="filterReject"
                            :hover="hover"
                            :sort-by.sync="sortByRejected"
                            :sort-desc.sync="sortDescRejected"
                            :head-variant="headVariant"
                            empty-text="Brak wniosków"
                            show-empty
                        >  
                            <template #cell(details)="row"> 
                            <b-button-group>
                                <b-button  variant="outline-info" size="sm" @click="DetailsApplication(row.item.id )" >
                                    Szczegóły
                                </b-button>
                                </b-button-group>            
                            </template>

                        </b-table>
                        <b-row>
                            <b-col offset="3" lg="6" sm="6" class="my-2">
                                <b-pagination
                                v-model="currentPageRejected"
                                :total-rows="totalRowsRejected"
                                :per-page="perPage"
                                align="fill"
                                size="sm"
                                class="my-0"
                                ></b-pagination>
                            </b-col>
                        </b-row>
                        </div>
                    </b-tab>
                </b-tabs>
                <b-modal id="modalApp" ok-variant="outline-primary" v-model="stateModal" ok-only title="Szczegóły wniosku">
                    <p class="my-4">Osoba: <b> {{personName}} </b></p>
                    <p class="my-4">Marka: <b> {{make}} </b></p>
                    <p class="my-4">Model: <b>{{model}}</b></p>
                    <p class="my-4">VIN: <b>{{vin}}</b></p>
                    <p class="my-4">Poj. silnika: <b>{{engineCapacity}}</b></p>
                    <p class="my-4">Dotychczasowy nr rejestracyjny: <b>{{registrationNumber}}</b></p>
                    <p class="my-4">{{messageReason}} <b>{{desiredRegistrationNumber}}</b></p>
                    <p class="my-4">Data złożenia wniosku: <b>{{dateAddedApp}}</b></p>
                    <p class="my-4">Data rozpatrzenia wniosku: <b>{{dateFinishedApp}}</b></p>
                    <p class="my-4">Status wniosku: <b>{{statusApplication}}</b></p>
                    <p v-if="reason != ''" class="my-4">Powód odrzucenia: <b>{{reason}}</b></p>
                </b-modal>

                <b-modal
                    id="modalAccept"
                    ok-title="Akceptuj"
                    ok-variant="outline-success"
                    cancel-title="Anuluj" 
                    cancel-variant="outline-dark"
                    title="Akceptacja wniosku"
                    ref="modal"
                    @show="ResetModal"
                    @hidden="ResetModal"
                    @ok="handleOkModalAccept"
                    >
                    <p class="my-4">Osoba: <b> {{personName}} </b></p>
                    <p class="my-4">Marka: <b> {{make}} </b></p>
                    <p class="my-4">Model: <b>{{model}}</b></p>
                    <p class="my-4">VIN: <b>{{vin}}</b></p>
                    <hr>
                    <form ref="form" @submit.stop.prevent="handleSubmitModalAccept">
                        <b-form-group
                        label="Numer rejestracyjny"
                        label-for="nrRej-input"
                        :invalid-feedback = "errorNbRegistration"
                        :state="desiredRegistrationNumberState"
                        >
                        <b-form-input
                            id="nrRej-input"
                            v-model="desiredRegistrationNumber"
                            :state="desiredRegistrationNumberState"
                            required
                        ></b-form-input>
                        </b-form-group>
                    </form>
                    </b-modal>

                 <b-modal
                    id="modalReject"
                    ok-title="Odrzuć"
                    ok-variant="outline-danger"
                    cancel-title="Anuluj" 
                    cancel-variant="outline-dark"
                    title="Odrzucenie wniosku"
                    ref="modal2"
                    @show="ResetModal"
                    @hidden="ResetModal"
                    @ok="handleOkModalReject"
                    >
                    <p class="my-4">Osoba: <b> {{personName}} </b></p>
                    <p class="my-4">Marka: <b> {{make}} </b></p>
                    <p class="my-4">Model: <b>{{model}}</b></p>
                    <p class="my-4">VIN: <b>{{vin}}</b></p>
                    <hr>
                    <form ref="form" @submit.stop.prevent="handleSubmitModalReject">
                        <b-form-group
                        label="Powód odrzucenia"
                        label-for="reject-input"
                        :invalid-feedback = "errorMessage"
                        :state="messageRejectState"
                        >
                        <b-form-textarea
                            id="reject-input"
                            v-model="messageReject"
                            :state="messageRejectState"
                            required
                        ></b-form-textarea>
                        </b-form-group>
                    </form>
                </b-modal>
            </div>
        </div>
    </div>
</template>
<script >
import axios from 'axios';
import moment from 'moment';

export default{
    name: 'AllAplication',
    data(){
        return{

        // waiting
        applicationWaiting: [],
        totalRowsWaiting: 1,
        currentPageWaiting: 1,
        fieldsWaiting: [],
        filterWaiting: null,
        sortByWaiting: 'make',
        sortDescWaiting: false,
        // accepted
        applicationAccepted: [],
        totalRowsAccepted: 1,
        currentPageAccepted: 1,
        fieldsAccepted: [],
        filterAccept: null,
        sortByAccepted: 'make',
        sortDescAccepted: false,
        // rejected
        applicationRejected: [],
        totalRowsRejected: 1,
        currentPageRejected: 1,
        fieldsRejected: [],
        filterReject: null,
        sortByRejected: 'make',
        sortDescRejected: false,


        fixed: true,
        striped: true,
        bordered: true,
        hover: true,        
        headVariant: 'dark',

        totalRows: 1,
        currentPage: 1,
        perPage: 10,

        //data for modal
        make: '',
        model: '',       
        vin: '',
        registrationNumber: '',
        engineCapacity: '',
        desiredRegistrationNumber: '',
        statusApplication: '',
        dateAddedApp: '',
        dateFinishedApp: '',
        messageRejected: '',
        messageAccepted: '',
        personName: '',
        error: '',

        //modalAccept
        desiredRegistrationNumberState: null,
        errorNbRegistration: '',
        currentIdApplication: null,

        //modalReject
        messageReject: '',
        messageRejectState: null,
        errorMessage: '',

        stateModal: false,
        reason: '',
        messageReason: '',
        }
    },       
    mounted(){
        this.fieldsWaiting = [
            {key: 'user',sortable: true, label: 'Składający'},
            {key: 'make',sortable: true, label: 'Marka'},
            {key: 'dateAdded',sortable: false, label: 'Data złożenia'},
            {key: 'details',sortable: false, label: 'Akcja'},
        ];
        this.fieldsAccepted = [
            {key: 'user',sortable: true, label: 'Składający'},
            {key: 'make',sortable: true, label: 'Marka'},
            {key: 'dateAdded',sortable: false, label: 'Data złożenia'},
            {key: 'dateFinished',sortable: false, label: 'Data akceptacji'},
            {key: 'details',sortable: false, label: 'Szczegóły'},
        ];
        this.fieldsRejected = [
            {key: 'user',sortable: true, label: 'Składający'},
            {key: 'make',sortable: true, label: 'Marka'},
            {key: 'dateAdded',sortable: false, label: 'Data złożenia'},
            {key: 'dateFinished',sortable: false, label: 'Data odrzucenia'},
            {key: 'details',sortable: false, label: 'Szczegóły'},
        ];
       this.LoadData();
    },
    created(){
        //user is not authorized
        if(localStorage.getItem('token') === null || localStorage.getItem('role') !== "Admin"){
            this.$router.push('/');
            this.$fire({
                title: "Odmowa",
                text: "Dostęp tylko dla autoryzowanych użytkowników",
                type: "error",
                })
        }
        
    },
    // watch:{
    //     stateModal: function(value){
    //         if(value==false){
    //             this.messageRejected = '';
    //             this.messageAccepted = '';
    //         }
    //         console.log("watch ",value);
    //     }
    // },
    computed:{
        nbRegistrationLength: function(){
            if(this.desiredRegistrationNumber.length == 0 || this.desiredRegistrationNumber.length == null){
                return 1;
            }
            if(this.desiredRegistrationNumber.length < 3 || this.desiredRegistrationNumber.length > 8 ) {
                return 2;
            }
            else{
                return 3;
            }                 
        },
        nbMessageLength: function(){
            if(this.messageReject.length < 200 &&  this.messageReject.length > 10)
                return false;
            else
                return true;
        },
    },
    methods:{
        LoadData(){
            this.ShowWaiting();
            this.ShowAccepted();
            this.ShowRejected();
        },
        ShowWaiting(){
            axios.get('http://localhost:31000/app/api/Application/waiting',
            {headers: {"Authorization": "Bearer " + localStorage.getItem('token')}
            })
            .then(res => {
                // console.log(res.data);
                this.applicationWaiting = res.data;
                this.totalRowsWaiting = res.data.length;
                for(let i = 0; i < this.totalRowsWaiting; i++){
                this.applicationWaiting[i].dateAdded = this.Format_date(this.applicationWaiting[i].dateAdded);
                }
            })
       }, 
       ShowAccepted(){
            axios.get('http://localhost:31000/app/api/Application/accepted',
            {headers: {"Authorization": "Bearer " + localStorage.getItem('token')}
            })
            .then(res => {
                // console.log(res.data);
                this.applicationAccepted = res.data;
                this.totalRowsAccepted = res.data.length;
                for(let i = 0; i < this.totalRowsAccepted; i++){
                this.applicationAccepted[i].dateAdded = this.Format_date(this.applicationAccepted[i].dateAdded);
                this.applicationAccepted[i].dateFinished = this.Format_date(this.applicationAccepted[i].dateFinished);
                }
            })
       }, 
        ShowRejected(){
            axios.get('http://localhost:31000/app/api/Application/rejected',
            {headers: {"Authorization": "Bearer " + localStorage.getItem('token')}
            })
            .then(res => {
                // console.log(res.data);
                this.applicationRejected = res.data;
                this.totalRowsRejected = res.data.length;
                for(let i = 0; i < this.totalRowsRejected; i++){
                this.applicationRejected[i].dateAdded = this.Format_date(this.applicationRejected[i].dateAdded);
                this.applicationRejected[i].dateFinished = this.Format_date(this.applicationRejected[i].dateFinished);
                }
                // this.applications = this.applicationsSource;
            })
       }, 
        DetailsApplication(id){
            axios.get('http://localhost:31000/app/api/Application/'+id,
            {headers: {"Authorization": "Bearer " + localStorage.getItem('token')}
            })
            .then(res => {
                // console.log(res.data);          
                this.messageReason = "Żądany nr rejestracyjny:";
                this.reason = '';  
                if(res.data.status == 0){
                    this.statusApplication = "Oczekujący";
                }
                else if(res.data.status == 1){
                    this.statusApplication = "Zaakceptowany";
                    // this.messageAccepted = res.data.finalRegistrationNumber;
                    this.messageReason = "Nowy nr rejestracyjny:";
                }
                else{
                    this.statusApplication = "Odrzucony";
                    this.reason = res.data.reasonRejected;
                }
                
                this.personName = res.data.userDto.firstName + ' ' + res.data.userDto.lastName;
                this.make = res.data.vehicle.make;
                this.model = res.data.vehicle.model;       
                this.vin = res.data.vehicle.vin; 
                this.registrationNumber = res.data.vehicle.registrationNumber;
                this.engineCapacity = res.data.vehicle.engineCapacity;
                res.data.finalRegistrationNumber == null? this.desiredRegistrationNumber = "Nie podano" : this.desiredRegistrationNumber = res.data.finalRegistrationNumber;                
                res.data.dateFinished != null? this.dateFinishedApp = this.Format_date(res.data.dateFinished): this.dateFinishedApp = "Nie rozpatrzono";
                this.dateAddedApp = this.Format_date(res.data.dateAdded);

                this.$bvModal.show("modalApp")
                // console.log(this.stateModal);
            })
            // console.log(id);
        },
        AcceptApplication(id){
            axios.get('http://localhost:31000/app/api/Application/'+id,
            {headers: {"Authorization": "Bearer " + localStorage.getItem('token')}
            })
            .then(res => {
                // console.log(res.data);            
                if(res.data.status == 0)
                this.statusApplication = "Oczekujący";
                else if(res.data.status == 1)
                this.statusApplication = "Zaakceptowany";
                else
                this.statusApplication = "Odrzucony";

                this.make = res.data.vehicle.make;
                this.model = res.data.vehicle.model;       
                this.vin = res.data.vehicle.vin; 
                this.personName = res.data.userDto.firstName + ' ' + res.data.userDto.lastName;
                
                this.desiredRegistrationNumber = res.data.finalRegistrationNumber;
                this.$bvModal.show("modalAccept")
                this.currentIdApplication = id;
            })
            // console.log(id);
         },
        RejectApplication(id){
                        axios.get('http://localhost:31000/app/api/Application/'+id,
            {headers: {"Authorization": "Bearer " + localStorage.getItem('token')}
            })
            .then(res => {
                // console.log(res.data);            
                if(res.data.status == 0)
                this.statusApplication = "Oczekujący";
                else if(res.data.status == 1)
                this.statusApplication = "Zaakceptowany";
                else
                this.statusApplication = "Odrzucony";

                this.make = res.data.vehicle.make;
                this.model = res.data.vehicle.model;       
                this.vin = res.data.vehicle.vin; 
                this.personName = res.data.userDto.firstName + ' ' + res.data.userDto.lastName;
                
                this.desiredRegistrationNumber = res.data.finalRegistrationNumber;
                this.$bvModal.show("modalReject");
                this.currentIdApplication = id;
            })
        },

        ResetModal() {
            // this.desiredRegistrationNumber = ''
            this.desiredRegistrationNumberState = null;
            this.messageRejectState = null;

        },
        handleOkModalAccept(bvModalEvent) {
            bvModalEvent.preventDefault()
            this.handleSubmitModalAccept() 
        },
        handleOkModalReject(bvModalEvent) {
            bvModalEvent.preventDefault()
            this.handleSubmitModalReject() 
        },

        handleSubmitModalAccept() {
            // Exit when the form isn't valid
            if (this.nbRegistrationLength == 1) {
                this.desiredRegistrationNumberState = false
                this.errorNbRegistration = "Nr rejestracyjny jest wymagany";
                return
            }
            else if (this.nbRegistrationLength == 2) {
                this.desiredRegistrationNumberState = false
                this.errorNbRegistration = "Nr rejestracyjny musi mieć od 3 do 8 znaków";
                return
            }
            
            this.errorNbRegistration = "";
            // Hide the modal manually
            this.$nextTick(() => {
            let acceptApplication = {
                applicationId: this.currentIdApplication,
                registrationNumber: this.desiredRegistrationNumber,
            }
            axios.put('http://localhost:31000/app/api/Application/accept',acceptApplication, {headers: {
                "Authorization": "Bearer " + localStorage.getItem('token'),
                }} ).then(res => {
                    //if successfull
                    if(res.status === 200){
                        // console.log(res);
                        // localStorage.setItem('alertMessSuccess',"Pomyślnie Dodano.");
                        this.LoadData();
                    }
                    this.error = '';
                },err => {
                    // console.log(err.response);
                    this.error = err.response.data.errors.Name[0];
                  })

            this.$bvModal.hide('modalAccept')
            })
        },
         handleSubmitModalReject() {
            // Exit when the form isn't valid
            if (this.nbMessageLength) {
                this.messageRejectState = false
                this.errorMessage = "Powód musi mieć od 10 do 200 znaków";
                return
            }
            
            this.errorMessage = "";
            // console.log("oky");
            // Hide the modal manually
            this.$nextTick(() => {
            let rejectApplication = {
                applicationId: this.currentIdApplication,
                reason: this.messageReject,
            }
            axios.put('http://localhost:31000/app/api/Application/reject',rejectApplication, {headers: {
                "Authorization": "Bearer " + localStorage.getItem('token'),
                }} ).then(res => {
                    //if successfull
                    if(res.status === 200){
                        // localStorage.setItem('alertMessSuccess',"Pomyślnie Dodano.");
                        this.LoadData();
                    }
                    this.error = '';
                },err => {
                    console.log(err.response);
                    // this.error = err.response.data.errors.Name[0];
                  })

            this.$bvModal.hide('modalReject')
            })
        },
        Format_date(value){
        if (value) {
          return moment(String(value)).add(2,'hours').format('YYYY-MM-DD, H:mm');
        }},
        goBack(){
            this.$router.push('/');
        },
    }
}
</script>
