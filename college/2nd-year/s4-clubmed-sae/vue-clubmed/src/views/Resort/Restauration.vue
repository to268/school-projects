<template>
  <div v-if="resort && restaurants" class="main">
    <div class="maindiv">
      <Restaurants :resortId="resort.resortId" />
      <Bars :resortId="resort.resortId" />
    </div>
  </div>
  <ProgressIndicator v-else class="progress" />
</template>

<script>
import Restaurants from '@/components/Restauration/Restaurants.vue';
import Bars from '@/components/Restauration/Bars.vue';
import ProgressIndicator from '@/components/ProgressIndicator.vue';

import { mapState } from 'vuex';

export default {
  name: 'Restauration',
  props: ['resort'],
  components: {
    Bars,
    Restaurants,
    ProgressIndicator
  },
  watch: {
    restaurants: function (val) {
      console.log(val);
    }
  },
  computed: {
    ...mapState({
      restaurants: (state) => state.restauration.restaurants,
      bars: (state) => state.restauration.bars,
      token: (state) => state.token
    })
  }
};
</script>

<style scoped>
.main {
  display: flex;
  align-items: center;
  justify-content: center;
}

.maindiv {
  width: 90%;
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: space-around;
  align-items: start;
}
</style>
