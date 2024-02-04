<template>
  <div class="avis">
    <div v-if="client && resort && avis" class="avisdiv">
      <div class="client">
        <BsPersonFill />
        <h3>De {{ client.prenom }} {{ client.nom }} dans {{ resort.nom }}</h3>
      </div>
      <div class="note">
        <div
          :class="[avis.note == 0.5 ? 'demi' : avis.note >= 1 ? 'plein' : 'vide', 'circle']"
        ></div>
        <div
          :class="[avis.note == 1.5 ? 'demi' : avis.note >= 2 ? 'plein' : 'vide', 'circle']"
        ></div>
        <div
          :class="[avis.note == 2.5 ? 'demi' : avis.note >= 3 ? 'plein' : 'vide', 'circle']"
        ></div>
        <div
          :class="[avis.note == 3.5 ? 'demi' : avis.note >= 4 ? 'plein' : 'vide', 'circle']"
        ></div>
        <div
          :class="[avis.note == 4.5 ? 'demi' : avis.note >= 5 ? 'plein' : 'vide', 'circle']"
        ></div>
      </div>
      <p class="commentaire">{{ avis.commentaire }}</p>
      <p>{{ date.getFullYear() }}-{{ date.getMonth() }}-{{ date.getDay() }}</p>
    </div>
    <ProgressIndicator v-else class="lilprogress" />
  </div>
</template>

<script>
import { BsPersonFill } from '@kalimahapps/vue-icons/bs';
import ProgressIndicator from '@/components/ProgressIndicator.vue';

import { mapGetters } from 'vuex';

export default {
  name: 'Avis',
  props: ['avis', 'resort'],
  components: {
    BsPersonFill,
    ProgressIndicator
  },
  mounted() {
    if (!this.getClientById(this.avis.clientId)) {
      this.$store.dispatch('client/fetchClientById', this.avis.clientId);
    }
  },
  computed: {
    client() {
      return this.getClientById(this.avis.clientId);
    },
    date() {
      return new Date(this.avis.date);
    },
    ...mapGetters({
      getClientById: 'client/getById'
    })
  }
};
</script>

<style scoped>
.avis {
  background: whitesmoke;
  box-shadow: 5px 5px 10px;
  width: 300px;
  display: flex;
  flex-direction: column;
  justify-content: space-evenly;
  padding: 20px;
  margin: 20px;
}

.client {
  display: flex;
  align-items: center;
  justify-content: space-evenly;
}

.note {
  display: flex;
}

.circle {
  width: 40px;
  height: 40px;
  margin: 0 5px 0 5px;
  border-radius: 100%;
  border: 1px solid black;
}

.circle.plein {
  background: #00aa6c;
}

.circle.vide {
  background: #fff;
}

.circle.demi {
  background: linear-gradient(to left, #fff 50%, #fff 50%, #00aa6c 50%, #00aa6c 50%);
}

.commentaire {
  word-wrap: break-word;
  overflow: scroll;
  height: 30%;
}

.lilprogress {
  height: 250px;
}
</style>
