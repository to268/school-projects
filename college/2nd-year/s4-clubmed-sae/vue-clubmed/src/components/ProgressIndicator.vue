<template>
  <div class="progress">
    <v-progress-circular color="blue" indeterminate class="progressicon" />
  </div>
</template>

<script>
export default {
  name: 'ProgressIndicator'
};
</script>

<style scoped>
.progress {
  display: flex;
  align-items: center;
  justify-content: center;
}

.progressicon {
  height: 50px;
  width: 50px;
}
</style>
