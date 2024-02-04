<template>
  <div v-if="client" class="formdiv">
    <div class="form">
      <Dates />
      <Participants />
      <Transport :resort="resort" />
      <RoomType :resort="resort" />
      <Activities :resort="resort" />
      <div class="pricediv">
        <h1>Total :</h1>
        <div class="price">
          <h1>2000 €</h1>
        </div>
      </div>
      <div @click="book" class="book">
        <div>Reserver</div>
      </div>
    </div>
  </div>
  <div v-else class="connect">
    <h1>Vous devez être connecté pour reserver</h1>
    <router-link :to="{ name: 'LogIn' }">Se connecter</router-link>
  </div>
</template>

<script>
import RoomType from '@/components/Reservation/RoomType.vue';
import Dates from '@/components/Reservation/Dates.vue';
import Participants from '@/components/Reservation/Participants.vue';
import Activities from '@/components/Reservation/Activities.vue';
import Transport from '@/components/Reservation/Transport.vue';

import { mapState, mapGetters } from 'vuex';

export default {
  name: 'Reservation',
  props: ['resortId'],
  components: {
    RoomType,
    Dates,
    Participants,
    Activities,
    Transport
  },
  methods: {
    book() {
      this.$store.dispatch('reservation/reserver');
    }
  },
  computed: {
    resort() {
      if (this.resortId) {
        return this.getResortById(parseInt(this.resortId));
      }
    },
    ...mapGetters({
      getResortById: 'resort/getById'
    }),
    ...mapState({
      client: (state) => state.currentClient,
      resorts: (state) => state.resort.resorts,
      reservation: (state) => state.reservation.currentReservation,
      reservations: (state) => state.reservation.reservations
    })
  }
};
</script>

<style scoped>
.formdiv {
  display: flex;
  justify-content: center;
  align-items: center;
  margin: 30px 0 30px 0;
}

.form {
  padding: 20px;
  border: 2px solid;
  border-radius: 20px;
  display: flex;
  flex-direction: column;
  justify-content: space-around;
  align-items: center;
}

.pricediv {
  display: flex;
  justify-content: space-between;
  align-items: center;
  width: 250px;
  margin-top: 20px;
}

.price {
  border: 1px solid;
  padding: 10px;
  border-radius: 10px;
}

.book {
  display: flex;
  justify-content: center;
  align-items: center;
  margin-top: 20px;
  border: 2px solid #1b4397;
  border-radius: 20px;
  padding: 20px;
  background-color: #1b4397;
}

.book:hover {
  cursor: pointer;
}

.book > div {
  color: white;
  text-decoration: none;
  font-size: 2em;
}

.connect {
  height: 80vh;
  display: flex;
  justify-content: center;
  align-items: center;
  font-size: 2em;
  flex-direction: column;
}
</style>
