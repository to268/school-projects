<template>
  <div class="main">
    <div v-if="resort" class="typechambre">
      <Chambre
        v-for="chambre in resort.lesTypeChambres"
        :key="chambre.typeChambreId"
        :chambre="chambre"
      />
    </div>
    <ProgressIndicator v-else class="progress" />
  </div>
</template>

<script>
import Chambre from '@/components/Resort/Chambre.vue';
import ProgressIndicator from '@/components/ProgressIndicator.vue';

// import { mapGetters, mapState } from 'vuex';

export default {
  name: 'TypeChambre',
  props: ['resort'],
  components: { Chambre, ProgressIndicator }
};
</script>

<style scoped>
.main {
  display: flex;
  align-items: center;
  justify-content: center;
}

.typechambre {
  width: 80%;
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: space-around;
  flex-wrap: wrap;
}
</style>
