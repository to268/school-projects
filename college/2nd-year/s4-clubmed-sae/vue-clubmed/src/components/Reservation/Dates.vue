<template>
  <div id="title">
    <h1>Veuillez choisir vos dates de séjour</h1>
    <div id="border"></div>
  </div>
  <div class="dates">
    <div class="date">
      <p>Date de début</p>
      <input v-model="dateDebut" type="date" name="begindate" />
    </div>
    <div class="date">
      <p>Date de fin</p>
      <input v-model="dateFin" type="date" name="enddate" />
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex';

export default {
  name: 'Dates',
  computed: {
    ...mapState({
      reservation: (state) => state.reservation.currentReservation
    }),
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
    dateDebut: {
      get() {
        let date = this.reservation.dateDebut ? this.reservation.dateDebut : this.nowStr;
        return date.substring(0, 4) + '-' + date.substring(5, 7) + '-' + date.substring(8, 10);
      },
      set(value) {
        this.reservation.dateDebut = value + 'T00:00:00';
      }
    },
    dateFin: {
      get() {
        let date = this.reservation.dateFin ? this.reservation.dateFin : this.nowStr;
        let dateStr =
          date.substring(0, 4) + '-' + date.substring(5, 7) + '-' + date.substring(8, 10);
        return dateStr;
      },
      set(value) {
        this.reservation.dateFin = value + 'T00:00:00';
      }
    }
  }
};
</script>

<style scoped>
.dates {
  width: 70%;
  display: flex;
  justify-content: space-around;
  align-items: center;
  margin-bottom: 50px;
}

.date {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: center;
  border: 1px solid;
  padding: 20px;
}

#title > h1 {
  color: white;
  background-color: #1b4397;
  width: 35%;
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
</style>
