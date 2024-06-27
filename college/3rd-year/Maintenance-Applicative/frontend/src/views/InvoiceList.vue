<script setup>
import { ref, onMounted } from "vue";
import { Invoice } from "../models/Invoice.js"; // Assurez-vous que le chemin est correct
import { useAuthStore } from "../JS/Auth.js";
import { User } from "../models/User.js";

const authStore = useAuthStore();
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
    user.value = new User(userData);
  } catch (error) {
    console.error(
      "Erreur lors de la récupération de l'utilisateur:",
      error.message
    );
  }
};

const formatDate = (date) => {
  return new Date(date).toLocaleDateString();
};

const formatCurrency = (amount) => {
  return new Intl.NumberFormat("fr-FR", {
    style: "currency",
    currency: "EUR",
  }).format(amount);
};

const payInvoice = async (invoice) => {
  try {
    const response = await authStore.fetchWithAuth(
      "http://localhost:5000/api/invoice/payement",
      {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ invoiceId: invoice._id }),
      }
    );

    if (!response.ok) {
      const errorData = await response.json();
      const errorMessage = errorData.message || "Erreur lors du paiement.";
      throw new Error(`Erreur ${response.status}: ${errorMessage}`);
    }

    const data = await response.json();
    window.location.href = data.redirectUrl;
    user.value = new user(userData);
  } catch (error) {
    console.error("Erreur lors du paiement:", error.message);
  }
};

const downloadPdf = async (invoice) => {
  try {
    const response = await authStore.fetchWithAuth(
      `http://localhost:5000/api/invoice/${invoice._id}/download`,
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
    link.setAttribute("download", `invoice_${invoice._id}.pdf`);
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
    window.URL.revokeObjectURL(url);
  } catch (error) {
    console.error("Erreur lors du téléchargement du PDF:", error.message);
  }
};

onMounted(fetchUser);
</script>

<template>
  <div v-if="user != null" class="container mx-auto p-4 overflow-y-auto">
    <h1 class="text-2xl font-bold mb-4">
      Factures de {{ user.firstName }} {{ user.lastName }}
    </h1>
    <div v-if="user.invoices.length === 0" class="text-center py-4">
      <p class="text-gray-500">Aucune facture trouvée.</p>
    </div>
    <div v-else class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4 h-full w-full">
      <div v-for="invoice in user.invoices" :key="invoice.id"
        class="bg-white shadow-md rounded-lg p-4 h-fit pb-20 relative">
        <h2 class="text-gray-600 text-lg font-semibold">
          Facture du {{ formatDate(invoice.createdAt) }}
        </h2>
        <p class="text-gray-600">Date: {{ formatDate(invoice.createdAt) }}</p>
        <p class="text-gray-600">
          Total: {{ formatCurrency(invoice.totalAmount) }}
        </p>
        <p class="text-gray-600">Statut: {{ invoice.status }}</p>
        <h3 class="text-gray-600 text-md font-semibold mt-4">Articles:</h3>
        <ul class="list-disc pl-5">
          <li v-for="item in invoice.items" :key="item.product" class="text-gray-600">
            {{ item.product }} - {{ item.description }} ({{ item.quantity }}) à
            {{ formatCurrency(item.price) }}
            chacun
          </li>
        </ul>
        <button @click="downloadPdf(invoice)"
          class="bg-green-500 text-white font-bold py-2 px-4 rounded hover:bg-blue-700 absolute bottom-5 left-5">
          Télécharger
        </button>
        <button @click="payInvoice(invoice)"
          class="bg-blue-500 text-white font-bold py-2 px-4 rounded hover:bg-blue-700 absolute bottom-5 right-5">
          Payer
        </button>
      </div>
    </div>
  </div>
</template>
