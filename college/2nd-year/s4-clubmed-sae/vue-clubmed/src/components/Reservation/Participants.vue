<template>
  <div id="title">
    <h1>Saisissez le nombre de participants, leurs noms / prénoms et date de naissance</h1>
    <div id="border"></div>
  </div>
  <div class="main">
    <div class="number">
      <p>Participants :</p>
      <input type="number" name="participants" v-model="number" min="1" max="10" />
    </div>
    <div class="participants">
      <Participant v-for="i in number" :number="i" :key="i" class="participant"> </Participant>
    </div>
  </div>
</template>

<script>
import Participant from '@/components/Reservation/Participant.vue';

export default {
  name: 'Participants',
  data() {
    return {
      number: 1
    };
  },
  components: {
    Participant
  }
};
</script>

<style scoped>
.main {
  display: flex;
  flex-direction: column;
  width: 80%;
}

.number {
  display: flex;
  justify-content: start;
  align-items: center;
  margin-left: 20px;
  margin-top: 20px;
}

.number > input {
  width: 50px;
}

.participants {
  display: flex;
  justify-content: space-evenly;
  align-items: center;
  flex-wrap: wrap;
  margin-top: 10px;
}

.participant {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: space-evenly;
  margin-top: 20px;
  padding: 10px;
  border: 1px solid;
}

#title > h1 {
  color: white;
  background-color: #1b4397;
  width: 80%;
  padding: 10px;
}

#title {
  width: 100%;
  display: flex;
  flex-direction: column;
  margin-bottom: 20px;
}

#border {
  margin: 15px 0 15px 0;
  border: 3px solid;
  border-radius: 10px;
}

button {
  text-align: center;
  color: white;
  margin-top: 20px;
  margin-bottom: 20px;
  background-color: #1b4397;
  padding: 10px;
}

input {
  border: 1px solid;
  padding: 10px;
  margin: 10px;
}
</style>
