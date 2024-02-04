<template>
  <div v-if="client" class="infos">
    <div class="row">
      <input type="text" placeholder="prenom*" v-model="client.prenom" />
      <input type="text" placeholder="nom*" v-model="client.nom" />
    </div>
    <div class="row">
      <select v-if="civilites" name="civilites" v-model="client.laCivilite">
        <option disabled value="">votre civilite: {{ client.laCivilite.libelle }}</option>
        <option v-for="civilite in civilites" :key="civilite.civiliteId" :value="civilite">
          {{ civilite.libelle }}
        </option>
      </select>
      <input type="tel" pattern="[0-9]{10}" placeholder="téléphone*" v-model="client.tel" />
    </div>
    <div class="row">
      <input type="date" placeholder="date de naissance*" v-model="dateNaissance" />
      <input type="text" placeholder="email*" v-model="client.email" />
    </div>
    <div class="row addressdiv">
      <input type="text" readonly placeholder="adresse*" v-model="address" />
      <div v-show="false" class="addresses">
        <div v-for="i in 15" :key="i" class="address">135 Avenue Thiers 33100 Bordeaux</div>
      </div>
    </div>
    <div class="row">
      <select v-model="client.lePays" v-if="pays" name="countries">
        <option disabled>Votre pays : {{ client.lePays.nom }}</option>
        <option v-for="country in pays" :key="country.paysId" :value="country">
          {{ country.nom }}
        </option>
      </select>
    </div>
    <button @click="change">Change</button>
  </div>
</template>

<script>
import { mapState } from 'vuex';

export default {
  name: 'PersonalInfos',
  created() {
    if (this.pays.length === 0) {
      this.$store.dispatch('pays/fetchPays');
    }

    if (this.civilites.length === 0) {
      this.$store.dispatch('civilite/fetchCivilites');
    }
  },
  methods: {
    change() {
      //this.$store.dispatch("changeInfos");
    }
  },
  computed: {
    address: {
      get() {
        return (
          this.client.numRue +
          '-' +
          this.client.nomRue +
          '-' +
          this.client.codePostal +
          '-' +
          this.client.ville
        );
      },
      set(value) {
        [this.client.numRue, this.client.nomRue, this.client.codePostal, this.client.ville] =
          value.split('-');
      }
    },
    dateNaissance: {
      set(value) {
        this.client.dateNaissance = value + 'T00:00:00';
      },
      get() {
        let date = this.client.dateNaissance;
        return date.substring(0, 4) + '-' + date.substring(5, 7) + '-' + date.substring(8, 10);
      }
    },
    ...mapState({
      client: (state) => {
        if (state.currentClient) {
          state.currentClient.laCivilite.lesClients = null;
        }
        return state.currentClient;
      },
      localisations: (state) => state.localisation.localisations,
      pays: (state) => {
        if (state.currentClient) {
          state.currentClient.lePays.lesClients = null;
        }
        return state.pays.pays;
      },
      civilites: (state) => state.civilite.civilites
    })
  }
};
</script>

<style scoped>
.infos {
  display: flex;
  flex-direction: column;
}

.row {
  display: flex;
  justify-content: space-evenly;
  margin: 20px 0 20px 0;
  flex-wrap: wrap;
}

input,
select {
  padding: 20px;
  border: 1px solid;
  border-radius: 8px;
  font-size: 1.5em;
  color: black;
}

.row > input {
  width: 30%;
}

.row > select {
  width: 33%;
}

.addressdiv {
  flex-direction: column;
  justify-content: center;
  align-items: center;
  position: relative;
  font-size: 10px;
}

.addresses {
  width: 33%;
  height: 205px;
  position: absolute;
  top: 70px;
  overflow: scroll;
}

.address {
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 10px;
  border: 1px solid;
  background: whitesmoke;
}

button {
  padding: 10px;
  font-size: 2em;
  border: none;
  background: #1b4397;
  color: white;
  font-weight: bold;
}

button:hover {
  cursor: pointer;
}
</style>
