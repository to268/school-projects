<script setup>
import { ref } from 'vue';
import { User } from '../../models/User.js'
import { useRouter } from 'vue-router';
import { useAuthStore } from '../../JS/Auth.js';


const router = useRouter();

const email = ref('');
const password = ref('');

async function login() {
    try {
        const response = await fetch('http://localhost:5000/api/auth', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ email : email.value, password: password.value})
        });

        if (!response.ok) {
            const errorData = await response.json();
            const errorMessage = errorData.message || 'Erreur lors de la connexion.';
            throw new Error(`Erreur ${response.status}: ${errorMessage}`);
        }

        const data = await response.json();
        
        connectionSuccess();

        const authStore = useAuthStore();
        authStore.checkAuth(); // Vérifier l'authentification lors du montage
        authStore.login(data.token);
        
        router.push('/'); 
    } catch (error) {
        connectionFailed('Erreur lors de la connexion: ' + error.message)
        console.error('Erreur lors de la connexion: ', error.message);
    }
}


const Toast = Swal.mixin({
    toast: true,
    position: "top-end",
    showConfirmButton: false,
    timer: 5000,
    timerProgressBar: true,
    didOpen: (toast) => {
      toast.onmouseenter = Swal.stopTimer;
      toast.onmouseleave = Swal.resumeTimer;
    }
  });

function connectionSuccess() {
    Toast.fire({
      title: "Connecté avec succès !",
      icon: "success",
    });
}


function connectionFailed(title) {
    Toast.fire({
      title: title,
      icon: "error",
    });
}

</script>

<template>
    <div class="flex items-center justify-center min-h-screen w-full">
        <div class="w-full max-w-md p-4 space-y-6 bg-white rounded-lg shadow-md">
            <h2 class="text-2xl font-bold text-center text-app">Connexion</h2>
            <form @submit.prevent="login">
                <div class="mb-4 px-3">
                    <label class="block text-sm font-medium text-emerald-700" for="email">Email</label>
                    <input v-model="email" type="email" id="email" placeholder="monmail@gmail.com" required
                        class="block w-full px-4 py-2 mt-1 text-gray-700 bg-gray-200 border rounded-md focus:border-indigo-500 focus:bg-white focus:outline-none" />
                </div>
                <div class="mb-6 px-3">
                    <label class="block text-sm font-medium text-app" for="password">Mot de Passe</label>
                    <input v-model="password" type="password" id="password" placeholder="monmotdepasse" required
                        class="block w-full px-4 py-2 mt-1 text-app bg-gray-200 border rounded-md focus:border-indigo-500 focus:bg-white focus:outline-none" />
                </div>
                <div class="mb-6 px-3">
                    <button type="submit"
                        class="w-full px-4 py-2 text-white bg-app rounded-md  focus:outline-none focus:bg-indigo-700" @click="login">
                        Se Connecter
                    </button>
                </div>
            </form>
            <div class="mt-4 text-center">
                <router-link to="/forgot-password" class="text-app hover:text-emerald-700">Mot de passe oublié
                    ?</router-link>
            </div>
            <div class="mt-2 text-center">
                <router-link to="/register" class="text-app hover:text-emerald-700">Créer un compte</router-link>
            </div>
        </div>
    </div>
</template>