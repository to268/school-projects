<template>
  <footer>
    <div class="credits">
      <div class="logo">
        <img src="/img/logoClubMed.png" alt="Club Med Logo" />
        <h1>Club Med</h1>
      </div>
      <div class="links">
        <router-link :to="{ name: 'Cookies' }">Charte Cookies</router-link>
        <router-link :to="{ name: 'Politic' }">Politique de confidentialité</router-link>
        <router-link :to="{ name: 'Legal' }">Informations légales</router-link>
      </div>
    </div>
  </footer>
</template>

<script>
export default {
  name: 'Credits'
};
</script>

<style scoped>
footer {
  height: 60vh;
  background-color: darkgrey;
  display: flex;
  justify-content: space-around;
  align-items: center;
}

.logo {
  display: flex;
  align-items: center;
  font-size: 2em;
  color: white;
}

.logo > img {
  width: 100px;
  height: 100px;
  border-radius: 15px;
}

.credits {
  display: flex;
  width: 60%;
  height: 50%;
  justify-content: space-around;
  align-items: center;
}

.links {
  display: flex;
  flex-direction: column;
  height: 100%;
  align-items: center;
  justify-content: space-around;
  font-size: 1.3em;
}

a {
  text-decoration: none;
  color: black;
}

a:hover {
  border-bottom: 1px solid black;
}
</style>
