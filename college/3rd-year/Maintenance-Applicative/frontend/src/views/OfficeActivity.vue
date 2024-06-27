<script setup>
import { ref, onMounted } from 'vue'
import { Invoice, InvoiceItem } from '../models/Invoice.js'
import { useAuthStore } from '../JS/Auth.js';
const authStore = useAuthStore();

const appointmentsWithDoctors = ref([])
const office = ref([])

let now = ref()
now.value = new Date(Date.now());

let user = ref()
const fetchUser = async () => {
    try {
        const response = await authStore.fetchWithAuth('http://localhost:5000/api/users/me', {
            method: 'GET',
            headers: {
                'Content-type': 'application/json'
            }
        });

        if (!response.ok) {
            const errorData = await response.json();
            const errorMessage = errorData.message || 'Erreur lors de la récupération de l\'utilisateur.';
            throw new Error(`Erreur ${response.status}: ${errorMessage}`);
        }

        let userData = await response.json();
        user.value = userData
    } catch (error) {
        console.error('Erreur lors de la récupération de l\'utilisateur:', error.message);
    }
};

// lifecycle hooks
onMounted(async () => {
    await fetchUser()
    await authStore.fetchWithAuth('http://localhost:5000/api/office/getDoctorByOffice/' + user.value.office)
        .then(response => response.json())
        .then(result => {
            appointmentsWithDoctors.value = result.appointments // Assigner les données récupérées à la variable réactive
            office.value = result.office // Assigner les données récupérées à la variable réactive

            $(document).ready(function () {
                var test = new DataTable('#datamedical', {
                    scrollY: '60vh',
                    responsive: true,
                    autoFill: {
                        columns: ':not(:first-child)'
                    },
                    retrieve: true,
                });
            });

        })
        .catch(error => {
            console.error('Erreur lors de la récupération des données:', error)
        })

    // Préparer les données pour le graphique
    const appointmentsByMonthThisYear = Array(12).fill(0); // 12 mois, initialisés à 0
    const appointmentsByMonthAllYear = Array(12).fill(0); // 12 mois, initialisés à 0
    appointmentsWithDoctors.value.forEach(appointment => {
        const date = new Date(appointment.appointment.startDate);
        const month = date.getMonth(); // 0-11
        if (now.value.getFullYear() == date.getFullYear()) {
            appointmentsByMonthThisYear[month]++;

        }
        appointmentsByMonthAllYear[month]++;
    })

    // Configuration du graphique
    const ctx = document.getElementById('appointmentsChartThisYear').getContext('2d');
    const appointmentsChartThisYear = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
            datasets: [{
                label: 'Nombre de rendez-vous',
                data: appointmentsByMonthThisYear,
                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                borderColor: 'rgba(255, 99, 132, 1)',
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    })

    const ctxAllYear = document.getElementById('appointmentsChartAllYear').getContext('2d');
    const appointmentsChartAllYear = new Chart(ctxAllYear, {
        type: 'bar',
        data: {
            labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
            datasets: [{
                label: 'Nombre de rendez-vous',
                data: appointmentsByMonthAllYear,
                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                borderColor: 'rgba(255, 99, 132, 1)',
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
})
// Method to truncate Date.parse(a string
const truncate = (string, length) => {
    return string.length > length ? string.substring(0, length) : string;
};
</script>


<template>
    <div class="bg-transparent overflow-y-auto h-full w-full py-20 px-8">
        <div
            class="max-w-4xl bg-white mx-auto  bg-opacity-70 text-app backdrop-filter backdrop-blur-lg rounded-lg shadow-2xl p-10">
            <h1 class="text-5xl font-extrabold  mb-12 text-center">{{ office.name }}</h1>

            <div class="mb-12">
                <h2 class="text-3xl font-semibold  mb-6 text-center">Nombre de rendez-vous sur toutes les
                    années</h2>
                <p class="text-2xl  text-center mb-4">{{ appointmentsWithDoctors.length }}</p>
            </div>

            <div class="mb-12">
                <h2 class="text-3xl font-semibold  mb-6 text-center">Graphique des rendez-vous par mois pour
                    l'année {{ now.getFullYear() }}</h2>
                <canvas id="appointmentsChartThisYear" class="bg-white bg-opacity-20 rounded-lg shadow-lg p-6"></canvas>
            </div>

            <div class="mb-12">
                <h2 class="text-3xl font-semibold  mb-6 text-center">Graphique des rendez-vous pour toutes les
                    années</h2>
                <canvas id="appointmentsChartAllYear" class="bg-white bg-opacity-20 rounded-lg shadow-lg p-6"></canvas>
            </div>

            <div>
                <h3 class="text-2xl font-medium  mb-8 text-center">Différents types de rendez-vous</h3>
                <div v-for="appointmentsWithDoctor in appointmentsWithDoctors"
                    class="mb-8 p-8 bg-white bg-opacity-20 rounded-lg shadow-lg hover:shadow-2xl transition-shadow duration-300">
                    <p class="text-xl  mb-4 font-semibold">{{ appointmentsWithDoctor.appointment.reason }} ({{
                new Date(appointmentsWithDoctor.appointment.startDate).toLocaleDateString('fr-FR') }})</p>
                    <p class="text-lg ">{{ appointmentsWithDoctor.appointment.description }}</p>
                </div>
            </div>
        </div>
    </div>

</template>


<script>

export default {
    // Options du composant/vue
    mounted() {
        this.loadExternalScripts();
    },
    methods: {
        loadExternalScripts() {
            // Chargement des scripts externes
            this.loadScript('https://cdn.jsdelivr.net/npm/chart.js');
            this.loadStylesheet('https://cdn.jsdelivr.net/npm/tailwindcss@2.2.19/dist/tailwind.min.css');
        },
        loadScript(src) {
            return new Promise((resolve, reject) => {
                const script = document.createElement('script');
                script.src = src;
                script.onload = resolve;
                script.onerror = reject;
                document.body.appendChild(script);
            });
        },
        loadStylesheet(href) {
            const link = document.createElement('link');
            link.rel = 'stylesheet';
            link.type = 'text/css';
            link.href = href;
            document.head.appendChild(link);
        }
    }
}
</script>
