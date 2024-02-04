<template>
  <div class="main">
    <div class="title">
      <h1>(Optionnel) Choisissez vos activitées à la cartes</h1>
      <div class="border" />
    </div>
    <div v-if="resort" class="activities">
      <div v-for="act in resort.lesActiviteEnfantCartes" :key="act" class="activity">
        <img src="/img/typeactivite/Activités-en-famille/Activités-en-famille.jpg" alt="" />
        <div class="infos">
          <div>
            <p>{{ act.titre }}</p>
            <p>{{ act.duree }} min</p>
            <p>{{ act.prix }} €</p>
          </div>
          <div class="take">
            <label for="taken">take</label>
            <input name="taken" type="checkbox" @click="toggleActiviteEnfantCarte(act)" />
          </div>
        </div>
      </div>
      <div v-for="act in resort.lesActiviteCartes" :key="act" class="activity">
        <img
          v-if="getTypeActivite(act.typeActiviteId)"
          :src="getPhoto(getTypeActivite(act.typeActiviteId).photoId).lien"
        />
        <div class="infos">
          <div>
            <p>{{ act.titre }}</p>
            <p>{{ act.duree }} min</p>
            <p>{{ act.prix }} €</p>
          </div>
          <div class="take">
            <label for="taken">take</label>
            <input name="taken" type="checkbox" @click="toggleActiviteCarte(act)" />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from 'vuex';

export default {
  name: 'Activities',
  props: ['resort'],
  computed: {
    ...mapState({
      reservation: (state) => state.reservation.currentReservation,
      lesActiviteCartes: (state) => state.reservation.currentReservation.lesActiviteCartes,
      lesActiviteEnfantCartes: (state) =>
        state.reservation.currentReservation.lesActiviteEnfantCartes,
      typeActivites: (state) => state.typeactivite.typeActivites,
      photos: (state) => state.photo.photos
    }),
    ...mapGetters({
      getTypeActivite: 'typeactivite/getById',
      getPhoto: 'photo/getById'
    })
  },
  methods: {
    toggleActiviteEnfantCarte(act) {
      if (!this.lesActiviteEnfantCartes.includes(act)) {
        this.lesActiviteEnfantCartes.push(act);
      } else {
        let index = this.lesActiviteEnfantCartes.indexOf(act);
        this.lesActiviteEnfantCartes.splice(index, 1);
      }
    },
    toggleActiviteCarte(act) {
      if (!this.lesActiviteCartes.includes(act)) {
        this.lesActiviteCartes.push(act);
      } else {
        let index = this.lesActiviteCartes.indexOf(act);
        this.lesActiviteCartes.splice(index, 1);
      }
    }
  }
};
</script>

<style scoped>
.main {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  margin-top: 20px;
}

.activities {
  width: 80%;
  display: flex;
  justify-content: center;
  align-items: center;
  flex-wrap: wrap;
}

.activity {
  display: flex;
  flex-direction: column;
  justify-content: space-evenly;
  padding: 0 20px 0 20px;
  margin-bottom: 30px;
}

.activity > img {
  width: 300px;
  margin-bottom: 10px;
}

.infos {
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.take {
  width: 40%;
  display: flex;
  justify-content: space-evenly;
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
</style>
