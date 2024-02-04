<template>
  <div class="activite">
    <h2 class="title">{{ activite.titre }}</h2>
    <p v-show="activite.duree">Durée: {{ activite.duree }} min</p>
    <div class="descprice">
      <div class="description">
        <p>
          {{ activite.description }}
        </p>
        <p v-show="activite.frequence" class="horaire">
          {{ activite.frequence }}
        </p>
        <p v-show="activite.ageMin" class="horaire">
          Pour tout ceux au dessus de {{ activite.ageMin }} ans
        </p>
      </div>
    </div>
    <div v-show="activite.prix" class="price">
      <p>A partir de {{ activite.prix }} €</p>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Activite',
  props: ['activite']
};
</script>

<style scoped>
.activite {
  margin: 20px 0 20px 0;
  padding-left: 10px;
  border: 1px solid;
  display: flex;
  flex-direction: column;
  justify-content: space-evenly;
  align-items: stretch;
  height: 50vh;
  animation-name: slideActivites;
  animation-iteration-count: 1;
  animation-duration: 1s;
  animation-fill-mode: forwards;
}

@keyframes slideActivites {
  from {
    opacity: 0;
    height: 0;
  }
  to {
    opacity: 1;
  }
}

.horaire {
  font-weight: bold;
}

.price {
  padding: 10px;
  background: #1b4397;
  color: white;
  font-weight: bold;
  width: max-content;
}

.descprice {
  display: flex;
  justify-content: start;
  align-items: center;
}

.title {
  font-size: 2em;
}
</style>
