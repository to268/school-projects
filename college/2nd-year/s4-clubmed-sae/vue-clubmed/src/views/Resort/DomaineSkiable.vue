<template>
  <div v-if="resort && domaine && photo" class="main">
    <div class="title">
      <h1>{{ domaine.titre }}</h1>
      <h3>{{ domaine.nom }}</h3>
    </div>
    <img :src="photo.lien" :alt="domaine.nom" />
    <div class="domainSkiable">
      <div class="infos">
        <div class="info">
          <BsGeoAltFill class="icon" />
          Altitude club : {{ domaine.altitudeClub }}
        </div>
        <div class="info">
          <MdDomain class="icon" />
          Altitude domaine : {{ domaine.altitudeStation }}
        </div>
        <div class="info">
          <CaListNumbered class="icon" />
          nombre de pistes : {{ domaine.nbPiste }}
        </div>
        <div class="info">
          <LaSkiingNordicSolid class="icon" />
          Ski au pied : {{ domaine.infoSkiAuPied ? 'Oui' : 'Non' }}
        </div>
        <div class="info">
          <FaPersonSkiing class="icon" />
          longueur des pistes : {{ domaine.longueurDesPistes }}
        </div>
      </div>
      <div class="description">
        {{ domaine.description }}
      </div>
    </div>
  </div>
  <ProgressIndicator v-else class="progress" />
</template>

<script>
import { BsGeoAltFill } from '@kalimahapps/vue-icons/bs';
import { FaPersonSkiing } from '@kalimahapps/vue-icons/fa';
import { LaSkiingNordicSolid } from '@kalimahapps/vue-icons/la';
import { CaListNumbered } from '@kalimahapps/vue-icons/ca';
import { MdDomain } from '@kalimahapps/vue-icons/md';

import { mapGetters, mapState } from 'vuex';

import ProgressIndicator from '@/components/ProgressIndicator.vue';

export default {
  name: 'DomaineSkiable',
  props: ['resort'],
  components: {
    FaPersonSkiing,
    LaSkiingNordicSolid,
    CaListNumbered,
    MdDomain,
    BsGeoAltFill,
    ProgressIndicator
  },
  created() {
    if (this.token && this.domaines.length === 0) {
    }
  },
  computed: {
    domaine() {
      let domaine = this.getDomaineById(this.resort.domaineId);
      return domaine;
    },
    photo() {
      let photo = this.getPhotoById(this.domaine.photoId);
      return photo;
    },
    ...mapGetters({
      getDomaineById: 'domaineSkiable/getById',
      getPhotoById: 'photo/getById'
    }),
    ...mapState({
      domaines: (state) => state.domaineSkiable.domaineSkiables
    })
  }
};
</script>

<style scoped>
@media screen and (max-width: 890px) {
  .infos {
    flex-direction: column;
  }
}

.main {
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
}

.main > img {
  width: 60%;
}

.title {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  margin: 10px 0 10px 0;
}

.title > h1 {
  color: goldenrod;
  font-size: 4em;
}

.domainSkiable {
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
  margin: 30px;
}

.infos {
  margin: 50px 0 50px 0;
  width: 100%;
  display: flex;
  justify-content: space-evenly;
  align-items: center;
}

.info {
  border: 1px solid grey;
  padding: 3%;
  border-radius: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
}

.icon {
  width: 2em;
  height: 2em;
}

.description {
  background: whitesmoke;
  padding: 20px;
  width: 70%;
  box-shadow: 5px 5px 10px grey;
}
</style>
