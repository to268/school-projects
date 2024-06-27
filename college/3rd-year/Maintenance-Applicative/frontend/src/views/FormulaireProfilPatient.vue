<script setup>
import { ref, onMounted } from "vue";
import {
  MedicalProfile,
  Disease,
  Allergy,
  Surgery,
  Diagnostic,
} from "../models/MedicalProfile.js";
import DossiersMedicaux from "./DossiersMedicaux.vue";
import { User } from "@/models/User";
import { useAuthStore } from "../JS/Auth.js";

const form = ref(null);
const authStore = useAuthStore();

let displayChoosePatient = ref(false);
let medicalProfileId = ref("");
let medicalProfile = ref(new MedicalProfile());
const ready = ref(false);

function validityCheck() {
  // Réinitialiser les messages de validation personnalisés pour tous les champs
  const inputs = form.value.querySelectorAll("input[required]");
  inputs.forEach((input) => input.setCustomValidity(""));

  // Vérifier chaque champ et définir les messages personnalisés si nécessaire
  inputs.forEach((input) => {
    if (!input.checkValidity()) {
      input.setCustomValidity(input.title);
    }
  });

  // Vérifier la validité du formulaire et afficher les messages de validation natifs
  if (form.value) {
    if (!form.value.reportValidity()) {
      inputs.forEach((input) => {
        if (!input.checkValidity()) {
          input.classList.add("focus:border-red-500", "border-red-500");
        } else {
          input.classList.remove("focus:border-red-500", "border-red-500");
        }
      });
      // Le formulaire n'est pas valide, les messages de validation natifs s'affichent automatiquement
    } else {
      // Le formulaire est valide, vous pouvez procéder avec la soumission ou d'autres actions
      saveProfile();
    }
  }
}

async function tryGetProfile() {
  try {
    const response = await authStore.fetchWithAuth(
      `http://localhost:5000/api/medicalprofile/${medicalProfileId.value}`,
      {
        method: "GET",
        headers: {
          "Content-type": "application/json",
        },
      }
    );

    if (!response.ok) {
    } else {
      const data = await response.json();
      medicalProfile.value = new MedicalProfile(data);
      medicalProfile.value.patient.birthDate = new Date(
        medicalProfile.value.patient.birthDate
      ).toLocaleDateString();
      ready.value = true;
    }
  } catch (error) {
    console.error(
      "Il n'y a pas de medical Profile pour cette utilisateur:",
      error.message
    );
  }
}

async function saveProfile() {
  try {
    const response = await authStore.fetchWithAuth(
      "http://localhost:5000/api/medicalprofile/save",
      {
        method: "POST",
        headers: {
          "Content-type": "application/json",
        },
        body: JSON.stringify({ medicalProfile: medicalProfile.value }),
      }
    );

    if (!response.ok) {
      const errorData = await response.json();
      const errorMessage =
        errorData.message || "Echec de l'enregistrement du Medical Profile:";
      saveProfilFailed();
      throw new Error(`Erreur ${response.status}: ${errorMessage}`);
    } else {
      saveProfilSuccess();
    }
  } catch (error) {
    console.error(
      "Echec de l'enregistrement du Medical Profile:",
      error.message
    );
  }
}

function validateForm() {
  validityCheck();
}

function updateBirthDate(event) {
  const value = event.target.value;
  this.medicalProfile.birthDate = value;
}

function displayDataChoosePatient() {
  displayChoosePatient.value = true;
}

function removeChoosePatient() {
  displayChoosePatient.value = false;
}

function receiveEmit(Id) {
  medicalProfileId.value = Id;
  tryGetProfile();
  removeChoosePatient();
}

function addNewDiagnostic() {
  medicalProfile.value.diagnostics.push(new Diagnostic());
}

function addNewSurgery() {
  medicalProfile.value.surgery.push(new Surgery());
}

function addNewAllergy() {
  medicalProfile.value.allergies.push(new Allergy());
}

function addNewDisease() {
  medicalProfile.value.diseases.push(new Disease());
}

function removeItem(array, index) {
  array.splice(index, 1);
}

onMounted(() => { });

function onInput(e) {
  text.value = e.target.value;
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

function saveProfilSuccess() {
  Toast.fire({
    title: "Profil medical enregistré avec succès !",
    icon: "success",
  });
}

function saveProfilFailed() {
  Toast.fire({
    title: "Echec de l'enregistrement du profil medical !",
    icon: "error",
  });
}
</script>
<template>
  <div class="flex flex-col w-full">
    <div class="w-full px-10 pt-10">
      <div class="flex flex-wrap -mx-3 mb-6">
        <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
          <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-patientId">
            ID du Patient
          </label>
          <input
            class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white"
            id="grid-patientId" type="text" v-model="medicalProfile._id" @focus="displayDataChoosePatient"
            placeholder="Click to choose Patient ID" />
        </div>
      </div>
    </div>
    <form v-if="ready" ref="form" class="w-full p-10 overflow-y-auto h-screen" @submit.prevent>
      <div class="flex flex-wrap -mx-3 mb-6">
        <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
          <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-first-name">
            Prénom
          </label>
          <input
            class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white"
            id="grid-first-name" type="text" placeholder="Jane" v-model="medicalProfile.patient.firstName" required
            title="Veuillez entrer le prénom" />
        </div>
        <div class="w-full md:w-1/2 px-3">
          <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-last-name">
            Nom
          </label>
          <input
            class="appearance-none block w-full bg-gray-300 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
            id="grid-last-name" type="text" placeholder="Doe" v-model="medicalProfile.patient.lastName" required
            title="Veuillez entrer le nom" />
        </div>
      </div>
      <div class="flex flex-wrap -mx-3 mb-6">
        <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
          <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-birth-date">
            Date de naissance
          </label>

          <div class="relative">
            <div class="absolute inset-y-0 start-0 flex items-center ps-3.5 pointer-events-none">
              <svg class="w-4 h-4 text-gray-500 dark:text-gray-400" aria-hidden="true"
                xmlns="http://www.w3.org/2000/svg" fill="currentColor" viewBox="0 0 20 20">
                <path
                  d="M20 4a2 2 0 0 0-2-2h-2V1a1 1 0 0 0-2 0v1h-3V1a1 1 0 0 0-2 0v1H6V1a1 1 0 0 0-2 0v1H2a2 2 0 0 0-2 2v2h20V4ZM0 18a2 2 0 0 0 2 2h16a2 2 0 0 0 2-2V8H0v10Zm5-8h10a1 1 0 0 1 0 2H5a1 1 0 0 1 0-2Z" />
              </svg>
            </div>
            <input datepicker datepicker-format="yyyy-mm-dd" type="text" id="grid-birth-date"
              class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white ps-10"
              @blur="updateBirthDate($event)" placeholder="Choisissez une date"
              v-model="medicalProfile.patient.birthDate" />
          </div>
        </div>
        <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
          <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-gender">
            Genre
          </label>
          <div class="relative">
            <select
              class="block appearance-none w-full bg-gray-300 border border-gray-200 text-gray-700 py-3 px-4 pr-8 rounded leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
              id="grid-gender" v-model="medicalProfile.patient.sex">
              <option disabled selected>Veuillez choisir le genre</option>
              <option>Homme</option>
              <option>Femme</option>
              <option>Autre</option>
            </select>
            <div class="pointer-events-none absolute inset-y-0 right-0 flex items-center px-2 text-gray-700">
              <svg class="fill-current h-4 w-4" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20">
                <path d="M9.293 12.95l.707.707L15.657 8l-1.414-1.414L10 10.828 5.757 6.586 4.343 8z" />
              </svg>
            </div>
          </div>
        </div>
      </div>
      <!-- <div class="flex flex-wrap -mx-3 mb-6">
            <div class="w-full px-3">
            <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-password">
                Mot de passe
            </label>
            <input class="appearance-none block w-full bg-gray-300 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white focus:border-gray-500" id="grid-password" type="password" placeholder="******************">
            <p class="text-gray-600 text-xs italic">Make it as long and as crazy as you'd like</p>
            </div>
        </div> -->
      <div class="flex flex-wrap -mx-3 mb-2">
        <div class="w-full md:w-1/4 px-3 mb-6 md:mb-0">
          <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-adress">
            Adresse
          </label>
          <input
            class="appearance-none block w-full bg-gray-300 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
            id="grid-adress" type="text" placeholder="77 rue Jean Camus"
            v-model="medicalProfile.patient.address.road" />
        </div>
        <div class="w-full md:w-1/4 px-3 mb-6 md:mb-0">
          <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-zip">
            Code postal
          </label>
          <input
            class="appearance-none block w-full bg-gray-300 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
            id="grid-zip" type="text" placeholder="90210" v-model="medicalProfile.patient.address.postalCode" />
        </div>
        <div class="w-full md:w-1/4 px-3 mb-6 md:mb-0">
          <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-city">
            Ville
          </label>
          <input
            class="appearance-none block w-full bg-gray-300 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
            id="grid-city" type="text" placeholder="Albuquerque" v-model="medicalProfile.patient.address.city" />
        </div>
        <div class="w-full md:w-1/4 px-3 mb-6 md:mb-0">
          <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-state">
            Pays
          </label>
          <div class="relative">
            <select
              class="block appearance-none w-full bg-gray-300 border border-gray-200 text-gray-700 py-3 px-4 pr-8 rounded leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
              id="grid-state" v-model="medicalProfile.patient.address.state">
              <option disabled selected>Veuillez choisir le pays</option>
              <option>France</option>
              <option>Europe</option>
              <option>Autre</option>
            </select>
            <div class="pointer-events-none absolute inset-y-0 right-0 flex items-center px-2 text-gray-700">
              <svg class="fill-current h-4 w-4" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20">
                <path d="M9.293 12.95l.707.707L15.657 8l-1.414-1.414L10 10.828 5.757 6.586 4.343 8z" />
              </svg>
            </div>
          </div>
        </div>
      </div>
      <div class="flex flex-wrap -mx-3 mb-6">
        <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
          <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-phone">
            Téléphone
          </label>
          <input
            class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white"
            id="grid-phone" type="text" placeholder="0695451842" v-model="medicalProfile.patient.phone.mobile" />
        </div>
        <div class="w-full md:w-1/2 px-3">
          <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-mail">
            Mail
          </label>
          <input
            class="appearance-none block w-full bg-gray-300 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
            id="grid-mail" type="text" placeholder="jane.doe@gmail.com" v-model="medicalProfile.patient.email" />
        </div>
      </div>

      <div class="flex flex-wrap -mx-3 mb-2">
        <div class="w-full md:w-1/3 px-3 mb-6 md:mb-0">
          <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-weight">
            Poids (kg)
          </label>
          <input
            class="appearance-none block w-full bg-gray-300 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
            id="grid-weight" type="number" min="0" placeholder="78,5" v-model="medicalProfile.weight" />
        </div>
        <div class="w-full md:w-1/3 px-3 mb-6 md:mb-0">
          <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-size">
            Taille (m)
          </label>
          <input
            class="appearance-none block w-full bg-gray-300 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
            id="grid-size" type="number" min="0" placeholder="1,82" v-model="medicalProfile.height" />
        </div>
        <div class="w-full md:w-1/3 px-3 mb-6 md:mb-0">
          <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-blood-type">
            Groupe Sanguin
          </label>
          <div class="relative">
            <select
              class="block appearance-none w-full bg-gray-300 border border-gray-200 text-gray-700 py-3 px-4 pr-8 rounded leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
              id="grid-blood-type" v-model="medicalProfile.bloodType">
              <option disabled selected>
                Veuillez choisir le groupe sanguin
              </option>
              <option>O-</option>
              <option>A-</option>
              <option>B-</option>
              <option>AB-</option>
              <option>O+</option>
              <option>A+</option>
              <option>B+</option>
              <option>AB+</option>
            </select>
            <div class="pointer-events-none absolute inset-y-0 right-0 flex items-center px-2 text-gray-700">
              <svg class="fill-current h-4 w-4" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20">
                <path d="M9.293 12.95l.707.707L15.657 8l-1.414-1.414L10 10.828 5.757 6.586 4.343 8z" />
              </svg>
            </div>
          </div>
        </div>
      </div>

      <div class="flex flex-wrap -mx-3 mb-2">
        <div class="w-full px-3 mb-6 md:mb-0">
          <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-family-history">
            Historique Familial
          </label>
          <textarea
            class="appearance-none block w-full bg-gray-300 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
            name="" id="grid-family-history" cols="30" rows="10" v-model="medicalProfile.famillyHistory"></textarea>
        </div>
      </div>

      <div class="flex flex-wrap -mx-3 mb-2">
        <div class="w-full px-3 mb-6 md:mb-0">
          <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2" for="grid-remark">
            Remarques
          </label>
          <textarea
            class="appearance-none block w-full bg-gray-300 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
            name="" id="grid-remark" cols="30" rows="10" v-model="medicalProfile.notes"></textarea>
        </div>
      </div>

      <div class="divide-y divide-dashed">
        <!-- Section pour les diagnostics -->
        <div class="flex flex-wrap -mx-3 my-5">
          <div class="w-full px-3 mb-6 md:mb-0">
            <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold my-4">
              Diagnostics
            </label>
            <div v-for="(diagnostic, index) in medicalProfile.diagnostics" :key="index" class="mb-4">
              <input class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-2 px-4 mb-2"
                type="text" placeholder="Diagnostic" v-model="diagnostic.diagnostic" />
              <input class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-2 px-4 mb-2"
                type="date" placeholder="Date" v-model="diagnostic.date" />
              <input class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-2 px-4 mb-2"
                type="text" placeholder="Traitement" v-model="diagnostic.treatment" />
              <textarea class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-2 px-4"
                placeholder="Remarques" v-model="diagnostic.notes"></textarea>
              <button @click="removeItem(medicalProfile.diagnostics, index)" class="mt-2 text-red-500">
                Supprimer
              </button>
            </div>
            <button @click="addNewDiagnostic"
              class="shadow bg-blue-500 hover:bg-blue-400 focus:shadow-outline focus:outline-none text-white font-bold py-2 px-4 rounded mt-2">
              Ajouter un Diagnostic
            </button>
          </div>
        </div>

        <!-- Section pour les chirurgies -->
        <div class="flex flex-wrap -mx-3 my-5">
          <div class="w-full px-3 mb-6 md:mb-0">
            <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold my-4">
              Chirurgies
            </label>
            <div v-for="(surgery, index) in medicalProfile.surgery" :key="index" class="mb-4">
              <input class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-2 px-4 mb-2"
                type="text" placeholder="Type de chirurgie" v-model="surgery.type" />
              <input class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-2 px-4 mb-2"
                type="date" placeholder="Date" v-model="surgery.date" />
              <textarea class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-2 px-4"
                placeholder="Détails" v-model="surgery.details"></textarea>
              <button @click="removeItem(medicalProfile.surgery, index)" class="mt-2 text-red-500">
                Supprimer
              </button>
            </div>
            <button @click="addNewSurgery"
              class="shadow bg-blue-500 hover:bg-blue-400 focus:shadow-outline focus:outline-none text-white font-bold py-2 px-4 rounded mt-2">
              Ajouter une Chirurgie
            </button>
          </div>
        </div>

        <!-- Section pour les allergies -->
        <div class="flex flex-wrap -mx-3 my-5">
          <div class="w-full px-3 mb-6 md:mb-0">
            <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold my-4">
              Allergies
            </label>
            <div v-for="(allergy, index) in medicalProfile.allergies" :key="index" class="mb-4">
              <input class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-2 px-4 mb-2"
                type="text" placeholder="Allergie" v-model="allergy.allergy" />
              <input class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-2 px-4 mb-2"
                type="text" placeholder="Réaction" v-model="allergy.reaction" />
              <input class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-2 px-4 mb-2"
                type="number" placeholder="Sévérité" v-model="allergy.severity" />
              <button @click="removeItem(medicalProfile.allergies, index)" class="mt-2 text-red-500">
                Supprimer
              </button>
            </div>
            <button @click="addNewAllergy"
              class="shadow bg-blue-500 hover:bg-blue-400 focus:shadow-outline focus:outline-none text-white font-bold py-2 px-4 rounded mt-2">
              Ajouter une Allergie
            </button>
          </div>
        </div>

        <!-- Section pour les maladies -->
        <div class="flex flex-wrap -mx-3 my-5">
          <div class="w-full px-3 mb-6 md:mb-0">
            <label class="block uppercase tracking-wide text-gray-700 text-xs font-bold my-4">
              Maladies
            </label>
            <div v-for="(disease, index) in medicalProfile.diseases" :key="index" class="mb-4">
              <input class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-2 px-4 mb-2"
                type="text" placeholder="Maladie" v-model="disease.name" />
              <input class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-2 px-4 mb-2"
                type="date" placeholder="Date de diagnostic" v-model="disease.diagnosisDate" />
              <textarea class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-2 px-4"
                placeholder="Détails" v-model="disease.details"></textarea>
              <button @click="removeItem(medicalProfile.diseases, index)" class="mt-2 text-red-500">
                Supprimer
              </button>
            </div>
            <button @click="addNewDisease"
              class="shadow bg-blue-500 hover:bg-blue-400 focus:shadow-outline focus:outline-none text-white font-bold py-2 px-4 rounded mt-2">
              Ajouter une Maladie
            </button>
          </div>
        </div>
      </div>
      <div class="flex justify-end">
        <button @click="validateForm"
          class="shadow bg-purple-500 hover:bg-purple-400 focus:shadow-outline focus:outline-none text-white font-bold py-2 px-4 rounded"
          type="button">
          Enregistrer
        </button>
      </div>
    </form>
  </div>
  <div class="w-screen h-screen z-50 absolute bg-gray-300 bg-opacity-50 flex justify-center items-center"
    v-if="displayChoosePatient" @click.self="removeChoosePatient">
    <div class="w-11/12 h-5/6 bg-app p-10 overflow-y-hidden relative m-14 rounded-lg">
      <svg @click="removeChoosePatient" height="40px" id="Layer_1"
        class="fill-gray-700 absolute top-5 right-5 cursor-pointer" style="enable-background: new 0 0 512 512"
        version="1.1" viewBox="0 0 512 512" width="40px" xml:space="preserve" xmlns="http://www.w3.org/2000/svg"
        xmlns:xlink="http://www.w3.org/1999/xlink">
        <path
          d="M443.6,387.1L312.4,255.4l131.5-130c5.4-5.4,5.4-14.2,0-19.6l-37.4-37.6c-2.6-2.6-6.1-4-9.8-4c-3.7,0-7.2,1.5-9.8,4  L256,197.8L124.9,68.3c-2.6-2.6-6.1-4-9.8-4c-3.7,0-7.2,1.5-9.8,4L68,105.9c-5.4,5.4-5.4,14.2,0,19.6l131.5,130L68.4,387.1  c-2.6,2.6-4.1,6.1-4.1,9.8c0,3.7,1.4,7.2,4.1,9.8l37.4,37.6c2.7,2.7,6.2,4.1,9.8,4.1c3.5,0,7.1-1.3,9.8-4.1L256,313.1l130.7,131.1  c2.7,2.7,6.2,4.1,9.8,4.1c3.5,0,7.1-1.3,9.8-4.1l37.4-37.6c2.6-2.6,4.1-6.1,4.1-9.8C447.7,393.2,446.2,389.7,443.6,387.1z" />
      </svg>
      <h3 class="block uppercase tracking-wide  font-bold mb-10 text-3xl">
        Sélectionner le patient
      </h3>
      <DossiersMedicaux :patient-id="patientId" @change-id="receiveEmit" />
    </div>
  </div>
</template>
