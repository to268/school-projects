<script setup>
import { ref, onMounted } from "vue";
import { Invoice } from "../models/Invoice.js"; // Assurez-vous que le chemin est correct
import { useAuthStore } from "../JS/Auth.js";
import { User } from "../models/User.js";

const authStore = useAuthStore();
let profile = ref(null);
let userData;
let calendar;
const URL_ALL_USERS = 'http://localhost:5000/api/users/all'

async function fetchUsers(url) {
  try {
    const response = await authStore.fetchWithAuth(url, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    });

    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }

    const data = await response.json(); // Lire et parser le JSON de la réponse
    return data; // Retourner les données
  } catch (error) {
    console.error("There was an error with the fetch operation:", error);
    return []; // Retourner un tableau vide en cas d'erreur
  }
}
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
    // Appeler la fonction pour récupérer les utilisateurs et attendre les données
    const users = await fetchUsers(URL_ALL_USERS);

    // Filtrer les utilisateurs avec le rôle "doctor"
    const doctors = users.filter(user => user.role === 'doctor');
    const patients = users.filter(user => user.role === 'patient');

    // Afficher les docteurs dans la console
    console.log(doctors);

    // Ajouter les docteurs à l'élément <select> avec l'ID 'select_docteurs'



    userData = await response.json();
    var appointment;

    if (userData.role == "doctor") {
      appointment = await authStore.fetchWithAuth(
        "http://localhost:5000/api/appointment/getByDoctorId/" + userData._id,
        {
          method: "GET",
          headers: {
            "Content-type": "application/json",
          },
        }
      );
      $('#select_docteurs').append(new Option(userData.firstName + " " + userData.lastName, userData._id, true, true));
      patients.forEach(patient => {
        $('#select_patients').append(new Option(`${patient.firstName} ${patient.lastName}`, patient._id));
      });
    }
    else if (userData.role == "patient") {
      appointment = await authStore.fetchWithAuth('http://localhost:5000/api/appointment/getByPatientId/' + userData._id, {
        method: 'GET',
        headers: {
          'Content-type': 'application/json'
        }
      });
      $('#select_patients').append(new Option(userData.firstName + " " + userData.lastName, userData._id, true, true));
      doctors.forEach(doctor => {
        $('#select_docteurs').append(new Option(`${doctor.firstName} ${doctor.lastName}`, doctor._id));
      });
    }

    const dataAppointement = await appointment.json();
    dataAppointement.forEach((element) => {
      let newEvent = {
        id: element._id,
        title: element.reason,
        start: new Date(element.startDate),
        end: new Date(element.endDate),
        color: element.color, // Couleur spécifique pour cet événement
        classNames: "cursor-pointer hover:opacity-50",
      };
      calendar.addEvent(newEvent);
    });

    profile.value = new User(userData);
  } catch (error) {
    console.error(
      "Erreur lors de la récupération de l'utilisateur:",
      error.message
    );
  }
};
onMounted(fetchUser);
async function deleteAppointment(id) {
  try {
    const response = await authStore.fetchWithAuth(
      `http://localhost:5000/api/appointment/${id}`,
      {
        method: "DELETE",
        headers: {
          "Content-Type": "application/json",
        },
      }
    );

    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }

    const result = await response.json();
    return result;
  } catch (error) {
    console.error(
      "Erreur lors de la suppression du rendez-vous :",
      error.message
    );
    throw error;
  }
}
$(document).ready(function () {
  $("#modal_event").hide();

  var calendarEl = $("#calendar");
  const Toast = Swal.mixin({
    toast: true,
    position: "top-end",
    showConfirmButton: false,
    timer: 3000,
    timerProgressBar: true,
    didOpen: (toast) => {
      toast.onmouseenter = Swal.stopTimer;
      toast.onmouseleave = Swal.resumeTimer;
    },
  });
  calendar = new FullCalendar.Calendar(calendarEl[0], {
    initialView: "dayGridMonth",
    locale: "fr",
    firstDay: 1,
    height: 700,
    headerToolbar: {
      left: "prev,next today",
      center: "title",
      right: "dayGridMonth,timeGridWeek,timeGridDay",
    },
    buttonText: {
      today: "Aujourd'hui",
      month: "Mois",
      week: "Semaine",
      day: "Jour",
      list: "Liste",
    },
    eventClick: function (info) {
      Swal.fire({
        title: "Voulez-vous vraiment supprimer cet évènement ?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Oui",
        cancelButtonText: "Non",
        position: "top",
      }).then((result) => {
        if (result.isConfirmed) {
          Toast.fire({
            title: "Supprimé avec succès!",
            icon: "success",
          });
          info.event.remove();

          // Appeler la fonction pour supprimer le rendez-vous
          deleteAppointment(info.event.id)
            .then((response) => { })
            .catch((error) => {
              console.error(
                "Erreur lors de la suppression du rendez-vous :",
                error.message
              );
            });
        }
      });
    },
  });

  calendar.setOption("locale", "fr");
  calendar.render();
  $(".fc-today-button").addClass("btn_calendar");
  $(".fc-dayGridMonth-button").addClass("btn_calendar");
  $(".fc-timeGridWeek-button").addClass("btn_calendar");
  $(".fc-timeGridDay-button").addClass("btn_calendar");
  $(".fc-prev-button").addClass("btn_calendar");
  $(".fc-next-button").addClass("btn_calendar");

  $(".btn_calendar").on("click", () => {
    $(".fc-today-button").addClass("btn_calendar border-2");
    $(".fc-dayGridMonth-button").addClass("btn_calendar border-2");
    $(".fc-timeGridWeek-button").addClass("btn_calendar border-2");
    $(".fc-timeGridDay-button").addClass("btn_calendar border-2");
    $(".fc-prev-button").addClass("btn_calendar border-2");
    $(".fc-next-button").addClass("btn_calendar border-2");
  });
  $("#btn_add_event").on("click", () => {
    $("#modal_event").fadeIn(100);
  });

  $("#btn-close").on("click", () => {
    $("#modal_event").fadeOut(100);
  });
  $("#input_date_range").daterangepicker({
    closeText: "Fermer",
    prevText: "Précédent",
    nextText: "Suivant",
    currentText: "Aujourd'hui",
    monthNames: [
      "janvier",
      "février",
      "mars",
      "avril",
      "mai",
      "juin",
      "juillet",
      "août",
      "septembre",
      "octobre",
      "novembre",
      "décembre",
    ],
    monthNamesShort: [
      "janv.",
      "févr.",
      "mars",
      "avril",
      "mai",
      "juin",
      "juil.",
      "août",
      "sept.",
      "oct.",
      "nov.",
      "déc.",
    ],
    dayNames: [
      "dimanche",
      "lundi",
      "mardi",
      "mercredi",
      "jeudi",
      "vendredi",
      "samedi",
    ],
    dayNamesShort: ["dim.", "lun.", "mar.", "mer.", "jeu.", "ven.", "sam."],
    dayNamesMin: ["D", "L", "M", "M", "J", "V", "S"],
    weekHeader: "Sem.",
    dateFormat: "dd/mm/yy",
    firstDay: 1,
    isRTL: false,
    showMonthAfterYear: false,
    yearSuffix: "",
  });

  $("#btn_save").on("click", () => {
    let date_heure_debut =
      $("#input_date_range")
        .data("daterangepicker")
        .startDate.format("YYYY-MM-DD") +
      " " +
      $("#heure_debut").val();
    let date_heure_fin =
      $("#input_date_range")
        .data("daterangepicker")
        .endDate.format("YYYY-MM-DD") +
      " " +
      $("#heure_fin").val();

    let newEvent = {
      id: 1,
      title: $("#titre_evenement").val(),
      start: date_heure_debut,
      end: date_heure_fin,
      color: $("#couleur_evenement").val(), // Couleur spécifique pour cet événement
      classNames: "cursor-pointer hover:opacity-50",
    };
    Toast.fire({
      title: "Enregistré avec succès !",
      icon: "success",
    });
    calendar.addEvent(newEvent);
    (async () => {
      // Appeler la fonction pour récupérer les utilisateurs et attendre les données
      const docteur = await fetchUsers(
        "http://localhost:5000/api/users/" + $("#select_docteurs").val()
      );
      const patient = await fetchUsers(
        "http://localhost:5000/api/users/" + $("#select_patients").val()
      );
      const appointmentData = {
        doctor: docteur[0]["_id"],
        patient: patient[0]["_id"],
        startDate: date_heure_debut,
        endDate: date_heure_fin,
        reason: $("#titre_evenement").val(),
        description: $("#description").trumbowyg("html"),
        prix: $("#prix").val(),
        color: $("#couleur_evenement").val(),
      };
      const reponsePost = await createNewAppointment(appointmentData);
    })();
    // createAppointment();
    $("#modal_event").fadeOut();
  });

  const URL_ALL_USERS = "http://localhost:5000/api/users/all";
  async function createAppointment(appointmentData) {
    try {
      const response = await authStore.fetchWithAuth(
        "http://localhost:5000/api/appointment",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(appointmentData),
        }
      );

      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }

      const savedAppointment = await response.json();
      return savedAppointment;
    } catch (error) {
      console.error(
        "Erreur lors de la création du rendez-vous :",
        error.message
      );
      throw error; // Rethrow l'erreur pour gérer plus tard si nécessaire
    }
  }

  // Exemple d'utilisation de la fonction createAppointment
  async function createNewAppointment(appointmentData) {
    try {
      const savedAppointment = await createAppointment(appointmentData);
    } catch (error) {
      console.error("Erreur lors de la création du rendez-vous :", error);
    }
  }



});
</script>
<template>
  <div class="w-full h-screen justify-center overflow-y-auto relative">
    <Modal></Modal>

    <div>
      <button id="btn_add_event"
        class="p-3 text-white ml-3 rounded bg-app hover:opacity-50 m-2 whitespace-nowrap text-base">
        <i class="bi bi-calendar2-plus"></i> Création d'un Rendez-vous
      </button>
    </div>
    <div class="h-5/6 m-3">
      <div id="calendar"
        class="animate__animated animate__fadeIn w-full shadow-lg bg-white/90 text-black h-full rounded backdrop-blur p-5">
      </div>
    </div>
  </div>
</template>

<script>
import Modal from "../components/Modal.vue";

export default {
  components: {
    Modal,
  },
  // Options du composant/vue
  mounted() {
    this.loadExternalScripts().then(() => {
      this.initializeTrumbowyg();
    });
  },
  methods: {
    loadExternalScripts() {
      // Chargement des scripts externes
      const scriptsToLoad = [
        "https://cdn.jsdelivr.net/momentjs/latest/moment.min.js",
        "https://cdn.jsdelivr.net/npm/daterangepicker/daterangepicker.min.js",
        "https://cdnjs.cloudflare.com/ajax/libs/Trumbowyg/2.27.3/trumbowyg.min.js",
      ];

      const stylesheetsToLoad = [
        "https://cdn.jsdelivr.net/npm/daterangepicker/daterangepicker.css",
        "https://cdnjs.cloudflare.com/ajax/libs/Trumbowyg/2.27.3/ui/trumbowyg.min.css",
      ];

      const loadScriptPromises = scriptsToLoad.map(this.loadScript);
      const loadStylesheetPromises = stylesheetsToLoad.map(this.loadStylesheet);

      return Promise.all([...loadScriptPromises, ...loadStylesheetPromises]);
    },
    loadScript(src) {
      return new Promise((resolve, reject) => {
        const script = document.createElement("script");
        script.src = src;
        script.onload = resolve;
        script.onerror = reject;
        document.body.appendChild(script);
      });
    },
    loadStylesheet(href) {
      return new Promise((resolve, reject) => {
        const link = document.createElement("link");
        link.rel = "stylesheet";
        link.type = "text/css";
        link.href = href;
        link.onload = resolve;
        link.onerror = reject;
        document.head.appendChild(link);
      });
    },
    initializeTrumbowyg() {
      $("#description").trumbowyg();
    },
  },
};
</script>
