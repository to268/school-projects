<script setup>
import { ref } from "vue";
import { twitterStore } from "@/stores/twitter";
const twitter = twitterStore();

const username = ref("");
const password = ref("");

function loginAccount() {
  twitter.loginAccount(username, password);
}
</script>

<template>
  <div class="hero min-h-screen bg-base-200">
    <div class="hero-content flex-col lg:flex-row-reverse">
      <div class="text-center lg:text-left">
        <h1 class="text-5xl font-bold">Connectez vous!</h1>
        <p class="py-6">Connectez vous pour poster vos premiers tweets</p>
      </div>
      <div class="card flex-shrink-0 w-full max-w-sm shadow-2xl bg-base-100">
        <div class="card-body">
          <div class="form-control">
            <label class="label">
              <span class="label-text">Pseudo</span>
            </label>
            <input
              type="text"
              placeholder="Pseudo"
              class="input input-bordered"
              v-model="username"
            />
          </div>
          <div class="form-control">
            <label class="label">
              <span class="label-text">Mot de passe</span>
            </label>
            <input
              type="text"
              placeholder="Mot de passe"
              class="input input-bordered"
              v-model="password"
            />
          </div>
          <div class="form-control mt-6">
            <button class="btn btn-primary" @click="loginAccount">
              Connection
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style></style>
