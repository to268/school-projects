<script setup>
import Navbar from './components/Navbar.vue';
import Particle from './components/Particle.vue';
import ProfileIcon from './components/ProfileIcon.vue';
import Login from './views/account/Login.vue';
import Register from './views/account/Register.vue';
import { onMounted } from 'vue';



import { useAuthStore } from './JS/Auth.js';

const authStore = useAuthStore();

onMounted(() => {
  authStore.checkAuth();
});

</script>

<template>
  <div v-if="authStore.isAuthenticated" class="flex h-screen w-screen">
    <Navbar />

    <ProfileIcon />
    <Router-view />
  </div>
  <div v-else class="flex h-screen w-screen">
    <RouterView />
  </div>

</template>

<style scoped>
*{color: white;}
</style>
