<script setup>
import { ref, onMounted } from "vue";
import { useAuthStore } from "../JS/Auth.js";
import { User } from "../models/User.js";

const authStore = useAuthStore();
const prescriptions = ref([]);

let user = ref(null);

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
    user.value = userData._id;
  } catch (error) {
    console.error(
      "Erreur lors de la récupération de l'utilisateur:",
      error.message
    );
  }
};

const fetchPrescriptions = async () => {
  await fetchUser();
  try {
    const response = await authStore.fetchWithAuth(
      `http://localhost:5000/api/prescription/getByUserId/`+user.value,
      {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
      }
    );

    if (!response.ok) {
      const errorData = await response.json();
      const errorMessage =
        errorData.message ||
        "Erreur lors de la récupération des prescriptions.";
      throw new Error(`Erreur ${response.status}: ${errorMessage}`);
    }

    const data = await response.json();
    console.log(data)
    prescriptions.value = data.prescription;
  } catch (error) {
    console.error(
      "Erreur lors de la récupération des prescriptions:",
      error.message
    );
  }
};

const formatDate = (date) => {
  return new Date(date).toLocaleDateString();
};

const downloadPdf = async (prescription) => {
  try {
    const response = await authStore.fetchWithAuth(
      `http://localhost:5000/api/prescription/${prescription._id}/download`,
      {
        method: "GET",
        headers: {
          "Content-Type": "application/pdf",
        },
      }
    );

    if (!response.ok) throw new Error("Erreur lors du téléchargement du PDF");

    const blob = await response.blob();
    const url = window.URL.createObjectURL(blob);

    // Créez un lien pour déclencher le téléchargement
    const link = document.createElement("a");
    link.href = url;
    link.setAttribute("download", `prescription_${prescription._id}.pdf`);
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
    window.URL.revokeObjectURL(url);
  } catch (error) {
    console.error("Erreur lors du téléchargement du PDF:", error.message);
  }
};

onMounted(fetchPrescriptions);
</script>

<template>
  <div
    v-if="prescriptions.length > 0"
    class="container mx-auto p-4 overflow-y-auto"
  >
    <h1 class="text-2xl font-bold mb-4">Prescriptions</h1>
    <div
      class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4 h-full w-full"
    >
      <div
        v-for="prescription in prescriptions"
        :key="prescription._id"
        class="bg-white shadow-md rounded-lg p-4 h-fit relative pb-20"
      >
        <h2 class="text-gray-600 text-lg font-semibold">
          Prescription du {{ formatDate(prescription.date) }}
        </h2>
        <p class="text-gray-600">
          Docteur: {{ prescription.doctor.firstName }}
          {{ prescription.doctor.lastName }}
        </p>
        <p class="text-gray-600">Date: {{ formatDate(prescription.date) }}</p>
        <h3 class="text-gray-600 text-md font-semibold mt-4">Médicaments:</h3>
        <ul class="list-disc pl-5">
          <li
            v-for="item in prescription.medicines"
            :key="item.medicine"
            class="text-gray-600"
          >
            {{ item.medicine }} - {{ item.dosage }} ({{ item.frequency }} fois
            par jour pendant {{ item.duration }} jours)
          </li>
        </ul>
        <button
          @click="downloadPdf(prescription)"
          class="bg-green-500 text-white font-bold py-2 px-4 rounded hover:bg-blue-700 absolute bottom-5 left-5"
        >
          Télécharger PDF
        </button>
      </div>
    </div>
  </div>
  <div v-else class="container mx-auto p-4">
    <h1 class="text-2xl font-bold mb-4">Prescriptions</h1>
    <p class="text-center text-gray-500">Aucune prescription trouvée.</p>
  </div>
</template>
