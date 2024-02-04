<template>
  <div v-if="resort" class="main">
    <div class="maindiv">
      <div class="avisdiv other">
        <h2>Les autres avis</h2>
        <div v-if="lesAvis.length !== 0" class="avis other">
          <Avis v-for="avis in lesAvis" :resort="resort" :avis="avis" :key="avis.avisId" />
        </div>
        <div v-else class="noAvis">
          <h1>Il n'y a pas d'avis</h1>
        </div>
      </div>
      <div class="avisdiv mine">
        <h2>Mes avis</h2>
        <div v-if="mesAvis.length !== 0" class="avis mine">
          <Avis v-for="avis in mesAvis" :resort="resort" :avis="avis" :key="avis.avisId" />
        </div>
        <div v-else-if="client" class="noAvis">
          <h1>Vous n'avez pas d'avis</h1>
        </div>
        <div v-else class="noAvis">
          <h1>Vous n'êtes pas connecté</h1>
        </div>
      </div>
      <div class="avisdiv addavis">
        <h2>Ajouter un avis</h2>
        <div v-if="client" class="form">
          <div>
            <h3>Note</h3>
            <select v-model="note">
              <option v-for="i in optionsNote" :key="i">{{ i }}</option>
            </select>
          </div>
          <div class="commentairediv">
            <h3>Commentaire</h3>
            <textarea class="commentaire" v-model="commentaire" />
          </div>
          <button class="add" @click="add">Ajouter</button>
        </div>
        <div v-else class="notCo">
          <h1>Vous devez être connecté pour ajouter un avis</h1>
        </div>
      </div>
    </div>
  </div>
  <ProgressIndicator v-else class="progress" />
</template>

<script>
import Avis from '@/components/Resort/Avis.vue';
import ProgressIndicator from '@/components/ProgressIndicator.vue';

import { mapState } from 'vuex';

export default {
  name: 'AvisResort',
  props: ['resort'],
  data() {
    return {
      optionsNote: [0, 0.5, 1, 1.5, 2, 2.5, 3, 3.5, 4, 4.5, 5],
      note: 0,
      commentaire: ''
    };
  },
  components: {
    Avis,
    ProgressIndicator
  },
  methods: {
    getDate() {
      let date = new Date();
      return date.getFullYear() + '-' + date.getMonth() + '-' + date.getDay() + 'T00:00:00';
    },
    add() {
      if (this.commentaire !== '') {
        let avis = {
          resortId: this.resort.resortId,
          clientId: this.client.clientId,
          photoId: null,
          commentaire: this.commentaire,
          note: this.note,
          date: this.getDate()
        };
        this.$store.dispatch('resort/addAvis', {
          resort: this.resort,
          avis: avis
        });
      }
    }
  },
  computed: {
    mesAvis() {
      if (this.client) {
        return this.resort.lesAvis.filter((item) => {
          return item.clientId === this.client.clientId;
        });
      } else {
        return [];
      }
    },
    lesAvis() {
      if (this.client) {
        return this.resort.lesAvis.filter((item) => {
          return item.clientId !== this.client.clientId;
        });
      } else {
        return this.resort.lesAvis;
      }
    },
    ...mapState({
      client: (state) => state.currentClient
    })
  }
};
</script>

<style scoped>
.main {
  display: flex;
  align-items: center;
  justify-content: center;
}

.maindiv {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  width: 90%;
}

.avis {
  display: flex;
}

.avisdiv {
  width: 100%;
  overflow-x: scroll;
}

.avisdiv > h2 {
  font-size: 5em;
  border-bottom: 5px solid #1b4397;
}

.noAvis {
  height: 30vh;
  display: flex;
  align-items: center;
}

.noAvis > h1 {
  color: red;
  font-size: 2em;
}

.form {
  width: 50%;
  height: 500px;
  border: 1px solid;
  margin: 30px;
  display: flex;
  justify-content: space-evenly;
  align-items: start;
  flex-direction: column;
  padding: 20px;
}

textarea {
  resize: none;
  height: 100%;
  width: 100%;
  border: 1px solid black;
}

select {
  background: transparent;
  border: 1px solid;
  padding: 10px;
}

.commentairediv {
  width: 100%;
}

.add {
  background-color: #1b4397;
  border-radius: 8px;
  color: white;
  padding: 20px;
}

.notCo {
  height: 30vh;
  font-size: 2em;
  display: flex;
  justify-content: center;
  align-items: center;
  color: red;
}
</style>
