<script setup>
import { ref, onMounted } from "vue";
// import { user } from '../models/user.js';
import { User } from "../models/User.js";
import { useAuthStore } from "../JS/Auth.js";

const authStore = useAuthStore();

const user = ref(new User());
const userId = ref(null);

const fetchUser = async () => {
    try {
      const response = await authStore.fetchWithAuth(
        "http://localhost:5000/api/users/me",
        {
          method: "GET",
          headers: {
            "Content-type": "application/json",
          },
        }
      );

      if (!response.ok) {
        const errorData = await response.json();
        const errorMessage =
          errorData.message || "Erreur lors de la récupération de l'utilisateur.";
        throw new Error(`Erreur ${response.status}: ${errorMessage}`);
      }

      let userData = await response.json();
      userId.value = userData._id;
      user.value = new User(userData);
    } catch (error) {
      console.error(
        "Erreur lors de la récupération de l'utilisateur:",
        error.message
      );
    }
  };

onMounted(() => {

  fetchUser()
});

async function saveProfile() {
  try {
    console.log(user.value._id)
    console.log(user.value)
      const response = await authStore.fetchWithAuth(
        "http://localhost:5000/api/users/save",
        {
          method: "POST",
          headers: {
            "Content-type": "application/json",
          },
          body: JSON.stringify({user: user.value.toJson(), id: userId.value})
        }
      );

      if (!response.ok) {
        const errorData = await response.json();
        const errorMessage =
          errorData.message || "Erreur lors de la sauvegarde de l'utilisateur.";
        saveUserFailed()
        throw new Error(`Erreur ${response.status}: ${errorMessage}`);
      }

      saveUserSuccess()
    } catch (error) {
      console.error(
        "Erreur lors de la sauvegarde de l'utilisateur:",
        error.message
      );
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
  },
});

function saveUserSuccess() {
  Toast.fire({
    title: "User enregistré avec succès !",
    icon: "success",
  });
}

function saveUserFailed() {
  Toast.fire({
    title: "Echec de l'enregistrement du user !",
    icon: "error",
  });
}
</script>

<template>
  <div class="w-full h-screen overflow-y-auto h-screen p-8">
    <div class="w-full p-4 space-y-6 bg-white rounded-lg shadow-md h-full">
      <h2 class="text-2xl font-bold text-center text-app">Profil</h2>
      <form @submit.prevent="saveProfile" class="h-5/6  ">
        <div class="overflow-y-auto h-full">
          <!-- Informations Générales -->
          <div class="flex flex-wrap mb-4">
            <div class="w-full mb-4 md:w-1/2 px-3">

              <label class="block text-sm font-medium text-app" for="firstName">Prénom</label>
              <input v-model="user.firstName" type="text" id="firstName" placeholder="Samuel"
                class="block w-full px-4 py-2 mt-1 text-app bg-gray-200 border rounded-md focus:border-indigo-500 focus:bg-white focus:outline-none" />
            </div>
            <div class="w-full mb-4 md:w-1/2 px-3">
              <label class="block text-sm font-medium text-app" for="lastName">Nom</label>
              <input v-model="user.lastName" type="text" id="lastName" placeholder="Pochat"
                class="block w-full px-4 py-2 mt-1 text-app bg-gray-200 border rounded-md focus:border-indigo-500 focus:bg-white focus:outline-none" />
            </div>
            <div class="w-full mb-4 px-3">
              <label class="block text-sm font-medium text-app" for="email">Email</label>
              <input v-model="user.email" type="email" id="email" placeholder="monmail@gmail.com"
                class="block w-full px-4 py-2 mt-1 text-app bg-gray-200 border rounded-md focus:border-indigo-500 focus:bg-white focus:outline-none" />
            </div>
            <div class="w-full mb-4 px-3">
              <label class="block text-sm font-medium text-app" for="phoneMobile">Téléphone Mobile</label>
              <input v-model="user.phone.mobile" type="text" id="phoneMobile" placeholder="06 12 34 56 78"
                class="block w-full px-4 py-2 mt-1 text-app bg-gray-200 border rounded-md focus:border-indigo-500 focus:bg-white focus:outline-none" />
            </div>
            <div class="w-full mb-4 px-3">
              <label class="block text-sm font-medium text-app" for="phoneLandline">Téléphone Fixe</label>
              <input v-model="user.phone.landline" type="text" id="phoneLandline" placeholder="01 23 45 67 89"
                class="block w-full px-4 py-2 mt-1 text-app bg-gray-200 border rounded-md focus:border-indigo-500 focus:bg-white focus:outline-none" />
            </div>
          </div>

          <!-- Informations d'Adresse -->
          <div class="flex flex-wrap mb-4">
            <div class="w-full mb-4 px-3">
              <label class="block text-sm font-medium text-app" for="addressRoad">Adresse</label>
              <input v-model="user.address.road" type="text" id="addressRoad" placeholder="123 Rue Exemple"
                class="block w-full px-4 py-2 mt-1 text-app bg-gray-200 border rounded-md focus:border-indigo-500 focus:bg-white focus:outline-none" />
            </div>
            <div class="w-full mb-4 md:w-1/2 px-3">
              <label class="block text-sm font-medium text-app" for="addressCity">Ville</label>
              <input v-model="user.address.city" type="text" id="addressCity" placeholder="Paris"
                class="block w-full px-4 py-2 mt-1 text-app bg-gray-200 border rounded-md focus:border-indigo-500 focus:bg-white focus:outline-none" />
            </div>
            <div class="w-full mb-4 md:w-1/2 px-3">
              <label class="block text-sm font-medium text-app" for="addressPostalCode">Code Postal</label>
              <input v-model="user.address.postalCode" type="text" id="addressPostalCode" placeholder="75001"
                class="block w-full px-4 py-2 mt-1 text-app bg-gray-200 border rounded-md focus:border-indigo-500 focus:bg-white focus:outline-none" />
            </div>
            <div class="w-full mb-4 px-3">
              <label class="block text-sm font-medium text-app" for="addressState">État</label>
              <input v-model="user.address.state" type="text" id="addressState" placeholder="Île-de-France"
                class="block w-full px-4 py-2 mt-1 text-app bg-gray-200 border rounded-md focus:border-indigo-500 focus:bg-white focus:outline-none" />
            </div>
          </div>

          <!-- Informations Professionnelles Médecin -->
          <div v-if="user.role === 'doctor'">
            <div class="w-full mb-4 px-3">
              <label class="block text-sm font-medium text-app" for="mainSpecialty">Spécialité
                Principale</label>
              <input v-model="user.mainSpecialty" type="text" id="mainSpecialty" placeholder="Cardiologie"
                class="block w-full px-4 py-2 mt-1 text-app bg-gray-200 border rounded-md focus:border-indigo-500 focus:bg-white focus:outline-none" />
            </div>
            <div class="w-full mb-4 px-3">
              <label class="block text-sm font-medium text-app" for="otherSpecialties">Autres
                Spécialités</label>
              <input v-model="user.otherSpecialties" type="text" id="otherSpecialties" placeholder="Dermatologie"
                class="block w-full px-4 py-2 mt-1 text-app bg-gray-200 border rounded-md focus:border-indigo-500 focus:bg-white focus:outline-none" />
            </div>
            <div class="w-full mb-4 px-3">
              <label class="block text-sm font-medium text-app" for="diploma">Diplômes</label>
              <input v-model="user.diploma" type="text" id="diploma" placeholder="MD, PhD"
                class="block w-full px-4 py-2 mt-1 text-app bg-gray-200 border rounded-md focus:border-indigo-500 focus:bg-white focus:outline-none" />
            </div>
          </div>

          <!-- Autres Informations Communes -->
          <div class="flex flex-wrap mb-4">
            <div class="w-full mb-4 px-3">
              <label class="block text-sm font-medium text-app" for="bio">Biographie</label>
              <textarea v-model="user.bio" id="bio" rows="3"
                class="block w-full px-4 py-2 mt-1 text-app bg-gray-200 border rounded-md focus:border-indigo-500 focus:bg-white focus:outline-none"></textarea>
            </div>
            <div class="w-full mb-4 px-3">
              <label class="block text-sm font-medium text-app" for="tags">Tags</label>
              <input v-model="user.tags" type="text" id="tags" placeholder="Médecin, Cardiologue, Patient"
                class="block w-full px-4 py-2 mt-1 text-app bg-gray-200 border rounded-md focus:border-indigo-500 focus:bg-white focus:outline-none" />
            </div>
            <div class="w-full mb-4 px-3">
              <label class="block text-sm font-medium text-app" for="socials">Réseaux Sociaux</label>
              <input v-model="user.socials.linkedin" type="text" id="linkedin" placeholder="Lien LinkedIn"
                class="block w-full px-4 py-2 mt-1 text-app bg-gray-200 border rounded-md focus:border-indigo-500 focus:bg-white focus:outline-none" />
            </div>
          </div>
        </div>
        <!-- Bouton Sauvegarder -->
        <div class="w-full mb-4 px-3 mt-12">
          <button type="submit"
            class="w-full px-4 py-2 text-white bg-app rounded-md  focus:outline-none focus:bg-indigo-700">
            Sauvegarder
          </button>
        </div>
      </form>
    </div>
  </div>
</template>
