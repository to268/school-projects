<template>
  <div v-if="client" class="main">
    <div class="profile">
      <h1 class="title">Mon Profil Personnel</h1>
      <Infos />
      <h1 class="title">Mes Séjours en cours</h1>
      <Sejours />
    </div>
  </div>
  <ProgressIndicator v-else class="progress" />
</template>

<script>
import Infos from '@/components/Profile/Infos.vue';
import Sejours from '@/components/Profile/Sejours.vue';
import ProgressIndicator from '@/components/ProgressIndicator.vue';

import { mapState } from 'vuex';

export default {
  name: 'Profile',
  components: { Sejours, Infos, ProgressIndicator },
  mounted() {
    if (!this.client) {
      this.$router.push({ path: '/' });
    }
  },
  computed: {
    ...mapState({
      client: (state) => {
        return state.currentClient;
      }
    })
  }
};
</script>

<style scoped>
.main {
  display: flex;
  align-items: center;
  justify-content: center;
  margin: 50px 0 50px 0;
}

.profile {
  width: 80%;
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
}

.title {
  font-size: 3em;
  width: 100%;
  border-bottom: 10px solid #1b4397;
  color: #1b4397;
}

.progresslil {
  height: 30vh;
}
</style>
