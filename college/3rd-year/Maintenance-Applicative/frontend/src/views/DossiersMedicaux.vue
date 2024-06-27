<script setup>
import { ref, onMounted } from "vue";
import { Invoice, InvoiceItem } from "../models/Invoice.js";
import { useAuthStore } from "../JS/Auth.js";
const authStore = useAuthStore();

const patients = ref([]);

// let props = defineProps({
//     patient: {
//             type: String,
//             default() {
//                 return null
//             }

//         }
// })

// let invoiceId = ref(props.invoiceId)
let profile = ref(null);
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
    profile.value = userData;
  } catch (error) {
    console.error(
      "Erreur lors de la récupération de l'utilisateur:",
      error.message
    );
  }
};

// lifecycle hooks
onMounted(async () => {
  await fetchUser();
  authStore
    .fetchWithAuth(
      "http://localhost:5000/api/users/patients/" + profile.value._id
    )
    .then((response) => response.json())
    .then((result) => {
      patients.value = result; // Assigner les données récupérées à la variable réactive
      $(document).ready(function () {
        var test = new DataTable("#datamedical", {
          scrollY: "60vh",
          responsive: true,
          autoFill: {
            columns: ":not(:first-child)",
          },
          retrieve: true,
        });
      });
    })
    .catch((error) => {
      console.error("Erreur lors de la récupération des données:", error);
    });
});
// Method to truncate Date.parse(a string
const truncate = (string, length) => {
  return string.length > length ? string.substring(0, length) : string;
};

// Define the emit function
const emit = defineEmits(["change-id"]);

function passId(id) {
  emit("change-id", id);
}

function getHealth(health) {
  switch (health) {
    case 0:
      return "Mort : Aucun signe de vie. L'état est critique et nécessite une intervention immédiate.";
    case 1:
      return "Critique : La santé est gravement compromise. Nécessite des soins médicaux urgents.";
    case 2:
      return "Blessé : Des blessures significatives sont présentes. Nécessite des soins médicaux.";
    case 3:
      return "Contusionné : Des contusions et des douleurs, mais l'état n'est pas critique.";
    case 4:
      return "En bonne santé : Légères gênes possibles, mais la santé générale est bonne.";
    case 5:
      return "Parfait : En excellente santé, aucune blessure ou maladie apparente.";
  }
  return "Error invalid health value";
}
</script>

<template>
  <div class="bg-app bg-opacity-80 overflow-y-auto h-full w-full py-20 px-8">
    <table id="datamedical" class="table table-striped cell-border hover stripe" style="width: 100%">
      <thead>
        <tr>
          <th>Patient</th>
          <th>Mail</th>
          <th>Etat de santé</th>
          <th>Notes</th>
          <th>Maladies</th>
          <th>Allergies</th>
          <th>Chirurgies</th>
          <th>Diagnostiques</th>
          <th>Dernier rendez vous</th>
        </tr>
      </thead>
      <tbody>
        <tr v-if="patients != null" v-for="patient in patients" @click="passId(patient.medicalFile.patient._id)"
          class="hover:bg-app cursor-pointer">
          <td>
            {{
          patient.medicalFile.patient.firstName +
          " " +
          patient.medicalFile.patient.lastName
        }}
            {{
            new Date(
              patient.medicalFile.patient.birthDate
            ).toLocaleDateString("fr-FR")
          }}
          </td>
          <td>
            {{ patient.medicalFile.patient.email }}
          </td>
          <td>
            <p style="font-size: 1px; color: transparent">
              {{ patient.medicalFile.health }}
            </p>
            {{ getHealth(patient.medicalFile.health) }}
          </td>
          <td>
            {{ patient.medicalFile.notes }}
          </td>
          <td>
            <ul v-for="disease in patient.medicalFile.diseases" :key="disease._id">
              <li>{{ disease.name }}</li>
              <ul>
                <li>
                  {{
          new Date(disease.diagnosisDate).toLocaleDateString("fr-FR")
        }}
                </li>
                <li>{{ disease.details }}</li>
              </ul>
            </ul>
          </td>
          <td>
            <ul v-for="allergy in patient.medicalFile.allergies">
              <li>{{ allergy.allergy }}</li>
              <ul>
                <li>{{ allergy.reaction }}</li>
                <li>{{ allergy.severity }}</li>
              </ul>
            </ul>
          </td>
          <td>
            <ul v-for="surgery in patient.medicalFile.surgery">
              <li>{{ surgery.type }}</li>
              <ul>
                <li>
                  {{ new Date(surgery.date).toLocaleDateString("fr-FR") }}
                </li>
                <li>{{ surgery.details }}</li>
              </ul>
            </ul>
          </td>
          <td>
            <ul v-for="diagnostic in patient.medicalFile.diagnostics">
              <li>{{ diagnostic.diagnostic }}</li>
              <ul>
                <li>
                  {{ new Date(diagnostic.date).toLocaleDateString("fr-FR") }}
                </li>
                <li>{{ diagnostic.treatment }}</li>
                <li>{{ diagnostic.notes }}</li>
              </ul>
            </ul>
          </td>
          <td>
            <p style="font-size: 1px; color: transparent">
              {{
          new Date(patient.appointments[0].startDate)
            .toISOString()
            .split("T")[0]
        }}
            </p>
            {{
            new Date(patient.appointments[0].startDate).toLocaleDateString(
              "fr-FR"
            )
            }}
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<style>
td>ul {
  list-style-type: disc;
  padding: 10px;
}

td>ul>ul {
  padding: 10px;
  list-style-type: circle;
}
</style>

<script>
export default {
  // Options du composant/vue
  mounted() {
    this.loadExternalScripts();
  },
  methods: {
    loadExternalScripts() {
      // Chargement des scripts externes
      this.loadScript("https://cdn.datatables.net/2.0.8/js/dataTables.min.js");
      //   this.loadStylesheet('https://cdn.datatables.net/2.0.8/css/dataTables.tailwindcss.css');
      //   this.loadScript('https://cdn.datatables.net/2.0.8/js/dataTables.tailwindcss.js');
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
      const link = document.createElement("link");
      link.rel = "stylesheet";
      link.type = "text/css";
      link.href = href;
      document.head.appendChild(link);
    },
  },
};
</script>
