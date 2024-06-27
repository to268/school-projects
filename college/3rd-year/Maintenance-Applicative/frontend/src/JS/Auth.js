// src/stores/auth.js
import { defineStore } from 'pinia';
import { ref } from 'vue';

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
    title: "Il semblerait que votre authentification soit expiré. Veuillez vous reconnecter",
    icon: "error",
  });
}

export const useAuthStore = defineStore('auth', () => {
  const isAuthenticated = ref(false);

  function login(token) {
    localStorage.setItem('authToken', token);
    isAuthenticated.value = true;
  }

  function logout() {
    localStorage.removeItem('authToken');
    isAuthenticated.value = false;
  }

  function checkAuth() {
    const token = localStorage.getItem('authToken');
    isAuthenticated.value = !!token;
  }

  // Intercepter les requêtes API et gérer les erreurs d'authentification
  async function fetchWithAuth(url, options = {}) {
    const token = localStorage.getItem('authToken');
    if (!token) {
      logout();
      invalidToken();
      return;
    }

    options.headers = {
      ...options.headers,
      'Authorization': `Bearer ${token}`
    };

    const response = await fetch(url, options);
    if (response.status === 401) {
      logout();
      invalidToken();
    } else {
      return response;
    }
  }

  return { isAuthenticated, login, logout, checkAuth, fetchWithAuth  };
});