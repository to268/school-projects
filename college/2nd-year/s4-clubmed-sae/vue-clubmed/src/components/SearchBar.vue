<template>
  <div class="searchBar">
    <div class="bar">
      <div class="barContent">
        <input type="text" v-model="searchValue" placeholder="Search" @keyup.enter="search" />
        <div class="filters">
          <div class="filter">
            <h2>Lieux</h2>
            <select @change="changeLoc" v-model="localisation" class="filter lieux">
              <option v-for="loc in localisations" :value="loc" :key="loc.localisationId">
                {{ loc.nom }}
              </option>
            </select>
          </div>
          <div class="filter">
            <h2>Type club</h2>
            <select @change="changeType" v-model="typeclub" class="filter typeclub">
              <option v-for="type in typeclubs" :value="type.typeClubId" :key="type.typeclubId">
                {{ type.libelle }}
              </option>
            </select>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from 'vuex';

export default {
  name: 'SearchBar',
  data() {
    return {
      searchValue: '',
      localisation: {},
      typeclub: {}
    };
  },
  methods: {
    search() {
      this.$store.dispatch('resort/filter', this.searchValue);
      this.localisation = {};
      this.typeclub = {};
    },
    changeLoc() {
      this.$store.dispatch('resort/filter', this.localisation);
      this.searchValue = '';
    },
    changeType() {
      this.$store.dispatch('resort/filter', this.typeclub);
      this.searchValue = '';
    }
  },
  computed: {
    ...mapState({
      localisations: (state) => state.localisation.localisations,
      resorts: (state) => state.resort.resorts,
      typeclubs: (state) => state.typeclub.typeclubs
    })
  }
};
</script>

<style scoped>
@media screen and (max-width: 1200px) {
  .barContent {
    flex-direction: column;
  }
}

@media screen and (max-width: 850px) {
  .filters {
    flex-direction: column;
  }
}

.searchBar {
  display: flex;
  justify-content: center;
  align-items: center;
}

.bar {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 98%;
  border-radius: 100px;
  margin-top: 20px;
  border: 1px solid black;
}

.barContent {
  display: flex;
  width: 80%;
}

.barContent > input {
  border: none;
  font-size: 1.3em;
  width: 30%;
  display: flex;
  margin: 20px 0 20px 0;
}

.barContent > input::placeholder {
  font-size: 1.3em;
}

.barContent > input:focus {
  outline: none;
}

.filter {
  display: flex;
  margin: 20px 0 20px 0;
  justify-content: center;
  align-items: center;
}

.filter > h2 {
  margin-right: 10px;
}

.filters {
  display: flex;
  justify-content: space-around;
  flex-grow: 1;
}

select {
  background-color: transparent;
  border: 1px solid black;
  border-radius: 8px;
  font-size: 1.2em;
  margin-left: 5px;
}
</style>
