<template>
  <h1 class="titlepassword">Change password</h1>
  <div class="password">
    <input v-model="previousPwd" type="text" placeholder="Previous password*" />
    <p v-if="areNotTheSamePrevious" style="color: red">Not the same as the previous one</p>
    <input v-model="newPwd" type="text" placeholder="New password*" />
    <input v-model="repeatPwd" ype="text" placeholder="Repeat password*" />
    <p v-if="areNotTheSameNew" style="color: red">They are not the same</p>
    <button>Change</button>
  </div>
</template>

<script>
import bcrypt from 'bcryptjs';
import axios from 'axios';

import { mapState } from 'vuex';

export default {
  name: 'Password',
  data() {
    return {
      previousPwd: '',
      newPwd: '',
      repeatPwd: '',
      areNotTheSamePrevious: false,
      areNotTheSameNew: false
    };
  },
  methods: {
    async changePwd() {
      this.areNotTheSameNew = false;
      this.areNotTheSamePrevious = false;

      if (this.previousPwd !== '') {
        const previousPwdHash = await bcrypt.hash(this.previousPwd, 10);
        if (previousPwdHash !== this.client.MotDePasse) {
          this.areNotTheSamePrevious = true;
        } else {
          if (this.newPwd !== this.repeatPwd) {
            this.areNotTheSameNew = true;
          } else {
            const newPwdHash = await bcrypt.hash(this.newPwd, 10);
            this.client.MotDePasse = newPwdHash;
            this.$store.dispatch('changeInfos');
          }
        }
      }
    }
  },
  computed: {
    ...mapState({
      client: (state) => state.currentClient,
      headers: (state) => state.headers
    })
  }
};
</script>

<style scoped>
.password {
  display: flex;
  align-items: center;
  flex-direction: column;
  justify-content: space-evenly;
  border: 1px solid;
  margin: 10px;
  padding: 10px;
  border-radius: 8px;
  height: 400px;
}

input,
select {
  padding: 20px;
  border: 1px solid;
  border-radius: 8px;
  font-size: 1.5em;
  color: black;
  width: 50%;
}

.password > button {
  padding: 10px;
  font-size: 2em;
  border: none;
  background: #1b4397;
  border-radius: 8px;
  color: white;
  font-weight: bold;
}

button:hover {
  cursor: pointer;
}

.titlepassword {
  margin-top: 50px;
}
</style>
