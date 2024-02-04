<template>
  <div class="restaurant">
    <img :src="getPhoto(restau.photoId).lien" alt="restau" />
    <div class="border" />
    <div class="infos">
      <h2>{{ restau.nom }}</h2>
      <p>
        {{ restau.description }}
      </p>
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from 'vuex';

export default {
  name: 'Restaurant',
  props: ['restau'],
  computed: {
    ...mapGetters({
      getPhoto: 'photo/getById'
    }),
    ...mapState({
      photos: (state) => state.photo.photos
    })
  }
};
</script>

<style scoped>
@media screen and (max-width: 1230px) {
  .restaurant {
    flex-direction: column;
    justify-items: center;
  }
  .border {
    visibility: hidden;
  }
  .infos {
    font-size: 70%;
    width: 200px;
  }
  .infos > p {
    width: 100%;
  }
}

.restaurant {
  display: flex;
  align-items: center;
  justify-content: space-around;
  height: 500px;
  margin: 40px 0 40px 0;
  border: 1px solid;
}

.restaurant > img {
  width: 40%;
}

.border {
  height: 40%;
  border: 1px solid darkgrey;
}

.infos {
  display: flex;
  flex-direction: column;
  justify-content: space-evenly;
  align-items: center;
  width: 40%;
  height: 300px;
  padding: 10px;
}

.infos > h2 {
  color: white;
  background: #1b4397;
  padding: 10px;
  font-weight: bold;
  font-size: 2em;
  width: 100%;
  display: flex;
  justify-content: center;
}

.infos > p {
  word-wrap: break-word;
  width: 100%;
}
</style>
