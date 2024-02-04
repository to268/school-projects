<template>
  <div class="main">
    <div id="title">
      <h1>Choisissez un type de chambre</h1>
      <div id="border" />
    </div>
    <div v-if="resort" class="types">
      <div
        :style="{
          backgroundColor:
            currentTypeChambre.typeChambreId === type.typeChambreId ? '#1b4397' : 'transparent',
          color: currentTypeChambre.typeChambreId === type.typeChambreId ? 'white' : 'black'
        }"
        @click="toggle(type)"
        v-for="type in resort.lesTypeChambres"
        :key="type.typeChambreId"
        class="type"
      >
        <h2>{{ type.libelleCatgorie }}</h2>
        <h3>{{ type.surface }} m²</h3>
        <h3>{{ type.capacite }} personne(s)</h3>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex';

export default {
  name: 'RoomType',
  props: ['resort'],
  data() {
    return {
      type: undefined
    };
  },
  methods: {
    toggle(type) {
      this.currentTypeChambre = type;
      if (!this.lesTypeChambres.includes(type)) {
        if (this.lesTypeChambres.length > 0) {
          this.lesTypeChambres.splice(0, 1);
        }
        this.lesTypeChambres.push(type);
      }
    }
  },
  computed: {
    ...mapState({
      lesTypeChambres: (state) => state.reservation.lesTypeChambres
    }),
    currentTypeChambre: {
      get() {
        if (!this.type && this.resort) {
          return this.resort.lesTypeChambres[0];
        } else if (this.resort) {
          return this.type;
        }
      },
      set(val) {
        this.type = val;
      }
    }
  }
};
</script>

<style scoped>
.main {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  width: 100%;
  margin: 50px 0 30px 0;
}

.types {
  width: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  flex-wrap: wrap;
}

.type {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  height: 60%;
  margin-left: 30px;
  margin-right: 30px;
  padding: 10px;
}

.type:hover {
  background-color: rgb(68, 109, 192);
  cursor: pointer;
}

#title > h1 {
  color: white;
  background-color: #1b4397;
  width: 45%;
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
</style>
