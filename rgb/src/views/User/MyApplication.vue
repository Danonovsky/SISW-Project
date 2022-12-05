<template>
<div class="container-fluid">
    <div class="text-white text-center mt-2 mb-4 h2">
        Moje wnioski
    </div>
    <div class="container bg-white p-2 mb-5 text-center">
     <b-row>

      <b-col lg="12" class="my-1">
        <b-row>
          <b-col lg="6"  class="my-1">   
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
                  v-model="filter"
                  type="search"
                  placeholder="Wyszukaj"
                ></b-form-input>

                <b-input-group-append>
                  <b-button :disabled="!filter" @click="filter = ''">Wyczyść</b-button>
                </b-input-group-append>
              </b-input-group>
            </b-form-group>
          </b-col>
          <b-col lg="6" class="my-1">
            <b-form-group
              label="Status wniosku"
              description=""
              label-cols-sm="3"
              label-align-sm="right"
              label-size="sm"
              class="mb-0"
              v-slot="{ ariaDescribedby }"
            >
              <b-form-checkbox-group
                v-model="filterOn"
                :aria-describedby="ariaDescribedby"
                class="mt-1"
              >
                <b-form-checkbox value="Oczekujący">Oczekujący</b-form-checkbox>
                <b-form-checkbox value="Zaakceptowany">Zaakceptowany</b-form-checkbox>
                <b-form-checkbox value="Odrzucony">Odrzucony</b-form-checkbox>
              </b-form-checkbox-group>
            </b-form-group>
          </b-col>
        </b-row>
      </b-col>

     </b-row>
      <b-table
        class="mt-3"
        :items="applications"
        :fields="fields"
        :current-page="currentPage"
        :per-page="perPage"
        :striped="striped"
        :bordered="bordered"
        :fixed="fixed"
        :filter="filter"
        :hover="hover"
        :sort-by.sync="sortBy"
        :sort-desc.sync="sortDesc"
        :head-variant="headVariant"
        empty-text="Brak wniosków"
        show-empty
      >  
<!-- 
        <template v-if="isAdmin" #cell(edit)="row"> 
            <b-button-group>
              <b-button  variant="outline-warning" size="sm" @click="EditCountry( row.item.Id )" >
                Edytuj            
              </b-button>
              <b-button  variant="outline-info" size="sm" @click="DetailsCountry( row.item.Id )" >
                Szczegóły
              </b-button>
              <b-button  variant="outline-danger" size="sm" @click="ShowMsgBox( row.item.Id )" >
                Usuń
              </b-button>
            </b-button-group>            
        </template> -->

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
              v-model="currentPage"
              :total-rows="totalRows"
              :per-page="perPage"
              align="fill"
              size="sm"
              class="my-0"
            ></b-pagination>
          </b-col>
      </b-row>
      <b-row>
        <b-col offset="5" lg="2" sm="2" class="my-4">  
            <b-button class="btn-block" variant="outline-success" size="sm" @click="AddApplication">
                  Złóż wniosek!
            </b-button>
          </b-col>
      </b-row>
    </div>
     <b-modal id="modalApp" ok-variant="outline-primary" ok-only title="Szczegóły wniosku">
        <p class="my-4">Marka: <b> {{make}} </b></p>
        <p class="my-4">Model: <b>{{model}}</b></p>
        <p class="my-4">VIN: <b>{{vin}}</b></p>
        <p class="my-4">Poj. silnika [cm<sup>3</sup>]: <b>{{engineCapacity}}</b></p>
        <p class="my-4">Dotychczasowy nr rejestracyjny: <b>{{registrationNumber}}</b></p>
        <p class="my-4">{{messageReason}} <b>{{desiredRegistrationNumber}}</b></p>
        <p class="my-4">Data złożenia wniosku: <b>{{dateAddedApp}}</b></p>
        <p class="my-4">Data rozpatrzenia wniosku: <b>{{dateFinishedApp}}</b></p>
        <p class="my-4">Status wniosku: <b>{{statusApplication}}</b></p>
        <p v-if="reason != ''" class="my-4">Powód odrzucenia: <b>{{reason}}</b></p>
    </b-modal>
</div>
</template>
<script >
import axios from 'axios';
import moment from 'moment';


export default{
    name: 'MyAplication',
    data(){
        return{
        fields: [

        ],
        applications: [],
        applicationsSource: [],
        fixed: true,
        striped: true,
        bordered: true,
        hover: true,        
        filter: null,
        sortBrand: true,
        headVariant: 'dark',
        sortBy: 'make',
        sortDesc: false,
        totalRows: 1,
        currentPage: 1,
        pageOptions: [3, 10, 15, { value: 100, text: "100" }],
        perPage: 10,
        filterOn: ["Oczekujący","Zaakceptowany","Odrzucony"],
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
        error: '',

        reason: '',
        messageReason: '',

        }
    },
    watch:{
      filterOn: function(value){
          this.applications = this.applicationsSource.filter(x => {
            return value.includes(x.status.toString())
          });
      }
    },
    created(){
        // user is not authorized
        if(localStorage.getItem('token') === null){
            this.$router.push('/login');
            this.$fire({
                title: "Odmowa",
                text: "Dostęp tylko dla autoryzowanych użytkowników",
                type: "error",
                })
        }
    },
    mounted(){
        this.fields = [
            {key: 'make',sortable: true, label: 'Marka'},
            {key: 'model',sortable: true, label: 'Model'},
            {key: 'status',sortable: true, label: 'Status'},
            {key: 'dateAdded',sortable: false, label: 'Data złożenia'},
            {key: 'details',sortable: false, label: 'Szczegóły'},
        ];
        this.ShowAll();
    },
    methods:{
      ShowAll(){
        axios.get('https://localhost:5001/app/api/Application',
        {headers: {"Authorization": "Bearer " + localStorage.getItem('token')}
        })
        .then(res => {
            // console.log(res.data);
            this.applicationsSource = res.data;
            this.totalRows = res.data.length;
            for(let i = 0; i < this.totalRows; i++){
              this.applicationsSource[i].dateAdded = this.Format_date(this.applicationsSource[i].dateAdded);
              
              if(this.applicationsSource[i].status == 0)
                this.applicationsSource[i].status  = "Oczekujący";

              else if(this.applicationsSource[i].status  == 1)
                this.applicationsSource[i].status  = "Zaakceptowany"; 

              else 
                this.applicationsSource[i].status  = "Odrzucony";

            }
            this.applications = this.applicationsSource;
        })
       },
      AddApplication(){
        this.$router.push('/addApplication');
      },
      DetailsApplication(id){
        axios.get('https://localhost:5001/app/api/Application/'+id,
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
              this.messageReason = "Nowy nr rejestracyjny:";
            }
            else{
              this.statusApplication = "Odrzucony";
              this.reason = res.data.reasonRejected;
            }
            
            
            this.make = res.data.vehicle.make;
            this.model = res.data.vehicle.model;       
            this.vin = res.data.vehicle.vin; 
            this.registrationNumber = res.data.vehicle.registrationNumber;
            this.engineCapacity = res.data.vehicle.engineCapacity;
            this.dateAddedApp = this.Format_date(res.data.dateAdded);
            res.data.finalRegistrationNumber == null? this.desiredRegistrationNumber = "Nie podano" : this.desiredRegistrationNumber = res.data.finalRegistrationNumber;                
            res.data.dateFinished != null? this.dateFinishedApp = this.Format_date(res.data.dateFinished): this.dateFinishedApp = "Nie rozpatrzono"; 
            this.$bvModal.show("modalApp")
        })
        console.log(id);
      },
      Format_date(value){
        if (value) {
          return moment(String(value)).add(2,'hours').format('YYYY-MM-DD, H:mm');
        }},
    }
}
</script>


