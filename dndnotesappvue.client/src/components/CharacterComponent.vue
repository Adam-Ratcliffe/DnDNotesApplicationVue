<template>
    <div>
        <div class="cover" v-if="loading" />
        <table v-if="table">
            <tr>
                <th>Name</th>
                <th>Race</th>
                <th></th>
            </tr>
            <tr v-for="char in characters">
                <td>{{ char.name}}</td>
                <td>{{ char.race}} </td>
                <td><div class="material-icons" @click="editCharacter(char.id)">edit</div></td>
            </tr>
            <tr>
                <td colspan="2"></td>
                <td><div class="material-icons" @click="createCharacter">create</div></td>
            </tr>
        </table>
        <div v-if="!table">
            <div class="profile">
                <img v-if="characterModify.profileURL != ''" :src="characterModify.profileURL" />
                <input class="plaque" v-model="characterModify.name" />
            </div>
            <h3>Age</h3>
            <input v-model="characterModify.age" />
            <h3>Race</h3>
            <select v-model="characterModify.race">
                <option>None</option>
                <option v-for="race in races">{{race}}</option>
            </select>
            <h3>Profile URL</h3>
            <input v-model="characterModify.profileURL" />
            <h3>Occupation</h3>
            <textarea rows="4" cols="50" v-model="characterModify.occupation" />
            <h3>History</h3>
            <textarea rows="4" cols="50" v-model="characterModify.history" />
            <h3>Physical Description</h3>
            <textarea rows="4" cols="50" v-model="characterModify.physicalDescription" />
            <h3>Party Attitude</h3>
            <input v-model="characterModify.partyAttitude" />
            <span class="loyalty">
                {{ attitudes[ Math.floor((parseInt(characterModify.partyAttitude) / maxAttitude) * attitudes.length) ] }}
            </span>
            <h3>Allies</h3>
            <div class="listed-characters" v-for="c in characterModify.likedCharacters">
                <p>{{c}}</p>
                <button class="material-icons" @click="removeFromArray(c, characterModify.likedCharacters)">delete</button>
            </div>
            <input v-model="temp" />
            <select v-model="temp">
                <option v-for="n in characterNames">{{ n }}</option>
            </select>
            <button class="material-icons" @click="addLikedCharacter">add_circle</button>
            <h3>Enemies</h3>
            <div class="listed-characters" v-for="c in characterModify.dislikedCharacters">
                <p>{{c}}</p>
                <button class="material-icons" @click="removeFromArray(c, characterModify.dislikedCharacters)">delete</button>
            </div>
            <input v-model="temp" />
            <select v-model="temp">
                <option v-for="n in characterNames">{{ n }}</option>
            </select>
            <button class="material-icons" @click="addDislikedCharacter">add_circle</button>
            <div class="data">
                <button class="material-icons" @click="submit">check</button>
                <button class="material-icons" @click="cancel">cancel</button>
                <button class="material-icons" @click="deleteCharacter(characterModify.id)">delete</button>
            </div>
        </div>
    </div>
</template>

<script lang="js">
    import { defineComponent } from 'vue';

    export default defineComponent({
        data() {
            return {
                characters: null,
                table: true,
                characterModify: {
                    "id": "",
                },
                races: null,
                characterDefaults: {
                    "id": "",
                    "name": "",
                    "age": "",
                    "race": "None",
                    "profileURL": "",
                    "occupation": "",
                    "history": "",
                    "physicalDescription": "",
                    "partyAttitude": "0",
                    "likedCharacters": [],
                    "dislikedCharacters": [],
                },
                loading: false,
                attitudes: [
                    "Disloyal",
                    "Indifferent",
                    "Acquaintance",
                    "Ally",
                    "Loyal",
                    "Passionate",
                    "Devout"
                ],
                temp: "",
                characterNames: [],
                maxAttitude: 20,
            };
        },
        created() {
            // fetch the data when the view is created and the data is
            // already being observed
        },
        watch: {
        },
        methods: {
            submit() {
                this.loading = true;
                var method = "";
                if (this.characterModify.id != this.characterDefaults.id) {
                    // Assumes Updated (PUT)
                    method = "PUT";
                }
                else {
                    // Assumes Create (POST)
                    method = "POST";
                }
                fetch('api/character', {
                    method: method,
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(this.characterModify),
                }).then(this.getAllCharacters());
            },
            cancel() {
                this.characterModify = this.characterDefaults;
                this.table = true;
            },
            editCharacter(id) {
                console.log(id);
                this.table = false;
                fetch('api/character/' + id)
                    .then(r => r.json())
                    .then(json => {
                        console.log(json);
                        if (json.race === null || json.race === undefined) {
                            json.race = this.characterDefaults.race;
                        }
                        this.characterModify = json;
                        this.loading = false;
                    });
            },
            createCharacter() {
                console.log("CREATE NEW");
                this.characterModify = this.characterDefaults;
                this.table = false;
            },
            deleteCharacter(id) {
                console.log("Deleting: " + id);
                if (id != "") {
                    if (confirm("Are you sure you want to delete" + id + "?")) {
                        fetch('api/character/' + id, {
                            method: "DELETE"
                        }).then(this.getAllCharacters());
                    }
                }
                else {
                    alert("Character not found");
                }
            },
            getAllCharacters() {
                fetch('api/character/all')
                    .then(r => r.json())
                    .then(json => {
                        console.log(json);
                        this.characters = json;
                        this.loading = false;
                        this.table = true;
                        this.characterNames = [];
                        Object.entries(json).forEach((entry) => {
                            const [key, value] = entry;
                            this.characterNames.push(value.name);
                        });
                    });
            },
            addLikedCharacter() {
                if (this.temp != "") {
                    this.characterModify.likedCharacters.push(this.temp);
                    this.temp = "";
                }
            },
            addDislikedCharacter() {
                if (this.temp != "") {
                    this.characterModify.dislikedCharacters.push(this.temp);
                    this.temp = "";
                }
            },
            removeFromArray(entry, array) {
                const index = array.indexOf(entry);
                if (index > -1) {
                    array.splice(index, 1);
                }
            }
        },
        mounted() {
            console.log("STARTUP");
            this.characterModify = this.characterDefaults;

            this.getAllCharacters();

            fetch('api/character/races')
                .then(r => r.json())
                .then(json => {
                    console.log(json);
                    this.races = json;
                });
        }
    });
</script>

<style scoped>
    th {
        font-weight: bold;
    }

    tr{
        cursor: pointer;
    }

    tr:nth-child(even) {
        background: #F2F2F2;
    }

    tr:nth-child(odd) {
        background: #FFF;
    }

    th, td {
        padding-left: .5rem;
        padding-right: .5rem;
    }

    .weather-component {
        text-align: center;
    }

    table {
        margin-left: auto;
        margin-right: auto;
    }
    tr:hover{
        background-color: black;
        color: white;
    }

    .cover {
        position: fixed;
        background-color: black;
        opacity: 0.4;
        height: 100%;
        width: 100%;
        top: 0;
        left: 0;
    }

    .profile {
        border: gold solid 10px;
        border-radius: 10% 10% 0 0;
        box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
        position: static;
        overflow: hidden;
        width: 400px;
        background-color: white;
    }

    .profile > img{
        width: 100%;
        height: auto;
    }

    .plaque {
        text-align: center;
        border: none;
        width: 100%;
        padding: 10px 0;
    }

    .data{
        display: flex;
    }

    .data > button{
        width: 100%;
    }

    .loyalty{
        margin-left: 10px;
    }

    .listed-characters{
        display: flex;
    }

    .listed-characters > p{
        width: 100%;
    }

    .listed-characters > button{
        right: 0;
        width: 100px;
    }
</style>