<template>
    <div>
        <table>
            <tr v-for="char in characters">
                <td>{{ char.name}}</td>
                <td><div class="material-icons" @click="editCharacter(char.id)">edit</div></td>
            </tr>
        </table>
    </div>
</template>

<script lang="js">
    import { defineComponent } from 'vue';

    export default defineComponent({
        data() {
            return {
                characters: null,
            };
        },
        created() {
            // fetch the data when the view is created and the data is
            // already being observed
        },
        watch: {
        },
        methods: {
            editCharacter(id) {
                console.log(id);
            }
        },
        mounted() {
            console.log("STARTUP");

            fetch('api/character/all')
                .then(r => r.json())
                .then(json => {
                    console.log(json);
                    this.characters = json;
                });
        }
    });
</script>

<style scoped>
    th {
        font-weight: bold;
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
</style>