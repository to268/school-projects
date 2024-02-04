import { ref, reactive } from "vue";
import { defineStore } from "pinia";
import axios from "axios";

export const twitterStore = defineStore("twitter", () => {
  let tweets = reactive([]);
  let users = ref([]);
  let token = ref();

  function loadFeed() {
    console.log(token);
    axios.get("/api/tweet/feed").then((response) => {
      response.data.forEach((tweet) => {
        tweets.push(tweet);
        users.value.push(fetchUser(tweet["user"]));
      });
      console.log(tweets);
      console.log(users);
    });
  }

  function sendTweet(content) {
    axios
      .post(
        "/api/tweet",
        {
          content: content.value,
        },
        {
          headers: {
            Authorization: "Bearer " + token.value,
          },
        }
      )
      .then((response) => {
        console.log(response);
      });
  }

  function authenticate(handle, password) {
    axios
      .post("/api/user/auth", {
        handle: handle.value,
        password: password.value,
      })
      .then((response) => {
        console.log(response);
      });
  }

  function createAccount(name, handle, password) {
    axios
      .post("/api/user", {
        name: name.value,
        handle: handle.value,
        password: password.value,
      })
      .then((response) => {
        if ((response.status = 201)) {
          alert("compte crée");
        } else {
          alert("erreur de création du compte");
        }
      });
  }

  function loginAccount(handle, password) {
    axios
      .post("/api/user/auth", {
        handle: handle.value,
        password: password.value,
      })
      .then((response) => {
        token.value = response.data["access_token"];
        if ((response.status = 200)) {
          alert("connexion réussite");
        } else {
          alert("erreur de connexion");
        }
      });
  }

  function fetchUser(id) {
    axios.get("/api/user/" + id).then((response) => {
      return response;
    });
  }

  return {
    users,
    tweets,
    token,
    sendTweet,
    authenticate,
    createAccount,
    loginAccount,
    loadFeed,
  };
});
