<template>
  <div class="resort">
    <div class="links">
      <div class="left">
        <router-link :to="{ name: 'DetailsResort' }">Details</router-link>
        <router-link :to="{ name: 'ActiviteResort' }">Activités</router-link>
        <router-link :to="{ name: 'TypeChambreResort' }">Chambres</router-link>
      </div>
      <div class="right">
        <router-link :to="{ name: 'RestaurationResort' }">Restauration</router-link>
        <router-link :to="{ name: 'AvisResort' }">Avis</router-link>
        <router-link v-if="resort && resort.domaineId" :to="{ name: 'DomaineSkiableResort' }"
          >Domaine Skiable</router-link
        >
      </div>
    </div>
    <div class="border" />
    <router-view :resort="resort" />
    <div class="linkedResorts"></div>
  </div>
</template>

<script>
import { mapGetters, mapState } from 'vuex';

import ProgressIndicator from '@/components/ProgressIndicator.vue';

export default {
  name: 'ResortLayout',
  components: { ProgressIndicator },
  props: ['resortId'],
  computed: {
    resort() {
      let resort = this.getResort(parseInt(this.resortId));
      return resort;
    },
    ...mapGetters({
      getResort: 'resort/getById'
    }),
    ...mapState({
      isResortsLoading: (state) => state.resort.isLoading,
      resorts: (state) => state.resort.resorts,
      photos: (state) => state.photo.photos
    })
  }
};
</script>

<style scoped>
@media screen and (max-width: 1000px) {
  .links {
    flex-direction: column;
    justify-content: space-evenly;
    align-items: stretch;
  }

  .right,
  .left {
    margin-top: 10px;
  }
}

@media screen and (max-width: 530px) {
  .left,
  .right {
    flex-direction: column;
  }
}

.links {
  margin-top: 20px;
  margin-bottom: 20px;
  display: flex;
  justify-content: space-around;
}

.left,
.right {
  flex-grow: 1;
  display: flex;
  justify-content: space-around;
}

.links a {
  text-decoration: none;
  color: black;
  font-weight: bold;
  font-size: 2em;
}

.links a.router-link-exact-active {
  border-bottom: 3px solid;
  color: #1b4397;
  border-block-end-color: #1b4397;
  border-block-start-color: #1b4397;
  animation-name: borderAnim;
  animation-duration: 0.5s;
  animation-iteration-count: 1;
  animation-delay: 0s;
  animation-fill-mode: forwards;
}

@keyframes borderAnim {
  0% {
    border-block-start-color: transparent;
    border-block-end-color: transparent;
  }
  50% {
    border-block-start-color: #1b4397;
    border-block-end-color: transparent;
  }
  100% {
    border-block-start-color: #1b4397;
    border-block-end-color: #1b4397;
  }
}

.border {
  border: 1px solid;
  margin: 30px 0 20px 0;
}
.progress {
  height: 80vh;
}
</style>
