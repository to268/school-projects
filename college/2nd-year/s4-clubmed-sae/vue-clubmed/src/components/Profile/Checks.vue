<template>
  <div class="checks">
    <h4>J'accepte de recevoir les actualités Club Med (informations produits & offres)</h4>
    <div class="check">
      <input type="checkbox" />
      <p>Par mail</p>
    </div>
    <div class="check">
      <input type="checkbox" />
      <p>Par SMS ou Application mobile</p>
    </div>
    <div class="check">
      <input type="checkbox" />
      <p>Par téléphone</p>
    </div>
    <div class="check">
      <input type="checkbox" />
      <p>Par courrier postal</p>
    </div>
    <div class="check">
      <input type="checkbox" />
      <p>
        J'accepte de recevoir des contenus personnalisés** en fonction de mes interactions avec Club
        Med (parcours d'achat, navigation)
      </p>
    </div>
  </div>
  <button class="accepte">J'accepte</button>
</template>

<script>
export default {
  name: 'Checks'
};
</script>

<style scoped>
.checks {
  display: flex;
  flex-direction: column;
  margin-top: 20px;
}

.check {
  display: flex;
  margin: 5px 0 5px 0;
}

.check > p {
  margin-left: 10px;
}

.accepte {
  width: 20%;
  padding: 10px;
}

button {
  background: #1b4397;
  color: white;
  font-weight: bold;
  font-size: 1.3em;
  margin-top: 10px;
}

button:hover {
  cursor: pointer;
}
</style>
