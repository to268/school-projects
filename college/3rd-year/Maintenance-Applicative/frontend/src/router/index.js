import { createRouter, createWebHistory } from 'vue-router'
import Acceuil from '../views/Accueil.vue'
import Dossiers from '../views/DossiersMedicaux.vue'
import OfficeActivity from '../views/OfficeActivity.vue'
import Agenda from '../views/Agenda.vue'
import FormulaireProfilPatient from '../views/FormulaireProfilPatient.vue'
import GestionDesPrescriptions from '../views/GestionDesPrescriptions.vue'
import FacturationPaiement from '../views/FacturationPaiement.vue'
import Cancel from '../views/stripe/Cancel.vue'
import Success from '../views/stripe/Success.vue'
import Login from '../views/account/Login.vue'
import Register from '../views/account/Register.vue'
import Profile from '../views/Profile.vue'
import InvoiceList from '../views/InvoiceList.vue'
import ListePrescription from '../views/ListePrescription.vue'
import { useAuthStore } from '../JS/Auth.js';


const routes = [
  {
    path: '/',
    name: 'Acceuil',
    component: Acceuil,
    meta: { requiresAuth: true }
  },
  {
    path: '/Agenda',
    name: 'Agenda',
    component: Agenda,
    meta: { requiresAuth: true }
  },
  {
    path: '/Dossiers',
    name: 'Dossiers',
    component: Dossiers,
    meta: { requiresAuth: true }
  },
  {
    path: '/activitecabinet',
    name: 'activitecabinet',
    component: OfficeActivity,
    meta: { requiresAuth: true }
  },
  {
    path: '/FormulaireProfilPatient',
    name: 'FormulaireProfilPatient',
    component: FormulaireProfilPatient,
    meta: { requiresAuth: true }
  },
  {
    path: '/GestionDesPrescriptions',
    name: 'GestionDesPrescriptions',
    component: GestionDesPrescriptions,
    meta: { requiresAuth: true }
  },
  {
    path: '/Cancel',
    name: 'Cancel',
    component: Cancel,
    meta: { requiresAuth: true }
  },
  {
    path: '/Success',
    name: 'Success',
    component: Success,
    meta: { requiresAuth: true }
  },
  {
    path: '/FacturationPaiement',
    name: 'FacturationPaiement',
    component: FacturationPaiement,
    meta: { requiresAuth: true }
  },

  {
    path: '/Login',
    name: 'Login',
    component: Login
  },

  {
    path: '/Register',
    name: 'Register',
    component: Register
  },

  {
    path: '/Profile',
    name: 'Profile',
    component: Profile,
    meta: { requiresAuth: true }
  },

  {
    path: '/InvoiceList',
    name: 'InvoiceList',
    component: InvoiceList,
    meta: { requiresAuth: true }
  },

  {
    path: '/ListePrescription',
    name: 'ListePrescription',
    component: ListePrescription,
    meta: { requiresAuth: true }
  },
]

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL), // Utilisez import.meta.env.BASE_URL
  routes
})

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


function invalidToken() {
  Toast.fire({
    title: "Il semblerait que votre authentification est expiré. Veuillez vous reconnecter",
    icon: "error",
  });
}

router.beforeEach((to, from, next) => {
  const authStore = useAuthStore();
  authStore.checkAuth();
  if (to.meta.requiresAuth && !authStore.isAuthenticated) {
    next('/login');
    invalidToken();
  } else {
    next();
  }
});

export default router
