<template>
  <header>
    <div class="left">
      <div class="logo">
        <img src="/img/logoClubMed.png" alt="Club Med Logo" />
        <h1>Club Med</h1>
      </div>
      <div class="links">
        <router-link class="link" :to="{ name: 'Resorts' }">Accueil</router-link>
      </div>
    </div>

    <div class="user" v-if="client">
      <router-link :to="{ name: 'Profile' }"> {{ client.nom }} {{ client.prenom }} </router-link>
      <BxUserCircle class="icon" />
    </div>
    <div v-else class="user">
      <div class="signin">
        <router-link :to="{ name: 'SignIn' }">Sign In</router-link>
      </div>
      <div class="login">
        <router-link :to="{ name: 'LogIn' }">Log In</router-link>
      </div>
    </div>
  </header>
</template>

<script>
import { BxUserCircle } from '@kalimahapps/vue-icons/bx';

import { mapState } from 'vuex';

export default {
  name: 'Banner',
  components: {
    BxUserCircle
  },
  computed: {
    ...mapState({
      client: (state) => state.currentClient
    })
  }
};
</script>

<style scoped>
@media screen and (max-width: 950px) {
  .left {
    flex-direction: column;
  }
  .link {
    margin-left: 50px;
  }
}

@media screen and (max-width: 860px) {
  .user {
    flex-direction: column;
    height: 100%;
  }
}

header {
  background-color: #1b4397;
  display: flex;
  justify-content: space-between;
  height: 100px;
  align-items: center;
  padding: 10px;
}

.logo {
  display: flex;
  width: 280px;
  font-size: 1.5em;
  justify-content: space-between;
  align-items: center;
  color: white;
}

.left {
  display: flex;
  align-items: center;
  width: 60%;
}

.logo > img {
  width: 50px;
  height: 50px;
  border-radius: 15px;
}

.links {
  display: flex;
  flex-grow: 1;
  justify-content: space-evenly;
  width: 30%;
}

.link {
  color: white;
  font-weight: bold;
  font-size: 2em;
}

a {
  text-decoration: none;
}

.links a.router-link-exact-active {
  border-bottom: 2px solid;
  border-block-end-color: black;
  border-block-start-color: black;
  animation-name: borderAnim;
  animation-duration: 0.5s;
  animation-iteration-count: 1;
  animation-delay: 0s;
  animation-fill-mode: forwards;
}

@keyframes borderAnim {
  0% {
    border-block-start-color: transparent;
    border-block-end-color: transparent;
  }
  50% {
    border-block-start-color: white;
    border-block-end-color: transparent;
  }
  100% {
    border-block-start-color: white;
    border-block-end-color: white;
  }
}

.user {
  margin-right: 3%;
  display: flex;
  justify-content: space-between;
  align-items: center;
  font-size: 1.3em;
  width: 200px;
}

.signin > a {
  color: white;
}

.login {
  background-color: white;
  padding: 15px;
  border-radius: 8px;
}

.login > a {
  color: #1b4397;
}

.user > .icon {
  width: 50px;
  height: 50px;
}

.user > a {
  color: black;
  text-decoration: none;
}
</style>
