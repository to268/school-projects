<template>
  <div class="droitlayout">
    <h1>{{ title }}</h1>
    <div class="maindroitlayout">
      <slot />
    </div>
  </div>
</template>

<script>
export default {
  props: ['title']
};
</script>

<style>
.droitlayout {
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
  margin: 30px 0 30px 0;
}

.layout > h1 {
  width: 100%;
  border-bottom: 7px solid #1b4397;
  color: #1b4397;
  font-size: 3em;
  margin-bottom: 30px;
}

.maindroitlayout {
  background: whitesmoke;
  box-shadow: 3px 3px 3px black;
  width: 85%;
  height: 100%;
  padding: 50px;
}
</style>
