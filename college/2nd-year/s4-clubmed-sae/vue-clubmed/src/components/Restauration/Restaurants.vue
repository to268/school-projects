<template>
  <div class="main">
    <div class="title">
      <h1>Restaurants</h1>
    </div>
    <div class="restaurants">
      <Restaurant
        :restau="restau"
        v-for="restau in getRestaurants(resortId)"
        :key="restau.restaurantId"
      />
    </div>
  </div>
</template>

<script>
import Restaurant from '@/components/Restauration/Restaurant.vue';

import { mapGetters } from 'vuex';

export default {
  name: 'Restaurants',
  props: ['resortId'],
  components: {
    Restaurant
  },
  computed: {
    ...mapGetters({
      getRestaurants: 'restauration/getRestaurantsByResortId'
    })
  }
};
</script>

<style scoped>
.main {
  display: flex;
  flex-direction: column;
  align-items: start;
  justify-content: start;
  width: 100%;
}

.title {
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: start;
  margin: 20px 0 20px 0;
  font-size: 1.5em;
}

.title > h1 {
  width: 60%;
  display: flex;
  justify-content: center;
}

.restaurants {
  width: 100%;
}
</style>
