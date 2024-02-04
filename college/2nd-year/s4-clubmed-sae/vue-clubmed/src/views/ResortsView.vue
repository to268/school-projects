<template>
  <SearchBar />
  <Resorts />
</template>

<script>
import SearchBar from '@/components/SearchBar.vue';
import Resorts from '@/components/Resorts/Resorts.vue';

export default {
  name: 'ResortsView',
  components: {
    Resorts,
    SearchBar
  }
};
</script>

<style scoped></style>
