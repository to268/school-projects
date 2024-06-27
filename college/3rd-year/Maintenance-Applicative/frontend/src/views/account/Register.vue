<script setup>
import { onMounted, ref, onUpdated } from "vue";
import { User } from "../../models/User.js";
import { useRouter } from "vue-router";

const router = useRouter();

const user = ref(new User());

function updateBirthDate(event) {
  const value = event.target.value;
  let age = calculate_age(Date.parse(value));

  if (age < 18) {
    const birthDateInput = document.getElementById("grid-birth-date");
    birthDateInput.setCustomValidity("Vous devez avoir au moins 18 ans.");
  }

  event.preventDefault();
  this.user.birthDate = value;
}

function calculate_age(time) {
  var diff_ms = Date.now() - time;
  var age_dt = new Date(diff_ms);

  return Math.abs(age_dt.getUTCFullYear() - 1970);
}

async function register() {
  try {
    const response = await fetch("http://localhost:5000/api/auth/register", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: user.value.toJsonStringify(),
    });

    if (!response.ok) {
      const errorData = await response.json();
      const errorMessage =
        errorData.message || "Erreur lors de la création de l'utilisateur.";
      throw new Error(`Erreur ${response.status}: ${errorMessage}`);
    }

    const data = await response.json();

    validationEmail();
    router.push("/Login");
  } catch (error) {
    console.error(
      "Erreur lors de la création de l'Utilisateur:",
      error.message
    );
  }
}

function validationEmail() {
  const Toast = Swal.mixin({
    toast: true,
    position: "top-end",
    showConfirmButton: false,
    timer: 20000,
    timerProgressBar: true,
    didOpen: (toast) => {
      toast.onmouseenter = Swal.stopTimer;
      toast.onmouseleave = Swal.resumeTimer;
    },
  });

  Toast.fire({
    title:
      "Créé avec succès ! Veuillez vérifiez votre email via le mail qui vous a été envoyé.",
    icon: "success",
  });
}
</script>

<template>
  <div class="flex items-center justify-center min-h-screen w-full">
    <div
      class="w-full max-w-lg p-4 space-y-6 bg-white text-app rounded-lg shadow-md"
    >
      <h2 class="text-2xl font-bold text-center ">
        Créer un Compte
      </h2>
      <form @submit.prevent="register">
        <div class="mb-4 px-3">
          <label
            class="block text-sm font-medium "
            for="accountType"
            >Type de Compte</label
          >
          <select
            v-model="user.role"
            id="accountType"
            required
            class="block w-full px-4 py-2 mt-1  bg-gray-200 border rounded-md  focus:bg-white focus:outline-none"
          >
            <option value="" disabled selected>
              Choisir le Type de Compte
            </option>
            <option value="patient">Patient</option>
            <option value="doctor">Médecin</option>
          </select>
        </div>

        <!-- Informations Générales -->
        <div v-show="user.role" class="flex flex-wrap">
          <div class="w-full mb-4 md:w-1/2 px-3">
            <label
              class="block text-sm font-medium "
              for="firstName"
              >Prénom</label
            >
            <input
              v-model="user.firstName"
              type="text"
              id="firstName"
              placeholder="Samuel"
              required
              class="block w-full px-4 py-2 mt-1  bg-gray-200 border rounded-md  focus:bg-white focus:outline-none"
            />
          </div>
          <div class="w-full mb-4 md:w-1/2 px-3">
            <label
              class="block text-sm font-medium "
              for="lastName"
              >Nom</label
            >
            <input
              v-model="user.lastName"
              type="text"
              id="lastName"
              placeholder="Pochat"
              required
              class="block w-full px-4 py-2 mt-1  bg-gray-200 border rounded-md  focus:bg-white focus:outline-none"
            />
          </div>
          <div class="w-full mb-4 px-3">
            <label class="block text-sm font-medium " for="email"
              >Email</label
            >
            <input
              v-model="user.email"
              type="email"
              id="email"
              placeholder="monmail@gmail.com"
              required
              class="block w-full px-4 py-2 mt-1  bg-gray-200 border rounded-md  focus:bg-white focus:outline-none"
            />
          </div>
          <div class="w-full mb-4 px-3">
            <label
              class="block uppercase tracking-wide  text-xs font-bold mb-2"
              for="grid-gender"
            >
              Genre
            </label>
            <div class="relative">
              <select
                class="block appearance-none w-full bg-gray-300 border border-gray-200  py-3 px-4 pr-8 rounded leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
                id="grid-gender"
                v-model="user.sex"
              >
                <option disabled selected>Veuillez choisir le genre</option>
                <option>Homme</option>
                <option>Femme</option>
                <option>Autre</option>
              </select>
              <div
                class="pointer-events-none absolute inset-y-0 right-0 flex items-center px-2 "
              >
                <svg
                  class="fill-current h-4 w-4"
                  xmlns="http://www.w3.org/2000/svg"
                  viewBox="0 0 20 20"
                >
                  <path
                    d="M9.293 12.95l.707.707L15.657 8l-1.414-1.414L10 10.828 5.757 6.586 4.343 8z"
                  />
                </svg>
              </div>
            </div>
          </div>
          <div class="w-full mb-4 px-3">
            <label
              class="block text-sm font-medium "
              for="password"
              >Mot de Passe</label
            >
            <input
              v-model="user.password"
              type="password"
              minlength="12"
              id="password"
              placeholder="monmotdepasse"
              required
              class="block w-full px-4 py-2 mt-1  bg-gray-200 border rounded-md  focus:bg-white focus:outline-none"
            />
          </div>

          <div class="w-full mb-4 px-3">
            <label
              class="block uppercase tracking-wide  text-xs font-bold mb-2"
              for="grid-birth-date"
            >
              Date de naissance
            </label>

            <div class="relative">
              <div
                class="absolute inset-y-0 start-0 flex items-center ps-3.5 pointer-events-none"
              >
                <svg
                  class="w-4 h-4 text-gray-500 dark:text-gray-400"
                  aria-hidden="true"
                  xmlns="http://www.w3.org/2000/svg"
                  fill="currentColor"
                  viewBox="0 0 20 20"
                >
                  <path
                    d="M20 4a2 2 0 0 0-2-2h-2V1a1 1 0 0 0-2 0v1h-3V1a1 1 0 0 0-2 0v1H6V1a1 1 0 0 0-2 0v1H2a2 2 0 0 0-2 2v2h20V4ZM0 18a2 2 0 0 0 2 2h16a2 2 0 0 0 2-2V8H0v10Zm5-8h10a1 1 0 0 1 0 2H5a1 1 0 0 1 0-2Z"
                  />
                </svg>
              </div>
              <input
                datepicker
                datepicker-format="yyyy-mm-dd"
                type="text"
                id="grid-birth-date"
                class="appearance-none block w-full bg-gray-300  border rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white ps-10"
                @blur="updateBirthDate($event)"
                placeholder="Choisissez une date"
              />
            </div>
          </div>
        </div>

        <!-- Informations Patient -->
        <div v-show="user.role === 'patient'">
          <!-- <div class="w-full mb-4 px-3">
                        <label class="block text-sm font-medium " for="medicalHistory">Historique
                            Médical</label>
                        <textarea v-model="medicalHistory" id="medicalHistory" rows="3"
                            class="block w-full px-4 py-2 mt-1  bg-gray-200 border rounded-md focus:border-indigo-500 focus:bg-white focus:outline-none"></textarea>
                    </div> -->
        </div>

        <!-- Informations Médecin -->
        <!-- <div v-if="user.role === 'doctor'">
                    <div class="w-full mb-4 px-3">
                        <label class="block text-sm font-medium " for="specialization">Spécialisation</label>
                        <input v-model="specialization" type="text" id="specialization" required
                            class="block w-full px-4 py-2 mt-1  bg-gray-200 border rounded-md focus:border-indigo-500 focus:bg-white focus:outline-none" />
                    </div>
                    <div class="w-full mb-4 px-3">
                        <label class="block text-sm font-medium " for="licenseNumber">Numéro de Licence</label>
                        <input v-model="licenseNumber" type="text" id="licenseNumber" required
                            class="block w-full px-4 py-2 mt-1  bg-gray-200 border rounded-md focus:border-indigo-500 focus:bg-white focus:outline-none" />
                    </div>
                </div> -->
        <div class="w-full mb-4 px-3">
          <button
            type="submit"
            class="w-full px-3 py-2 text-white bg-app rounded-md  focus:outline-none "
            id="create_account"
          >
            Créer un Compte
          </button>
        </div>
      </form>
      <div class="mt-4 text-center">
        <router-link to="/login" class="text-app"
          >Se connecter</router-link
        >
      </div>
    </div>
  </div>
</template>
