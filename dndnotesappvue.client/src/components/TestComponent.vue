<template>
    <div>
        <input v-model="text" />
        <button @click="postToTest">POST</button>
        <button @click="getToTest">GET</button>
    </div>
</template>

<script lang="js">
    import { defineComponent } from 'vue';

    export default defineComponent({
        data() {
            return {
                loading: false,
                post: null,
                text: null
            };
        },
        created() {
            // fetch the data when the view is created and the data is
            // already being observed
        },
        watch: {
            // call again the method if the route changes
            '$route': 'fetchData'
        },
        methods: {
            postToTest() {
                var b = { "id": 0, "name": "Test text" };
                console.log(b);
                console.log(JSON.stringify(b));

                fetch('/api/notes', {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify(b),
                });
            },
            getToTest() {
                fetch('/api/notes/1')
                    .then(r => r.json())
                    .then(json => {
                        console.log(json);
                    });
            },
        },
        mounted() {
            console.log("STARTUP");

            fetch('api/notes/getall')
                .then(r => r.json())
                .then(json => {
                    console.log(json)
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
</style>