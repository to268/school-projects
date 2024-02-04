<template>
  <div v-if="resort && transports" class="main">
    <div class="title">
      <h1>Choisissez votre transport</h1>
      <div class="border" />
    </div>
    <select v-model="reservation.transportId" name="transport">
      <option
        v-for="transport in transports"
        :value="transport.transportId"
        :key="transport.transportId"
      >
        {{ transport.libelle }}
      </option>
    </select>
  </div>
</template>

<script>
import { mapState } from 'vuex';

export default {
  name: 'Transport',
  props: ['resort'],
  computed: {
    ...mapState({
      reservation: (state) => state.reservation.currentReservation,
      transports: (state) => state.transport.transports
    })
  },
  methods: {}
};
</script>

<style scoped>
.main {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  margin-top: 20px;
  width: 100%;
}
.title > h1 {
  color: white;
  background-color: #1b4397;
  width: 45%;
  padding: 10px;
}

.title {
  width: 100%;
  display: flex;
  flex-direction: column;
  margin-bottom: 20px;
}

.border {
  margin: 15px 0 15px 0;
  border: 3px solid;
  border-radius: 10px;
}

select {
  border: 1px solid;
  padding: 10px;
}
</style>
