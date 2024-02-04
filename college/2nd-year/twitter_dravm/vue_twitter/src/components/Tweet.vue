<script setup>
import { twitterStore } from "@/stores/twitter";
const twitter = twitterStore();
</script>

<script>
export default {
  props: ["tweet", "users"],
};
</script>

<template>
  <article class="stats shadow">
    <div class="stat">
      <div class="stat-title">
        {{ users.find((user) => user.id == tweet.user).name }} :
      </div>
      <div class="stat-desc">{{ tweet.content }}</div>
    </div>
  </article>
  <br />
</template>

<style>
article {
  padding: 1rem;
  margin: 1rem;
  border-radius: 0.5rem;
  background-color: #fff;
  box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15);
}
</style>
