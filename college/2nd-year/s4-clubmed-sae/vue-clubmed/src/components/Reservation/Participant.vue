<template>
  <div class="participant">
    <h2>Participant {{ number }}</h2>
    <input v-model="nom" type="text" placeholder="nom" />
    <input v-model="prenom" type="text" placeholder="prenom" />
    <input v-model="getDate" type="date" />
    <div class="action">
      <label for="add">Ajouter le participant</label>
      <input type="checkbox" name="add" class="add" @click="add" />
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex';

export default {
  name: 'Participants',
  props: ['number'],
  data() {
    return {
      nom: '',
      prenom: '',
      date: undefined
    };
  },
  methods: {
    nowStr() {
      let now = new Date();
      let nowStr =
        now.getFullYear() +
        '-' +
        (now.getMonth().toString().length === 1 ? '0' + now.getMonth() : now.getMonth()) +
        '-' +
        (now.getDay().toString().length === 1 ? '0' + now.getDay() : now.getDay());
      return nowStr;
    },
    add() {
      if (this.nom.length !== 0 && this.prenom.length !== 0) {
        let participant = {
          civiliteId: 3,
          prenom: this.prenom,
          nom: this.nom,
          dateNaissance: this.date
        };
        if (
          this.lesParticipants.find((item) => {
            return (
              item.nom == participant.nom &&
              item.prenom == participant.prenom &&
              item.dateNaissance == participant.dateNaissance &&
              item.civiliteId == participant.civiliteId
            );
          })
        ) {
          let index = this.lesParticipants.indexOf(participant);
          this.lesParticipants.splice(index, 1);
        } else {
          this.lesParticipants.push(participant);
        }
      }
    }
  },
  computed: {
    ...mapState({
      reservation: (state) => state.reservation.currentReservation,
      lesParticipants: (state) => state.reservation.currentReservation.lesAutreParticipants
    }),
    getDate: {
      set(val) {
        this.date = val + 'T00:00:00';
      },
      get() {
        if (this.date) {
          let date = this.date;
          return date.substring(0, 4) + '-' + date.substring(5, 7) + '-' + date.substring(8, 10);
        } else {
          return this.nowStr();
        }
      }
    }
  }
};
</script>

<style scoped>
.participant {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: space-evenly;
  margin-top: 20px;
  padding: 10px;
  border: 1px solid;
}

#title > h1 {
  color: white;
  background-color: #1b4397;
  width: 80%;
  padding: 10px;
}

#title {
  width: 100%;
  display: flex;
  flex-direction: column;
  margin-bottom: 20px;
}

#border {
  margin: 15px 0 15px 0;
  border: 3px solid;
  border-radius: 10px;
}

button {
  text-align: center;
  color: white;
  margin-top: 20px;
  margin-bottom: 20px;
  background-color: #1b4397;
  padding: 10px;
}

input {
  border: 1px solid;
  padding: 10px;
  margin: 10px;
}

.action {
  display: flex;
  justify-content: center;
  align-items: center;
}

.add {
  height: 30px;
  width: 30px;
}
</style>
