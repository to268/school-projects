<script setup>
import { ref, onMounted } from "vue";
import { Medication } from "../models/Medication.js";
import { Prescription, PrescriptionItem } from "../models/Prescription.js";
import DossiersMedicaux from "./DossiersMedicaux.vue";
import { useAuthStore } from "../JS/Auth.js";
// import { User } from '../models/User';

let displayFormAddMedication = ref(false);

let prescription = ref(new Prescription());

const medication = ref(new Medication());

const displayChoosePatient = ref(false);

const authStore = useAuthStore();

function displayDataChoosePatient() {
  displayChoosePatient.value = true;
}

function removeChoosePatient() {
  displayChoosePatient.value = false;
}

function addMedication() {
  displayFormAddMedication.value = true;
}

function removeAddMedication() {
  displayFormAddMedication.value = false;
}

const addItem = () => {
  prescription.value.medicines.push(new PrescriptionItem());
};

const removeItem = (index) => {
  if (prescription.value.medicines.length > 1) {
    prescription.value.medicines.splice(index, 1);
  } else {
    prescription.value.medicines[0] = new PrescriptionItem();
  }
};

const createPrescription = async () => {
  const doctor = await fetchDoctor();
  prescription.value.date = new Date();
  prescription.value.doctorId = doctor._id;

  try {
    const response = await authStore.fetchWithAuth(
      "http://localhost:5000/api/prescription",
      {
        method: "POST",
        headers: {
          "Content-type": "application/json",
        },
        body: prescription.value.toJson(),
      }
    );

    if (!response.ok) {
      const errorData = await response.json();
      const errorMessage =
        errorData.message || "Erreur lors de la création de la prescription.";
      createPrescriptionFailed();
      throw new Error(`Erreur ${response.status}: ${errorMessage}`);
    }

    const data = await response.json();
    prescription.value = new Prescription();
    createPrescriptionSuccess();
  } catch (error) {
    console.error(
      "Erreur lors de la création de la prescription:",
      error.message
    );
  }
};

const fetchDoctor = async () => {
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

    const data = await response.json();
    return data;
  } catch (error) {
    console.error(
      "Erreur lors de la récupération de l'utilisateur:",
      error.message
    );
  }
};

onMounted(() => {
  addItem();
});
function receiveEmit(patientId) {
  prescription.value.patientId = patientId;
  removeChoosePatient();
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

function createPrescriptionSuccess() {
  Toast.fire({
    title: "Prescription créée avec succès !",
    icon: "success",
  });
}

function createPrescriptionFailed() {
  Toast.fire({
    title: "Echec de la création de la Prescription !",
    icon: "error",
  });
}
</script>

<template>
  <div class="w-full h-screen overflow-y-auto h-screen p-20">
    <button
      class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline mb-10"
      type="submit"
      @click="addMedication"
    >
      Créer Médicament
    </button>
    <form ref="form" class="w-full h-fit">
      <div class="w-full md:w-1/2 -mx-3 px-3 mb-20">
        <label
          class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
          for="grid-patientId"
        >
          ID du Patient
        </label>
        <input
          class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white"
          id="grid-patientId"
          type="text"
          v-model="prescription.patientId"
          @focus="displayDataChoosePatient"
          placeholder="Patient ID"
        />
      </div>
      <div
        v-for="(prescriptionItem, index) in prescription.medicines"
        :key="index"
        class="flex flex-wrap -mx-3 mb-6"
      >
        <div class="w-full md:w-1/5 px-3 mb-6 md:mb-0">
          <label
            class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
            for="grid-medicine"
          >
            Médicament
          </label>
          <input
            v-model="prescriptionItem.medicine"
            class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white"
            id="grid-medicine"
            type="text"
            placeholder="Doliprane"
            required
            title="Veuillez entrer le prénom"
          />
        </div>
        <div class="w-full md:w-1/5 px-3">
          <label
            class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
            for="grid-dosage"
          >
            Dosage
          </label>
          <input
            v-model="prescriptionItem.dosage"
            class="appearance-none block w-full bg-gray-300 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
            id="grid-dosage"
            type="text"
            placeholder="500mg"
            required
            title="Veuillez entrer le nom"
          />
        </div>
        <div class="w-full md:w-1/5 px-3">
          <label
            class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
            for="grid-frequency"
          >
            Fréquence
          </label>
          <input
            v-model="prescriptionItem.frequency"
            class="appearance-none block w-full bg-gray-300 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
            id="grid-frequency"
            type="text"
            placeholder="2 fois par jour"
            required
            title="Veuillez entrer le nom"
          />
        </div>
        <div class="w-full md:w-1/5 px-3">
          <label
            class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
            for="grid-duration"
          >
            Durée en jours
          </label>
          <input
            v-model="prescriptionItem.duration"
            class="appearance-none block w-full bg-gray-300 text-gray-700 border border-gray-200 rounded py-3 px-4 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
            id="grid-duration"
            type="number"
            min="0"
            max="100"
            placeholder="en jours"
            required
            title="Veuillez entrer la durée de la boite en jours"
          />
        </div>
        <div class="w-full md:w-1/5 px-3 flex items-center justify-center mt-4">
          <button
            type="button"
            @click="removeItem(index)"
            class="bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded"
          >
            Supprimer
          </button>
        </div>

        <div class="w-full px-3 mb-6"></div>
      </div>

      <button
        type="button"
        @click="addItem"
        class="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 rounded"
      >
        Ajouter un Médicament
      </button>
      <div class="flex justify-end">
        <button
          class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline mb-10"
          type="button"
          @click="createPrescription"
        >
          Créer Prescription
        </button>
      </div>
    </form>
  </div>

  <div
    class="w-screen h-screen z-50 absolute bg-gray-300 bg-opacity-50 flex justify-center items-center"
    v-if="displayFormAddMedication"
    @click.self="removeAddMedication"
  >
    <div
      class="w-10/12 h-5/6 bg-red-300 p-20 overflow-y-scroll relative rounded-lg"
    >
      <svg
        @click="removeAddMedication"
        height="40px"
        id="Layer_1"
        class="fill-gray-700 absolute top-5 right-5 cursor-pointer"
        style="enable-background: new 0 0 512 512"
        version="1.1"
        viewBox="0 0 512 512"
        width="40px"
        xml:space="preserve"
        xmlns="http://www.w3.org/2000/svg"
        xmlns:xlink="http://www.w3.org/1999/xlink"
      >
        <path
          d="M443.6,387.1L312.4,255.4l131.5-130c5.4-5.4,5.4-14.2,0-19.6l-37.4-37.6c-2.6-2.6-6.1-4-9.8-4c-3.7,0-7.2,1.5-9.8,4  L256,197.8L124.9,68.3c-2.6-2.6-6.1-4-9.8-4c-3.7,0-7.2,1.5-9.8,4L68,105.9c-5.4,5.4-5.4,14.2,0,19.6l131.5,130L68.4,387.1  c-2.6,2.6-4.1,6.1-4.1,9.8c0,3.7,1.4,7.2,4.1,9.8l37.4,37.6c2.7,2.7,6.2,4.1,9.8,4.1c3.5,0,7.1-1.3,9.8-4.1L256,313.1l130.7,131.1  c2.7,2.7,6.2,4.1,9.8,4.1c3.5,0,7.1-1.3,9.8-4.1l37.4-37.6c2.6-2.6,4.1-6.1,4.1-9.8C447.7,393.2,446.2,389.7,443.6,387.1z"
        />
      </svg>
      <h3
        class="block uppercase tracking-wide text-gray-700 font-bold mb-10 text-3xl"
      >
        Ajout d'un Médicament
      </h3>
      <form @submit.prevent="addMedication" ref="form" class="w-full h-screen">
        <div class="flex flex-wrap -mx-3 mb-6">
          <div class="w-full md:w-1/4 px-3 mb-6 md:mb-0">
            <label
              class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
              for="grid-name"
            >
              Nom du Médicament
            </label>
            <input
              class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white"
              id="grid-name"
              type="text"
              v-model="medication.name"
              placeholder="Doliprane"
              required
            />
          </div>
          <div class="w-full md:w-1/4 px-3 mb-6 md:mb-0">
            <label
              class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
              for="grid-genericName"
            >
              Nom Générique
            </label>
            <input
              class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white"
              id="grid-genericName"
              type="text"
              v-model="medication.genericName"
              placeholder="Paracétamol"
              required
            />
          </div>
          <div class="w-full md:w-1/4 px-3 mb-6 md:mb-0">
            <label
              class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
              for="grid-medicationCode"
            >
              Code du Médicament
            </label>
            <input
              class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white"
              id="grid-medicationCode"
              type="text"
              v-model="medication.medicationCode"
              placeholder="123456"
              required
            />
          </div>
          <div class="w-full md:w-1/4 px-3 mb-6 md:mb-0">
            <label
              class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
              for="grid-form"
            >
              Forme Galénique
            </label>
            <div class="relative">
              <select
                class="block appearance-none w-full bg-gray-300 border border-gray-200 text-gray-700 py-3 px-4 pr-8 rounded leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
                id="grid-form"
                v-model="medication.form"
              >
                <option disabled selected>
                  Veuillez choisir la forme galénique
                </option>
                <option>Comprimés</option>
                <option>Comprimés à libération prolongée</option>
                <option>Gélules</option>
                <option>Gélules à libération prolongée</option>
                <option>Sachets</option>
                <option>Suppositoires</option>
                <option>Collyres</option>
                <option>Injectables</option>
                <option>Granules</option>
                <option>Sirops</option>
                <option>Solutions buvables</option>
                <option>Pommades</option>
                <option>Crèmes</option>
                <option>Bains de bouche</option>
                <option>Patchs</option>
              </select>
              <div
                class="pointer-events-none absolute inset-y-0 right-0 flex items-center px-2 text-gray-700"
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
          <div class="w-full md:w-1/4 px-3 mb-6 md:mb-0">
            <label
              class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
              for="grid-dosage"
            >
              Dosage
            </label>
            <input
              class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white"
              id="grid-dosage"
              type="text"
              v-model="medication.dosage"
              placeholder="500 mg"
              required
            />
          </div>
          <div class="w-full md:w-1/4 px-3 mb-6 md:mb-0">
            <label
              class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
              for="grid-frequency"
            >
              Fréquence
            </label>
            <input
              class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white"
              id="grid-frequency"
              type="text"
              v-model="medication.frequency"
              placeholder="3 fois par jour"
              required
            />
          </div>
          <div class="w-full md:w-1/4 px-3 mb-6 md:mb-0">
            <label
              class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
              for="grid-duration"
            >
              Durée
            </label>
            <input
              class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white"
              id="grid-duration"
              type="text"
              v-model="medication.duration"
              placeholder="7 jours"
              required
            />
          </div>
          <div class="w-full md:w-1/1 px-3 mb-6 md:mb-0">
            <label
              class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
              for="grid-administrationInstructions"
            >
              Instructions d'Administration
            </label>
            <textarea
              class="appearance-none block w-full bg-gray-300 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
              name=""
              id="grid-administrationInstructions"
              cols="30"
              rows="10"
              placeholder="À prendre avec de la nourriture"
              v-model="medication.administrationInstructions"
            ></textarea>
          </div>
          <div class="w-full md:w-1/1 px-3 mb-6 md:mb-0">
            <label
              class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
              for="grid-sideEffects"
            >
              Effets Secondaires
            </label>
            <textarea
              class="appearance-none block w-full bg-gray-300 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
              name=""
              id="grid-sideEffects"
              cols="30"
              rows="5"
              placeholder="Nausées, maux de tête"
              v-model="medication.sideEffects"
            ></textarea>
          </div>
          <div class="w-full md:w-1/1 px-3 mb-6 md:mb-0">
            <label
              class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
              for="grid-contraindications"
            >
              Contre-Indications
            </label>
            <textarea
              class="appearance-none block w-full bg-gray-300 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
              name=""
              id="grid-contraindications"
              cols="30"
              rows="5"
              placeholder="Grossesse, allergies"
              v-model="medication.contraindications"
            ></textarea>
          </div>
          <div class="w-full md:w-1/4 px-3 mb-6 md:mb-0">
            <label
              class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
              for="grid-manufacturer"
            >
              Fabricant
            </label>
            <input
              class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white"
              id="grid-manufacturer"
              type="text"
              v-model="medication.manufacturer"
              placeholder="PharmaCorp"
            />
          </div>
          <div class="w-full md:w-1/4 px-3 mb-6 md:mb-0">
            <label
              class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
              for="grid-expirationDate"
            >
              Date d'Expiration après achat
            </label>
            <input
              class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white"
              id="grid-expirationDate"
              type="week"
              v-model="medication.expirationDate"
            />
          </div>
          <div class="w-full md:w-1/4 px-3 mb-6 md:mb-0">
            <label
              class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
              for="grid-therapeuticClass"
            >
              Classe Thérapeutique
            </label>
            <div class="relative">
              <select
                class="block appearance-none w-full bg-gray-300 border border-gray-200 text-gray-700 py-3 px-4 pr-8 rounded leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
                id="grid-therapeuticClass"
                v-model="medication.therapeuticClass"
              >
                <option disabled selected>
                  Veuillez choisir la classe thérapeutique
                </option>
                <option>Allergologie</option>
                <option>Anesthésie, réanimation</option>
                <option>Antalgiques</option>
                <option>Anti-inflammatoires</option>
                <option>Cancérologie et hématologie</option>
                <option>Cardiologie et angéiologie</option>
                <option>Contraception et interruption de grossesse</option>
                <option>Dermatologie</option>
                <option>Endocrinologie</option>
                <option>Gastro-Entéro-Hépatologie</option>
                <option>Gynécologie</option>
                <option>Hémostase et sang</option>
                <option>Immunologie</option>
                <option>Infectiologie - Parasitologie</option>
                <option>Métabolisme et nutrition</option>
                <option>Neurologie-psychiatrie</option>
                <option>Ophtalmologie</option>
                <option>Oto-rhino-laryngologie</option>
                <option>Pneumologie</option>
                <option>
                  Produits diagnostiques ou autres produits thérapeutiques
                </option>
                <option>Rhumatologie</option>
                <option>Sang et dérivés</option>
                <option>Souches Homéopathiques</option>
                <option>Stomatologie</option>
                <option>Toxicologie</option>
                <option>Urologie néphrologie</option>
              </select>
              <div
                class="pointer-events-none absolute inset-y-0 right-0 flex items-center px-2 text-gray-700"
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
          <div class="w-full md:w-1/4 px-3 mb-6 md:mb-0">
            <label
              class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
              for="grid-price"
            >
              Prix
            </label>
            <input
              class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white"
              id="grid-price"
              type="number"
              v-model="medication.price"
              placeholder="10.00"
            />
          </div>
          <div class="w-full md:w-1/1 px-3 mb-6 md:mb-0">
            <label
              class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
              for="grid-additionalNotes"
            >
              Notes Supplémentaires
            </label>
            <textarea
              class="appearance-none block w-full bg-gray-300 text-gray-700 border border-gray-200 rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white focus:border-gray-500"
              name=""
              id="grid-additionalNotes"
              cols="30"
              rows="5"
              placeholder="Instructions spéciales"
              v-model="medication.additionalNotes"
            ></textarea>
          </div>
        </div>
        <button
          class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
          type="submit"
        >
          Ajouter Médicament
        </button>
      </form>
    </div>
  </div>
  <div
    class="w-screen h-screen z-50 absolute bg-gray-300 bg-opacity-50 flex justify-center items-center"
    v-if="displayChoosePatient"
    @click.self="removeChoosePatient"
  >
    <div
      class="w-11/12 h-5/6 bg-app p-10 overflow-y-scroll relative rounded-lg"
    >
      <svg
        @click="removeChoosePatient"
        height="40px"
        id="Layer_1"
        class="fill-gray-700 absolute top-5 right-5 cursor-pointer"
        style="enable-background: new 0 0 512 512"
        version="1.1"
        viewBox="0 0 512 512"
        width="40px"
        xml:space="preserve"
        xmlns="http://www.w3.org/2000/svg"
        xmlns:xlink="http://www.w3.org/1999/xlink"
      >
        <path
          d="M443.6,387.1L312.4,255.4l131.5-130c5.4-5.4,5.4-14.2,0-19.6l-37.4-37.6c-2.6-2.6-6.1-4-9.8-4c-3.7,0-7.2,1.5-9.8,4  L256,197.8L124.9,68.3c-2.6-2.6-6.1-4-9.8-4c-3.7,0-7.2,1.5-9.8,4L68,105.9c-5.4,5.4-5.4,14.2,0,19.6l131.5,130L68.4,387.1  c-2.6,2.6-4.1,6.1-4.1,9.8c0,3.7,1.4,7.2,4.1,9.8l37.4,37.6c2.7,2.7,6.2,4.1,9.8,4.1c3.5,0,7.1-1.3,9.8-4.1L256,313.1l130.7,131.1  c2.7,2.7,6.2,4.1,9.8,4.1c3.5,0,7.1-1.3,9.8-4.1l37.4-37.6c2.6-2.6,4.1-6.1,4.1-9.8C447.7,393.2,446.2,389.7,443.6,387.1z"
        />
      </svg>
      <h3
        class="block uppercase tracking-wide text-white font-bold mb-10 text-3xl"
      >
        Sélectionner le patient
      </h3>
      <DossiersMedicaux
        :patient-id="prescription.patientId"
        @change-id="receiveEmit"
      />
    </div>
  </div>
</template>
