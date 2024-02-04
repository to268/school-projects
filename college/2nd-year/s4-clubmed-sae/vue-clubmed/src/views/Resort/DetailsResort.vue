<template>
  <div v-if="resort && !isLocalisationLoading" class="main">
    <div class="titleImage">
      <img
        v-if="resort.lesPhotos.length !== 0"
        class="img"
        :src="resort.lesPhotos[0].lien"
        alt=""
      />
      <ProgressIndicator v-else class="progress" />
      <div class="infos">
        <div class="title">
          <h1 class="name">{{ resort.nom }}</h1>
          <h3>{{ getLocalisation(resort.localisationId).nom }}</h3>
          <div class="avislien">
            <div class="avis">{{ resort.moyenneAvis }}/5</div>
          </div>
        </div>
        <div class="bookdiv">
          <div class="price">A partir de {{ resort.prixDepart }} €</div>
          <router-link
            class="book"
            :to="{ name: 'Reservation', params: { resortId: resort.resortId } }"
          >
            Reserver
          </router-link>
        </div>
      </div>
    </div>
    <div class="link">
      <a href="."> PDF de la Documentation du resort </a>
      <AnOutlinedArrowRight />
    </div>
    <div class="descriptiondiv">
      <div class="description">{{ resort.descriptionGen }}</div>
    </div>
  </div>
  <ProgressIndicator v-else class="mainprogress" />
</template>

<script>
import { AnOutlinedArrowRight } from '@kalimahapps/vue-icons/an';

import { mapGetters, mapState } from 'vuex';
import ProgressIndicator from '@/components/ProgressIndicator.vue';

export default {
  name: 'DetailsResort',
  props: ['resort'],
  components: {
    ProgressIndicator,
    AnOutlinedArrowRight
  },
  computed: {
    ...mapGetters({
      getResort: 'resort/getById',
      getLocalisation: 'localisation/getById'
    }),
    ...mapState({
      isLocalisationLoading: (state) => state.localisation.isLoading,
      resorts: (state) => state.resort.resorts,
      localisations: (state) => state.localisation.localisations
    })
  }
};
</script>

<style scoped>
@media screen and (max-width: 1000px) {
  .bookdiv {
    flex-direction: column;
  }
}

.main {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: start;
}

.titleImage {
  display: flex;
  justify-content: space-evenly;
  align-items: stretch;
  margin-top: 20px;
}

.infos {
  display: flex;
  width: 40%;
  flex-direction: column;
  justify-content: space-evenly;
  align-items: start;
}

.avis {
  background-color: goldenrod;
  padding: 20px;
  font-size: 1.5em;
  color: white;
  font-weight: bold;
  border-radius: 8px;
}

.name {
  font-size: 3em;
  color: goldenrod;
}

.avislien {
  margin-top: 10px;
  display: flex;
  justify-content: space-evenly;
}

.titleImage > .img {
  width: 50%;
  height: 50%;
  border-radius: 10px;
}

.description {
  margin: 30px 50px 30px 50px;
  background-color: whitesmoke;
  padding: 30px;
  width: 80%;
  text-align: center;
  word-wrap: break-word;
  box-shadow: 5px 5px 7px black;
}

.title {
  width: 100%;
  display: flex;
  flex-direction: column;
  align-items: start;
}

.link {
  margin-top: 20px;
  display: flex;
  align-items: center;
  justify-content: center;
  width: 50%;
}

.link > a {
  text-decoration: none;
  color: black;
}

.bookdiv {
  display: flex;
  justify-content: space-evenly;
  align-items: center;
  width: 100%;
}

.price {
  border: 1px solid;
  padding: 10px;
  font-size: 1.3em;
}

.book {
  text-decoration: none;
  color: white;
  background-color: #1b4397;
  padding: 20px;
  border-radius: 5px;
  font-size: 2em;
}

.descriptiondiv {
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
}

.mainprogress {
  height: 80vh;
}
</style>
