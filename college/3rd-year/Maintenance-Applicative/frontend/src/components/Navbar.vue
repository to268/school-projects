<template>
   <div>
      <!-- Bouton de bascule pour la barre latérale -->
      <button @click="toggleSidebar" class="sm:hidden p-2 text-white bg-app">
         <i class="bi bi-heart-pulse-fill"></i>
      </button>

      <aside
         :class="['transition-transform bg-app', sidebarOpen ? 'translate-x-0' : '-translate-x-full', 'sm:translate-x-0 sm:w-1/10 h-screen']"
         aria-label="Sidebar">
         <div class="h-full px-3 py-4 overflow-y-auto">
            <div class="w-full flex justify-center">
               <h1 class="text-2xl mb-2">HealthCarePro <i class="bi bi-heart-pulse-fill"></i></h1>
            </div>
            <hr class="mb-2">
            <ul class="space-y-2 font-medium">
               <!-- Éléments de menu communs -->
               <li>
                  <a href="/"
                     class="flex items-center p-2 text-gray-900 rounded-lg hover-item-menu dark:hover:bg-gray-700 group">
                     <span class="flex-1 ms-3 whitespace-nowrap font-medium text-base text-white"><i
                           class="bi bi-house-fill"></i> Accueil</span>
                  </a>
               </li>
               <li>
                  <a href="/Profile"
                     class="flex items-center p-2 text-gray-900 rounded-lg hover-item-menu dark:hover:bg-gray-700 group">
                     <span class="flex-1 ms-3 whitespace-nowrap font-medium text-base text-white"><i
                           class="bi bi-person-square"></i> Profil</span>
                  </a>
               </li>
               <li>
                  <a href="/Agenda"
                     class="flex items-center p-2 text-gray-900 rounded-lg hover-item-menu dark:hover:bg-gray-700 group">
                     <span class="flex-1 ms-3 whitespace-nowrap font-medium text-base text-white"><i
                           class="bi bi-calendar3"></i> Agenda</span>
                  </a>
               </li>

               <!-- Éléments de menu conditionnels basés sur le rôle de l'utilisateur -->
               <template v-if="userData">
                  <li v-if="userData.role === 'doctor'">
                     <a href="/Dossiers"
                        class="flex items-center p-2 text-gray-900 rounded-lg hover-item-menu dark:hover:bg-gray-700 group">
                        <span class="flex-1 ms-3 whitespace-nowrap font-medium text-base text-white"><i
                              class="bi bi-journal-medical"></i> Dossiers Médicaux</span>
                     </a>
                  </li>
                  <li v-if="userData.role === 'doctor'">
                     <a href="/ActiviteCabinet"
                        class="flex items-center p-2 text-gray-900 rounded-lg hover-item-menu dark:hover:bg-gray-700 group">
                        <span class="flex-1 ms-3 whitespace-nowrap font-medium text-base text-white"><i
                              class="bi bi-bar-chart"></i> Activité du cabinet</span>
                     </a>
                  </li>
                  <li v-if="userData.role === 'doctor'">
                     <a href="/FormulaireProfilPatient"
                        class="flex items-center p-2 text-gray-900 rounded-lg hover-item-menu dark:hover:bg-gray-700 group">
                        <span class="flex-1 ms-3 whitespace-nowrap font-medium text-base text-white"><i
                              class="bi bi-person-vcard"></i> Formulaire profil patient</span>
                     </a>
                  </li>
                  <li v-if="userData.role === 'doctor'">
                     <a href="/GestionDesPrescriptions"
                        class="flex items-center p-2 text-gray-900 rounded-lg hover-item-menu dark:hover:bg-gray-700 group">
                        <span class="flex-1 ms-3 whitespace-nowrap font-medium text-base text-white"><i
                              class="bi bi-prescription2"></i> Gestion des prescriptions</span>
                     </a>
                  </li>
                  <li>
                     <a href="/ListePrescription"
                        class="flex items-center p-2 text-gray-900 rounded-lg hover-item-menu dark:hover:bg-gray-700 group">
                        <span class="flex-1 ms-3 whitespace-nowrap font-medium text-base text-white"><i
                              class="bi bi-card-list"></i> Liste des prescriptions</span>
                     </a>
                  </li>
                  <li v-if="userData.role === 'doctor'">
                     <a href="/FacturationPaiement"
                        class="flex items-center p-2 text-gray-900 rounded-lg hover-item-menu dark:hover:bg-gray-700 group">
                        <span class="flex-1 ms-3 whitespace-nowrap font-medium text-base text-white"><i
                              class="bi bi-credit-card-2-back-fill"></i> Facturation et Paiement</span>
                     </a>
                  </li>
                  <li v-if="userData.role === 'doctor' || userData.role === 'patient'">
                     <a href="/InvoiceList"
                        class="flex items-center p-2 text-gray-900 rounded-lg hover-item-menu dark:hover:bg-gray-700 group">
                        <span class="flex-1 ms-3 whitespace-nowrap font-medium text-base text-white"><i
                              class="bi bi-receipt"></i> Liste des factures</span>
                     </a>
                  </li>
               </template>
            </ul>
         </div>
      </aside>
   </div>
</template>


<script setup>
import { ref, onMounted } from 'vue';
import { useAuthStore } from '../JS/Auth.js';

const authStore = useAuthStore();
let userData = ref(null);
let sidebarOpen = ref(false);

const toggleSidebar = () => {
   sidebarOpen.value = !sidebarOpen.value;
};

onMounted(async () => {
   try {
      const response = await authStore.fetchWithAuth('http://localhost:5000/api/users/me', {
         method: 'GET',
         headers: {
            'Content-type': 'application/json',
         },
      });

      if (!response.ok) {
         const errorData = await response.json();
         const errorMessage = errorData.message || 'Erreur lors de la récupération de l\'utilisateur.';
         throw new Error(`Erreur ${response.status}: ${errorMessage}`);
      }

      const fetchedData = await response.json();
      userData.value = fetchedData;
   } catch (error) {
      console.error('Erreur lors de la récupération de l\'utilisateur:', error.message);
   }
});
</script>
