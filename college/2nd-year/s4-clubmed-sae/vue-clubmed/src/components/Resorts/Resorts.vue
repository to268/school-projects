<template>
  <div v-if="resorts.length !== 0 && !isResortsLoading" class="resorts">
    <div v-for="resort in getResort" :key="resort.resortId" class="resort">
      <img v-if="getPhoto(resort)" :src="getPhoto(resort)" :alt="resort.nom" />
      <h2>{{ resort.nom }}</h2>
      <p class="description">{{ resort.descriptionGen }}</p>
      <div class="form">
        <router-link
          :to="{ name: 'Reservation', params: { resortId: resort.resortId } }"
          class="link book"
        >
          Réserver
        </router-link>
        <router-link
          :to="{ name: 'DetailsResort', params: { resortId: resort.resortId } }"
          class="link details"
        >
          Détails
        </router-link>
      </div>
    </div>
  </div>
  <ProgressIndicator v-else class="progress" />
</template>

<script>
import { mapState, mapGetters } from 'vuex';

import ProgressIndicator from '@/components/ProgressIndicator.vue';

export default {
  name: 'Resorts',
  components: {
    ProgressIndicator
  },
  data() {
    return {
      currentResort: undefined
    };
  },
  mounted() {
    scrollTo({
      top: 10,
      left: 10,
      behavior: 'smooth'
    });
  },
  unmounted() {
    scrollTo({
      top: 10,
      left: 10,
      behavior: 'smooth'
    });
  },
  methods: {
    getPhoto(resort) {
      if (resort) {
        if (resort.lesPhotos) {
          if (resort.lesPhotos.length !== 0) {
            return resort.lesPhotos[0].lien;
          }
        }
      }
      return undefined;
    }
  },
  computed: {
    ...mapGetters({
      getResort: 'resort/getFiltered'
    }),
    ...mapState({
      resorts: (state) => state.resort.resorts,
      isResortsLoading: 'resort/isLoading'
    })
  }
};
</script>

<style scoped>
.resorts {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  align-items: center;
}

.resort {
  height: 600px;
  margin: 30px;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: space-evenly;
  background-color: whitesmoke;
  animation-name: resort;
  animation-duration: 1s;
  animation-fill-mode: forwards;
  animation-iteration-count: 1;
}

@keyframes resort {
  from {
    width: 0;
  }
  to {
    width: 500px;
  }
}

img {
  width: 100%;
}

.form {
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: space-evenly;
}

.link {
  background-color: transparent;
  border: 1px solid;
  border-radius: 8px;
  padding: 10px;
  font-size: 1.9em;
}

.link.book {
  background-color: #1b4397;
  color: whitesmoke;
  border-color: transparent;
}

a {
  text-decoration: none;
  color: black;
}

.link:hover {
  cursor: pointer;
}

.description {
  height: 50px;
  font-size: 13px;
  overflow: hidden;
}

.progress {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 70vh;
}
</style>
