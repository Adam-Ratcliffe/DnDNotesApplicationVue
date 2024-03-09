<template>
    <div>
        <h2>Name</h2>
        <input v-model="character.name" />
    </div>
</template>

<script lang="js">
import { defineComponent } from "vue";

    export default defineComponent({
        data() {
            return {
            }
        },
        methods: {
            submit() {
                console.log(this.props.character);
                if (this.props.character) {

                }
            },
            updateCharacter() {

            },
            createCharacter() {

            }
        },
        props: ['character']
    });
</script>