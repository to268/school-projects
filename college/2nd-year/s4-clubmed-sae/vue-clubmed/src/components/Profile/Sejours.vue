<template>
  <div v-if="reservations" class="mainsejour">
    <div v-for="resa in reservations" :key="resa.reservationId" class="sejour" @click="click(resa)">
      <img
        v-if="getResort(resa.resortId)"
        :src="getResort(resa.resortId).lesPhotos[0].lien"
        alt="resort"
      />
      <div class="infos">
        <h2 v-if="getResortById(resa.resortId)">{{ getResortById(resa.resortId).nom }}</h2>
        <p>Date début: {{ getDate(resa.dateDebut) }}</p>
        <p>Date fin: {{ getDate(resa.dateFin) }}</p>
      </div>
    </div>
  </div>
  <DetailsSejour v-if="activated" :resa="currentResa" />
</template>

<script>
import DetailsSejour from '@/components/Profile/DetailsSejour.vue';

import { mapGetters, mapState } from 'vuex';

export default {
  name: 'Sejours',
  data() {
    return {
      activated: false,
      currentResa: {}
    };
  },
  components: {
    DetailsSejour
  },
  methods: {
    click(resa) {
      this.currentResa = resa;
      this.activated = !this.activated;
    },
    getPhoto(id) {
      return this.getPhotoById(id);
    },
    getResort(id) {
      return this.getResortById(id);
    },
    getDate(str) {
      let date = new Date(str);
      return date.getDay() + '/' + date.getMonth() + '/' + date.getFullYear();
    }
  },
  created() {
    if (this.reservations.length === 0 && this.client) {
      this.$store.dispatch('reservation/fetchReservations', this.client.lesReservations);
    }
  },
  updated() {
    if (this.sejourResorts.length === 0 && this.reservations.length !== 0) {
      this.$store.dispatch(
        'resort/fetchSejourResorts',
        this.reservations.map((item) => item.resortId)
      );
    }
  },
  computed: {
    ...mapState({
      client: (state) => {
        return state.currentClient;
      },
      reservations: (state) => {
        return state.reservation.reservations;
      },
      sejourResorts: (state) => state.resort.sejourResorts
    }),
    ...mapGetters({
      getResortById: 'resort/sejourGetById',
      getPhotoById: 'photo/getById'
    })
  }
};
</script>

<style scoped>
.mainsejour {
  width: 100%;
  height: 400px;
  margin: 20px 0 20px 0;
  display: flex;
  overflow: scroll;
}

.sejour {
  width: 100%;
  height: 100%;
  margin-right: 20px;
  background: whitesmoke;
  display: flex;
  align-items: center;
  justify-content: start;
  flex-direction: column;
}

.sejour:hover {
  cursor: pointer;
}

.sejour > img {
  width: 400px;
}

.sejour.infos {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: space-evenly;
  flex-grow: 1;
}
</style>
