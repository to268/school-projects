<template>
  <div class="profileinfos">
    <div class="image">
      <BsFilePersonFill class="photo" />
    </div>
    <div class="personal">
      <PersonalInfos />
      <Password />
      <Checks />
    </div>
    <div class="deconnexion">
      <button @click="deco">Se Déconnecter</button>
      <button @click="suppr">Supprimer mes données</button>
    </div>
  </div>
</template>

<script>
import { BsFilePersonFill } from '@kalimahapps/vue-icons/bs';
import Checks from '@/components/Profile/Checks.vue';
import PersonalInfos from '@/components/Profile/PersonalInfos.vue';
import Password from '@/components/Profile/Password.vue';

export default {
  name: 'Infos',
  components: {
    Password,
    PersonalInfos,
    Checks,
    BsFilePersonFill
  },
  methods: {
    deco() {
      this.$store.dispatch('disconnect');
      this.$router.push({ name: 'Resorts' });
    },
    suppr() {
      this.$store.dispatch('anonymisation');
    }
  }
};
</script>

<style scoped>
.profileinfos {
  width: 80%;
  background: whitesmoke;
  box-shadow: 0 0 10px #1b4397;
  display: flex;
  align-items: start;
  margin: 40px 0 40px;
  justify-content: start;
  padding: 50px;
  flex-wrap: wrap;
}

.photo {
  width: 200px;
  height: 200px;
}

.personal {
  display: flex;
  flex-direction: column;
  flex-grow: 1;
}

.deconnexion {
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: space-evenly;
  height: 100px;
}

.deconnexion > button {
  padding: 20px;
  background: red;
  color: white;
  font-weight: bold;
  font-size: 2em;
}

.deconnexion > button:hover {
  cursor: pointer;
}
</style>
