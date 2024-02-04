<template>
  <div class="typeactivitediv">
    <h2 class="titletypeactivite">{{ title }}</h2>
    <div v-if="typeactivites" class="typeactivites">
      <div
        v-for="type in typeactivites"
        :key="type.typeActiviteId"
        @click="showActivite(type)"
        class="type"
      >
        <img v-if="getPhoto(type.photoId)" :src="getPhoto(type.photoId).lien" alt="Golf" />
        <h3>{{ type.titre }}</h3>
      </div>
    </div>
    <div v-else class="typeactivites">
      <div v-for="act in activites" :key="act.activiteId" class="type">
        <img src="/img/typeactivite/Activités-en-famille/Activités-en-famille.jpg" />
        <h3>{{ act.titre }}</h3>
      </div>
    </div>
    <div
      v-show="activiteShow"
      v-for="activite in getActivite(currentType.typeActiviteId)"
      :key="activite"
      class="activites"
    >
      <Activite :activite="activite" />
    </div>
  </div>
</template>

<script>
import Activite from '@/components/Resort/Activite.vue';

import { mapGetters } from 'vuex';

export default {
  name: 'TypeActivite',
  components: { Activite },
  props: ['title', 'activites', 'tag'],
  data() {
    return {
      activiteShow: false,
      currentType: {}
    };
  },
  methods: {
    getPhoto(id) {
      return this.getPhotoById(id);
    },
    showActivite(type) {
      this.currentType = type;
      this.activiteShow = !this.activiteShow;
    },
    getActivite(typeId) {
      return this.activites.filter((item) => {
        return item.typeActiviteId === typeId;
      });
    }
  },
  computed: {
    typeactivites() {
      if (this.tag === 'adulte') {
        let types = [];
        for (let i of this.activites) {
          let isThere = false;
          let type = this.getTypeActivite(i.typeActiviteId);

          if (type) {
            for (let y of types) {
              if (y.typeActiviteId === type.typeActiviteId) {
                isThere = true;
                break;
              }
            }
            if (!isThere) {
              types.push(type);
            }
          }
        }
        return types;
      } else {
        return undefined;
      }
    },
    ...mapGetters({
      getTypeActivite: 'typeactivite/getById',
      getPhotoById: 'photo/getById'
    })
  }
};
</script>

<style scoped>
.type {
  width: 300px;
  height: 240px;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: space-between;
  font-size: 1.3em;
  color: goldenrod;
  margin: 0 10px 0 10px;
}

.type:hover {
  transform: scale(0.95);
  transition-duration: 0.5s;
  cursor: pointer;
}

.type > img {
  width: 100%;
}

.titletypeactivite {
  margin: 10px 0 10px 0;
  font-size: 2em;
  border-bottom: 2px solid;
  padding: 10px 0 10px 10px;
  background: #1b4397;
  color: white;
}

.typeactivites {
  display: flex;
  flex-wrap: wrap;
  justify-content: start;
  align-items: center;
}

.typeactivitediv {
  width: 100%;
}
</style>
