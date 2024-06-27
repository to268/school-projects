<script setup>
import { onMounted, ref, watchEffect } from "vue";
import { Invoice, InvoiceItem } from "../models/Invoice.js";
import { Medication } from "../models/Medication.js";
import DossiersMedicaux from "./DossiersMedicaux.vue";
import { useAuthStore } from "../JS/Auth.js";

const authStore = useAuthStore();

const invoice = ref(new Invoice());

const displayChoosePatient = ref(false);

let userId = null;

function displayDataChoosePatient() {
  displayChoosePatient.value = true;
}

function removeChoosePatient() {
  displayChoosePatient.value = false;
}

const addItem = () => {
  invoice.value.items.push(
    new InvoiceItem({
      product: "",
      description: "",
      quantity: 1,
      price: 0,
      total: 0,
    })
  );
};

const removeItem = (index) => {
  invoice.value.items.splice(index, 1);
};

const calculateTotal = () => {
  invoice.value.totalAmount = invoice.value.items.reduce(
    (acc, item) => acc + item.quantity * item.price,
    0
  );
};

watchEffect(calculateTotal);

const createInvoice = async () => {
  calculateTotal();
  try {
    const response = await authStore.fetchWithAuth(
      "http://localhost:5000/api/invoice",
      {
        method: "POST",
        headers: {
          "Content-type": "application/json",
        },
        body: invoice.value.toJson(),
      }
    );

    if (!response.ok) {
      const errorData = await response.json();
      const errorMessage =
        errorData.message || "Erreur lors de la création de la facture.";
      invoiceFailed();
      throw new Error(`Erreur ${response.status}: ${errorMessage}`);
    }

    const data = await response.json();
    invoiceSuccess();
    invoice.value = new Invoice();
    invoice.value.items.push(new InvoiceItem());
  } catch (error) {
    console.error("Erreur lors de la création de la facture:", error.message);
  }
};

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
    invoice.value.doctorId = userData._id;
  } catch (error) {
    console.error(
      "Erreur lors de la récupération de l'utilisateur:",
      error.message
    );
  }
};

onMounted(() => {
  invoice.value.items.push(
    new InvoiceItem({
      product: "",
      description: "",
      quantity: 1,
      price: 0,
      total: 0,
    })
  );
  fetchUser();
});

function receiveEmit(patientId) {
  invoice.value.patientId = patientId;
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

function invoiceSuccess() {
  Toast.fire({
    title: "Facture créé avec succès !",
    icon: "success",
  });
}

function invoiceFailed() {
  Toast.fire({
    title: "Facture annulé !",
    icon: "error",
  });
}
</script>

<template>
  <form
    @submit.prevent="createInvoice"
    ref="form"
    class="w-full p-10 overflow-y-auto h-screen"
  >
    <!-- Patient ID Section -->
    <div class="flex flex-wrap -mx-3 mb-6">
      <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
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
          v-model="invoice.patientId"
          @focus="displayDataChoosePatient"
          placeholder="Patient ID"
        />
      </div>
      <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
        <label
          class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
          for="grid-totalAmount"
        >
          Montant Total
        </label>
        <input
          class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white"
          id="grid-totalAmount"
          type="number"
          v-model="invoice.totalAmount"
          placeholder="Montant Total"
          readonly
        />
      </div>
    </div>

    <!-- Divider -->
    <hr class="border-t-2 border-gray-200 my-4" />

    <!-- Items Section -->
    <div class="flex flex-wrap -mx-3 mb-6">
      <div
        v-for="(item, index) in invoice.items"
        :key="index"
        class="w-full px-3 mb-6"
      >
        <div class="flex flex-wrap -mx-3 mb-2">
          <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
            <label
              class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
              for="grid-product"
            >
              Produit
            </label>
            <input
              class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white"
              id="grid-product"
              type="text"
              v-model="item.product"
              placeholder="Produit"
              required
            />
          </div>
          <div class="w-full md:w-1/2 px-3 mb-6 md:mb-0">
            <label
              class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
              for="grid-description"
            >
              Description
            </label>
            <input
              class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white"
              id="grid-description"
              type="text"
              v-model="item.description"
              placeholder="Description"
              required
            />
          </div>
          <div class="w-full md:w-1/4 px-3 mb-6 md:mb-0">
            <label
              class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
              for="grid-quantity"
            >
              Quantité
            </label>
            <input
              class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white"
              id="grid-quantity"
              type="number"
              v-model="item.quantity"
              placeholder="Quantité"
              min="0"
              required
            />
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
              v-model="item.price"
              placeholder="Prix"
              min="0"
              required
            />
          </div>
        </div>
        <div class="flex flex-wrap -mx-3 mb-2">
          <div class="w-full md:w-1/4 px-3 mb-6 md:mb-0">
            <label
              class="block uppercase tracking-wide text-gray-700 text-xs font-bold mb-2"
              for="grid-total"
            >
              Total
            </label>
            <input
              class="appearance-none block w-full bg-gray-300 text-gray-700 border rounded py-3 px-4 mb-3 leading-tight focus:outline-none focus:bg-white"
              id="grid-total"
              type="number"
              :value="item.quantity * item.price"
              placeholder="Total"
              readonly
            />
          </div>
          <div class="w-full md:w-1/4 px-3 mb-6 md:mb-0 flex items-center">
            <button
              type="button"
              @click="removeItem(index)"
              class="bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded"
            >
              Supprimer
            </button>
          </div>
        </div>
        <!-- Divider -->
        <hr
          class="border-t-2 border-gray-200 my-4"
          v-if="index < invoice.items.length - 1"
        />
      </div>
    </div>
    <div class="w-full px-3 mb-6">
      <button
        type="button"
        @click="addItem"
        class="bg-green-500 hover:bg-green-700 text-white font-bold py-2 px-4 rounded"
      >
        Ajouter un Item
      </button>
    </div>

    <!-- Divider -->
    <hr class="border-t-2 border-gray-200 my-4" />

    <div class="w-full px-3 mb-6">
      <button
        class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
        type="submit"
      >
        Créer Facture
      </button>
    </div>
  </form>

  <div
    class="w-screen h-screen z-50 absolute bg-gray-300 bg-opacity-50 flex justify-center items-center"
    v-if="displayChoosePatient"
    @click.self="removeChoosePatient"
  >
    <div
      class="w-11/12 h-5/6 bg-red-300 p-10 overflow-y-scroll relative rounded-lg"
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
        class="block uppercase tracking-wide text-gray-700 font-bold mb-10 text-3xl"
      >
        Sélectionner le patient
      </h3>
      <!-- <DossiersMedicaux :invoiceid="invoice.patientId" @update:invoice.patient-id="invoice.patientId = $event" /> -->

      <DossiersMedicaux
        :patient-id="invoice.patientId"
        @change-id="receiveEmit"
      />
    </div>
  </div>
</template>
