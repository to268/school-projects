<script>
import Banner from '@/components/Banner.vue';
import Credits from '@/components/Credits.vue';

import { mapState } from 'vuex';

export default {
  name: 'App',
  components: {
    Banner,
    Credits
  },
  created() {
    this.$store.dispatch('fetchToken');
    if (this.token) {
      this.$store.dispatch('resort/fetchResorts');
    }
  },
  watch: {
    client: function (val) {
      // arcu@outlook.ca
      // 2@eC81@9p873{
      console.log(val);
    },
    token: function () {
      //this.$store.dispatch('fetchClient', 11);
      this.$store.dispatch('resort/fetchResorts', { min: 1, max: 10 });
      this.$store.dispatch('resort/fetchResorts', { min: 11, max: 20 });
      this.$store.dispatch('resort/fetchResorts', { min: 21, max: 30 });
      this.$store.dispatch('resort/fetchResorts', { min: 31, max: 40 });
      this.$store.dispatch('resort/fetchResorts', { min: 41, max: 50 });
      this.$store.dispatch('localisation/fetchLocalisations');
      this.$store.dispatch('photo/fetchPhotos');
      this.$store.dispatch('typeclub/fetchTypeclubs');
      this.$store.dispatch('pays/fetchPays');
      this.$store.dispatch('civilite/fetchCivilites');
      this.$store.dispatch('restauration/fetchRestauration');
      this.$store.dispatch('domaineSkiable/fetchDomaines');
      this.$store.dispatch('typeactivite/fetchTypeActivites');
      this.$store.dispatch('transport/fetchTransports');
    }
  },
  computed: {
    ...mapState({
      client: (state) => state.currentClient,
      token: (state) => state.token
    })
  }
};
</script>

<template>
  <Banner />
  <RouterView />
  <Credits />
</template>

<style>
* {
  padding: 0;
  margin: 0;
  font-family: Ubuntu;
}

.progress {
  height: 80vh;
}
</style>
