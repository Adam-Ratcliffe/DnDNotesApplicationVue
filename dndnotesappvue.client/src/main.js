import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import ModifyCharacter from './components/ModifyCharacter.vue';

const app = createApp(App);

app.component('CharacterComponent', ModifyCharacter);

app.mount('#app');
