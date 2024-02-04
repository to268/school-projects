<template>
  <DroitLayout title="Politique de protection des données">
    <Politique_1_2 />
    <Politique_4_5 />
    <Politique_6_7 />
    <Politique_11_12_13 />
    <Politique_8_9_10 />
  </DroitLayout>
</template>

<script>
import DroitLayout from '@/views/Droit/DroitLayout.vue';
import Politique_1_2 from '@/components/Politique/1_2_3.vue';
import Politique_4_5 from '@/components/Politique/4_5.vue';
import Politique_6_7 from '@/components/Politique/6_7.vue';
import Politique_8_9_10 from '@/components/Politique/8_9_10.vue';
import Politique_11_12_13 from '@/components/Politique/11_12_13.vue';

export default {
  name: 'Politique',
  components: {
    DroitLayout,
    Politique_1_2,
    Politique_4_5,
    Politique_6_7,
    Politique_8_9_10,
    Politique_11_12_13
  }
};
</script>

<style scoped>
p {
  margin-left: 20px;
}

ul {
  margin-left: 50px;
}
</style>
