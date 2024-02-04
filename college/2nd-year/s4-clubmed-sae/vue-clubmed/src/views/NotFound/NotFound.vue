<template>
  <div class="NotFound">
    <div class="oops">
      <h1>Oops !</h1>
      <p>Cette page n'a pas été trouvé !</p>
    </div>
    <div class="linkdiv">
      <p>Page d'accueil :</p>
      <router-link class="link" :to="{ path: '/' }"> ici </router-link>
    </div>
  </div>
</template>

<script>
export default {
  name: 'NotFound'
};
</script>

<style scoped>
.NotFound {
  height: 60vh;
  display: flex;
  flex-direction: column;
  justify-content: space-evenly;
  align-items: center;
}

h1 {
  font-size: 5em;
}

p {
  font-size: 1.3em;
}

.oops {
  display: flex;
  justify-content: space-around;
  align-items: center;
  flex-direction: column;
  height: 30%;
}

.linkdiv {
  display: flex;
  align-items: center;
  justify-content: space-around;
  width: 10%;
}

.link {
  font-size: 1.3em;
}
</style>
