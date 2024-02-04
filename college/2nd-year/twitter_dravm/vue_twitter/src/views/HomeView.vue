<script setup>
import { twitterStore } from "@/stores/twitter";
import Tweet from "../components/Tweet.vue";
import NewTweet from "../components/NewTweet.vue";

const twitter = twitterStore();
</script>

<template>
  <section>
    <NewTweet />
    <Tweet
      v-if="twitter.token"
      v-for="tweet in twitter.tweets"
      :tweet="tweet"
      :users="twitter.users"
      :key="tweet.id"
    >
    </Tweet>
    <h3 v-else class="text-center m-4">
      Vous devez vous connecter pour voir votre fil personalisé
    </h3>

    <div v-if="twitter.token" class="btn-group grid grid-cols-2">
      <button class="btn btn-outline">Previous page</button>
      <button class="btn btn-outline">Next</button>
    </div>
  </section>
</template>

<style scoped>
section {
  margin: 5vh;
}
</style>
