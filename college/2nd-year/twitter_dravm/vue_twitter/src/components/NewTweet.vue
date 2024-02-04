<script setup>
import { ref } from "vue";
import { twitterStore } from "@/stores/twitter";

const twitter = twitterStore();

const content = ref("");

function add() {
  twitter.sendTweet(content);
}
</script>

<template>
  <div class="form-control">
    <div class="input-group">
      <input
        type="text"
        placeholder="Search…"
        class="input input-bordered"
        v-model="content"
      />
      <button class="btn btn-square" @click="add">Post</button>
    </div>
  </div>
</template>
