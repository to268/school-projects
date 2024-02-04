<template>
  <div class="main">
    <div class="title">
      <h1>Bars</h1>
    </div>
    <Bar :bar="bar" v-for="bar in getBars(resortId)" :key="bar" class="bars" />
  </div>
</template>

<script>
import Bar from '@/components/Restauration/Bar.vue';

import { mapGetters } from 'vuex';

export default {
  name: 'Bars',
  props: ['resortId'],
  components: { Bar },
  computed: {
    ...mapGetters({
      getBars: 'restauration/getBarsByResortId'
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

.bars {
  width: 100%;
}
</style>
