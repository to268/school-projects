<template>
  <div class="chambre">
    <h1 class="name">{{ chambre.libelleCatgorie }}</h1>
    <p class="description">
      {{ chambre.presentation }}
    </p>
    <div class="lists">
      <div class="pointsfortsdiv">
        <h2>Points forts</h2>
        <ul class="pointsforts">
          <li class="pointsfort" :key="i" v-for="i in 10">Coin Salon</li>
        </ul>
      </div>
      <div class="commoditediv">
        <h2>Commodites</h2>
        <ul class="commodites">
          <li class="commodite" :key="i" v-for="i in 40">lit bébé (dès la réservation)</li>
        </ul>
      </div>
    </div>
    <div class="details">
      <div class="price">prix journalier: {{ chambre.prixJournalier }} €</div>
      <div v-show="chambre.capacite" class="capacity">max {{ chambre.capacite }} personne(s)</div>
      <div v-show="chambre.surface" class="surface">{{ chambre.surface }} m²</div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Chambre',
  props: ['chambre']
};
</script>

<style scoped>
.chambre {
  width: 40%;
  background: whitesmoke;
  display: flex;
  flex-direction: column;
  padding: 30px;
  margin: 20px;
  box-shadow: 5px 5px 5px black;
}

.chambre:hover {
  transform: scale(1.05);
  transition-duration: 0.5s;
}

.description {
  word-wrap: break-word;
}

.name {
  color: goldenrod;
  font-size: 2.3em;
  display: flex;
  justify-content: center;
  margin-bottom: 10px;
}

.pointsforts {
  overflow: scroll;
}

.commoditediv,
.pointsfortsdiv {
  height: 100%;
  margin: 10px 0 10px 0;
}

.commodites,
.pointsforts {
  height: 85%;
  overflow: scroll;
}

.commodite,
.pointsfort {
  margin-top: 10px;
}

.lists {
  display: flex;
  justify-content: space-around;
  align-items: start;
  height: 200px;
  margin: 20px;
}

.details {
  margin-top: 10px;
  flex-grow: 1;
  display: flex;
  justify-content: space-around;
  align-items: center;
}

.price {
  font-size: 1.5em;
  color: goldenrod;
  font-weight: bold;
}

.capacity {
  background: #1b4397;
  padding: 5px;
  color: whitesmoke;
  font-weight: bold;
}
</style>
