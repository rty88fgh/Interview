<template>
    <div>
        <h1 id="tableLabel">Country</h1>

        <div class="searchDiv">
        <label for="fname">Country:</label>
            <input type="text" v-model.trim="searchStr" @input="() => renderPageData()">
        </div>

        <p v-if="!allCountries"><em>Loading...</em></p>
        <table class='table table-striped' aria-labelledby="tableLabel" v-if="allCountries">
            <thead>
                <tr>
                    <th></th>
                    <th>Flag</th>
                    <th>Name</th>
                    <th>Alpha 2 Code</th>
                    <th>Alpha 3 Code</th>
                    <th>Native Name</th>
                    <th>Alt Spellings</th>
                    <th>Calling Codes</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="country of showCountries" v-bind:key="country">
                    <td> <a href="#"
                            class="btn btn-primary"
                            data-toggle="modal"
                            data-target=".bd-example-modal-lg">Selected</a></td>
                    <td><input type="image" v-bind:src="country.flag" style="display:table-cell;max-width:50px;" /></td>
                    <td>{{ country.name }}</td>
                    <td>{{ country.alpha2Code }}</td>
                    <td>{{ country.alpha3Code }}</td>
                    <td>{{ country.nativeName }}</td>
                    <td v-if="country.altSpellings.count <=1">{{ country.altSpellings[0] }}</td>
                    <td v-else>{{ country.altSpellings.join(",") }}</td>
                    <td v-if="country.callingCodes.count <=1">{{ country.callingCodes[0] }}</td>
                    <td v-else>{{ country.callingCodes.join(",") }}</td>
                </tr>
            </tbody>
        </table>
        <nav aria-label="Page navigation">
            <ul class="pagination">
                <li :class=" page == 1 ? 'page-item disabled' : 'page-item' ">
                    <a class="page-link" href="#" tabindex="-1" @click="() => onPageClick(page -1)">Previous</a>
                </li>
                
                <li v-if="page <= 2" :class="page == 1 ? 'page-item active' : 'page-item'" >
                    <a class="page-link" href="#" @click="() => onPageClick(1)">1<span v-if="page == 1" class="sr-only">(current)</span></a>
                </li>
                <li v-else-if="page == lastPage" :class="page == lastPage -2  ? 'page-item active' : 'page-item'" >
                    <a class="page-link" href="#" @click="() => onPageClick(lastPage - 2)">{{ lastPage -2 }}</a>
                </li>
                <li v-else class="page-item">
                    <a class="page-link" href="#" @click="() => onPageClick(page - 1)">{{ page -1 }}</a>
                </li>

                <!--second-->
                <li v-if="lastPage >=2 && page <= 2" :class="page == 2 ? 'page-item active' : 'page-item'" >
                    <a class="page-link" href="#" @click="() => onPageClick(2)">2<span v-if="page == 2" class="sr-only">(current)</span></a>
                </li>
                <li v-else-if="lastPage >=2 && (page != 2 && page == lastPage)" class="page-item" >
                    <a class="page-link" href="#" @click="() => onPageClick(lastPage - 1)">{{ lastPage -1 }}</a>
                </li>
                <li v-else-if="lastPage >=2" class="page-item active">
                    <a class="page-link" href="#" @click="() => onPageClick(page)">{{ page }}<span class="sr-only">(current)</span></a>
                </li>

                <!--third-->
                <li v-if="lastPage >=3 && page <= 2" :class="page == 3 ? 'page-item active' : 'page-item'" >
                    <a class="page-link" href="#" @click="() => onPageClick(3)">3<span v-if="page == 3" class="sr-only">(current)</span></a>
                </li>
                <li v-else-if="lastPage >=3 && (page != 3 && page == lastPage)" class="page-item active" >
                    <a class="page-link" href="#" @click="() => onPageClick(lastPage)">{{ lastPage }}<span class="sr-only">(current)</span></a>
                </li>
                <li v-else-if="lastPage >=3" class="page-item">
                    <a class="page-link" href="#" @click="() => onPageClick(page + 1 )">{{ page + 1}}</a>
                </li>

                <!--Next-->
                <li :class=" page == lastPage ? 'page-item disabled' : 'page-item' ">
                    <a class="page-link" href="#" tabindex="-1" @click="() => onPageClick(page + 1)">Next</a>
                </li>
            </ul>
        </nav>
    </div>
</template>


<script>
    import axios from 'axios'
    export default {
        name: "FetchData",
        data() {
            return {
                allCountries: [],
                showCountries:[],
                lastPage:Number,
                pageSize:25,
                searchStr:"",
                page:1                
            }
        },
        methods: {
            getAllCountry() {
                axios.get('https://restcountries.eu/rest/v2/all')
                    .then((response) => {
                        this.allCountries =  response.data;
                        this.renderPageData();
                    })
                    .catch(function (error) {
                        alert(error);
                    });
            },
            renderPageData(){
                var searchItems = this.allCountries;
                if(this.searchStr != undefined && this.searchStr.trim() == this.searchStr)
                    searchItems = searchItems.filter(country => country.name.toLowerCase().includes(this.searchStr.toLowerCase()));

                console.log("================");
                var startItemToGet = (this.page - 1) * this.pageSize;
                this.showCountries = searchItems.slice(startItemToGet,startItemToGet + this.pageSize);
                Math.ceil(searchItems.length / this.pageSize);
                this.lastPage = Math.ceil(searchItems.length / this.pageSize);
            },           
            onPageClick(clickPage){
                console.log(clickPage);
                this.page = clickPage;
                this.renderPageData();
            }
        },
        counputed:{

        },
        mounted() {
            this.getAllCountry();
        }
    }
</script>
