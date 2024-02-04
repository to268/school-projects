<template>
  <div class="details">
    <div v-if="resa" class="case infos">
      <div v-if="transports" class="info">
        <p>Transport</p>
        <input
          v-if="getTransport(resa.transportId)"
          type="text"
          readonly
          :value="getTransport(resa.transportId).libelle"
        />
      </div>
      <div class="info">
        <p>Prix</p>
        <input type="text" readonly :value="price" />
      </div>
    </div>
    <div class="case">
      <h2>Les autres participants</h2>
      <div v-if="resa.lesAutreParticipants.length !== 0" class="list participants">
        <p v-for="i in resa.lesAutreParticipants" :key="i.autreParticipantId" class="tile">
          {{ i.nom }} {{ i.prenom }}
        </p>
      </div>
      <div
        style="
          display: flex;
          justify-content: center;
          align-items: center;
          color: red;
          font-size: 25px;
        "
        v-else
        class="list participants"
      >
        <p>Pas de participant</p>
      </div>
    </div>
    <div class="case">
      <h2>Les activités prises:</h2>
      <div class="list activite">
        <p v-for="i in resa.lesActiviteEnfantCartes" :key="i" class="tile">{{ i.titre }}</p>
        <p v-for="i in resa.lesActiviteCartes" :key="i" class="tile">{{ i.titre }}</p>
      </div>
    </div>
    <div class="case">
      <div class="validation">
        <FaCheck :style="{ color: validColor }" class="icon" />
        <p :style="{ color: validColor }">
          {{ resa.validation ? 'Validé' : 'En cours de validation' }}
        </p>
      </div>
      <div class="confirmation">
        <FaCalendarCheck :style="{ color: confirmColor }" class="icon" />
        <p :style="{ color: confirmColor }">
          {{ resa.confirmation ? 'Confirmé' : 'En cours de confirmation' }}
        </p>
      </div>
    </div>
  </div>
</template>

<script>
import { FaCalendarCheck } from '@kalimahapps/vue-icons/fa';
import { FaCheck } from '@kalimahapps/vue-icons/fa';

import { mapState, mapGetters } from 'vuex';

export default {
  name: 'DetailsSejour',
  props: ['resa'],
  components: {
    FaCalendarCheck,
    FaCheck
  },
  methods: {
    getTransport(id) {
      return this.getTransportById(id);
    }
  },
  created() {
    if (this.transports.length === 0) {
      this.$store.dispatch('transport/fetchTransports');
    }
  },
  computed: {
    price() {
      return this.resa.prix + ' €';
    },
    confirmColor() {
      return this.resa.confirmation ? 'green' : 'red';
    },
    validColor() {
      return this.resa.validation ? 'green' : 'red';
    },
    ...mapGetters({
      getTransportById: 'transport/getById'
    }),
    ...mapState({
      transports: (state) => state.transport.transports
    })
  }
};
</script>

<style scoped>
@media screen and (max-width: 1350px) {
  .details {
    flex-direction: column;
  }

  .confirme {
    flex-direction: row;
  }
}

.details {
  width: 100%;
  padding: 30px;
  margin-bottom: 40px;
  display: flex;
  border: 1px solid;
  justify-content: space-around;
  align-items: center;
  animation-name: slideActivites;
  animation-iteration-count: 1;
  animation-duration: 1s;
  animation-fill-mode: forwards;
}

@keyframes slideActivites {
  from {
    opacity: 0;
  }
  to {
    opacity: 1;
  }
}

.case {
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: space-around;
  align-items: center;
}

.case {
  height: 400px;
}

.info > input {
  padding: 10px;
  font-size: 1.4em;
  width: 80%;
}

.list {
  overflow-y: scroll;
  overflow-x: hidden;
  height: 60%;
  width: 250px;
  display: flex;
  flex-direction: column;
  align-items: center;
  background: #d0d0d0;
}

.list > .tile {
  margin: 20px 0 20px 0;
  word-wrap: unset;
}

.icon {
  width: 50%;
  height: 50%;
}

.confirmation,
.validation {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
}
</style>
