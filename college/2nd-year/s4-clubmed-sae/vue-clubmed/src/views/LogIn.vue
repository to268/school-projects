<template>
  <div class="main">
    <div class="login">
      <div class="titlediv">
        <div class="title">
          <img src="/icon.png" alt="" />
          <h1>Log in</h1>
        </div>
        <div class="border"></div>
      </div>
      <input v-model="email" type="email" placeholder="email" />
      <input v-model="password" type="password" placeholder="mot de passe" />
      <div style="color: red" v-if="failed" class="failed">
        Vos crédentials ne sont pas les bons
      </div>

      <button @click="connect" class="connect">Se connecter</button>
    </div>
    <div class="forgot">
      <router-link :to="{ name: 'SignIn' }">
        Se créer un compte
        <AnOutlinedArrowRight />
      </router-link>
    </div>
  </div>
</template>

<script>
import { AnOutlinedArrowRight } from '@kalimahapps/vue-icons/an';

import axios from 'axios';
import { mapState } from 'vuex';
import bcrypt from 'bcryptjs';

export default {
  name: 'LogIn',
  components: {
    AnOutlinedArrowRight
  },
  created() {
    if (this.client) {
      this.$router.push({ path: '/' });
    }
  },
  computed: {
    ...mapState({
      headers: (state) => state.headers,
      token: (state) => state.token
    })
  },
  data() {
    return {
      password: '',
      email: '',
      failed: false
    };
  },
  methods: {
    async connect() {
      const headers = this.headers;
      const salt = bcrypt.genSaltSync(10);
      const pwdHash = bcrypt.hashSync(this.password, salt);
      let pwdHashApi = await axios.post('/api/client/HashMdp?mdp=' + pwdHash, '', { headers });
      let client = await axios.get('/api/client/GetByEmail/' + this.email, { headers });
      if (client.data.password) {
        bcrypt.compare(client.data.password, pwdHashApi.data, async (err, result) => {
          if (result) {
            this.$store.dispatch('fetchClient', client.data.clientId);
            this.$router.push({ name: 'Resorts' });
          } else {
            this.failed = true;
          }
        });
      } else {
        this.failed = true;
      }
    }
  }
};
</script>

<style scoped>
.main {
  height: 90vh;
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
}

.login {
  border: 1px solid;
  height: 400px;
  width: 300px;
  border-radius: 10px;
  display: flex;
  flex-direction: column;
  justify-content: space-evenly;
  align-items: center;
}

.titlediv {
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}

.title {
  display: flex;
  justify-content: center;
  align-items: center;
}

.title > img {
  width: 50px;
}

.title > h1 {
  color: #1b4397;
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
}

.border {
  width: 100%;
  border: 1px solid #1b4397;
  margin-top: 20px;
}

.login > input {
  background-color: transparent;
  border: 1px solid;
  padding: 10px;
}

.login > input:focus {
  outline: none;
}

.login > button {
  background-color: #1b4397;
  color: white;
  padding: 10px;
  font-size: 1.5em;
  border: none;
}

.login > button:hover {
  cursor: pointer;
}

.forgot > a {
  margin: 20px;
  display: flex;
  justify-content: center;
  align-items: center;
}
</style>
