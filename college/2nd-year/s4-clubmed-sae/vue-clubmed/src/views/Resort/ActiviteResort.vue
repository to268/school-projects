<template>
  <div class="main">
    <div v-if="resort && !isTypeActiviteLoading" class="maindiv">
      <TypeActivite
        tag="adulte"
        :activites="resort.lesActiviteCartes"
        title="Activités adulte à la carte"
      />
      <TypeActivite
        tag="enfant"
        :activites="resort.lesActiviteEnfantCartes"
        title="Activité enfant à la carte"
      />
      <TypeActivite
        tag="adulte"
        :activites="resort.lesActiviteIncluses"
        title="Activité adulte incluse"
      />
      <TypeActivite
        tag="enfant"
        :activites="resort.lesActiviteEnfantIncluses"
        title="Activité enfant incluse"
      />
    </div>
    <ProgressIndicator v-else class="progress" />
  </div>
</template>

<script>
import TypeActivite from '@/components/Resort/TypeActivite.vue';
import ProgressIndicator from '@/components/ProgressIndicator.vue';

import { mapState } from 'vuex';

export default {
  name: 'ActiviteResort',
  props: ['resort'],
  components: {
    TypeActivite,
    ProgressIndicator
  },
  computed: {
    ...mapState({
      isTypeActiviteLoading: (state) => state.typeactivite.isTypeActiviteLoading,
      typeActivites: (state) => state.typeactivite.typeActivites,
      token: (state) => state.token
    })
  }
};
</script>

<style scoped>
.main {
  display: flex;
  justify-content: center;
  align-items: center;
}

.maindiv {
  margin-bottom: 30px;
  width: 80%;
  height: 100%;
  display: flex;
  flex-direction: column;
  align-items: start;
  justify-content: space-evenly;
}

.progress {
  height: 80vh;
}
</style>
