<template>
    <div>
        <div>
            <button @click="changeComponent(0)">Characters</button>
            <button @click="changeComponent(1)">Locations</button>
        </div>
        <CharacterComponent v-if="component == 0" />
        <LocationComponent v-if="component == 1" />
    </div>
</template>

<script lang="js">
    import { defineComponent } from 'vue';
    import CharacterComponent from './CharacterComponent.vue';
    import LocationComponent from './LocationComponent.vue';

    export default defineComponent({
        data() {
            return {
                loading: false,
                component: 0,
            };
        },
        components: {
            CharacterComponent,
            LocationComponent
        },
        created() {
        },
        methods: {
            changeComponent(i) {
                this.component = i;
            }
        },
    });
</script>