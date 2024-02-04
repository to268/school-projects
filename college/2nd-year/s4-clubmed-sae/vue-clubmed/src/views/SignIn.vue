<template>
  <div class="main">
    <div class="form">
      <div class="titlediv">
        <div class="title">
          <img src="/icon.png" alt="" />
          <h1>Sign In</h1>
        </div>
        <div class="border"></div>
      </div>
      <div class="inputs">
        <select v-if="pays" v-model="country" name="countries" class="input">
          <option disabled>Veuillez sélectionner votre pays</option>
          <option v-for="pays in pays" :value="pays" :key="pays.paysId">
            {{ pays.nom }}
          </option>
        </select>

        <select v-if="civilites" v-model="civilite" name="civility" class="input">
          <option v-for="civ in civilites" :value="civ" :key="civ.civiliteId">
            {{ civ.libelle }}
          </option>
        </select>

        <input v-model="nom" class="input" type="text" name="name" placeholder="nom*" id="nom" />
        <input
          v-model="prenom"
          class="input"
          type="text"
          name="surname"
          placeholder="prénom*"
          id="prenom"
        />
        <div style="color: red" v-if="champsfailed" class="champsFailed">
          les champs ne doivent pas être vides
        </div>
        <input
          v-model="email"
          class="input"
          type="email"
          name="email"
          placeholder="email*"
          id="email"
        />
        <div style="color: red" v-if="emailfailed" class="emailFailed">
          l'email n'est pas bien formatté
        </div>
        <input
          v-model="telephone"
          class="input"
          type="tel"
          name="phone"
          placeholder="téléphone"
          id="telephone"
        />
        <div style="color: red" v-if="telfailed" class="telFailed">
          Le téléphone doit avoir 10 nombres
        </div>
        <input
          v-model="pwd"
          class="input"
          type="password"
          name="password"
          placeholder="mot de passe*"
          id="password"
        />
        <input
          v-model="repeatPwd"
          class="input"
          type="password"
          name="confirmpwd"
          placeholder="confirmation mot de passe*"
          id="repeatpassword"
        />
        <div style="color: red" v-if="pwdfailed" class="pwdFailed">
          Ce ne sont pas les même mots de passe
        </div>

        <div class="droit cgu input">
          <input v-model="isCGUAccepted" type="checkbox" name="cgu" id="cgu" />
          <p>J'accepte les Conditions Générales de Vente</p>
        </div>
        <div style="color: red" v-if="cguFailed" class="cguFailed">vous devez accepte les CGU</div>
        <div class="droit politic input">
          <input v-model="isPoliticAccepted" type="checkbox" name="politic" id="politique" />
          <p>J'ai pris connaissance de la politique de protection de mes données</p>
        </div>
        <div style="color: red" v-if="polFailed" class="polFailed">
          vous devez accepte les politiques
        </div>

        <div
          style="
            width: 100%;
            color: red;
            font-size: 1.5em;
            display: flex;
            align-content: center;
            justify-content: center;
          "
          v-if="failed"
          class="failed"
        >
          <p>votre inscription n'a pas marché</p>
        </div>

        <div class="creer">
          <button @click="create" class="buttcreer">Créer le compte</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex';
import bcrypt from 'bcryptjs';

export default {
  name: 'SignIn',
  data() {
    return {
      nom: '',
      prenom: '',
      email: '',
      telephone: '',
      pwd: '',
      repeatPwd: '',
      failed: false,
      isCGUAccepted: false,
      isPoliticAccepted: false,
      emailfailed: false,
      telfailed: false,
      pwdfailed: false,
      champsfailed: false,
      cguFailed: false,
      polFailed: false
    };
  },
  created() {
    if (this.client) {
      this.$router.push({ path: '/' });
    }
  },
  computed: {
    civilite() {
      if (this.civilites && this.civilites.length !== 0) {
        return this.civilites[0];
      } else {
        return {};
      }
    },
    country() {
      if (this.pays && this.pays.length !== 0) {
        return this.pays.find((item) => item.nom === 'France');
      } else {
        return {};
      }
    },
    ...mapState({
      client: (state) => state.currentClient,
      pays: (state) => state.pays.pays,
      civilites: (state) => state.civilite.civilites,
      headers: (state) => state.headers
    })
  },
  watch: {
    client: function (val) {
      if (val) {
        this.$router.push({ name: 'Resorts' });
      }
    }
  },
  methods: {
    async create() {
      if (this.verifInfos()) {
        const salt = bcrypt.genSaltSync(10);
        const pwdHash = bcrypt.hashSync(this.pwd, salt);
        let client = {
          civiliteId: this.civilite.civiliteId,
          paysId: this.country.paysId,
          nom: this.nom,
          prenom: this.prenom,
          dateNaissance: '2020-10-09T00:00:00Z',
          email: this.email,
          tel: this.telephone,
          numRue: 69,
          nomRue: 'Rue deez nuts',
          codePostal: '69420',
          ville: 'deez',
          password: pwdHash
        };
        this.$store.dispatch('registerClient', client);
        this.failed = false;
      } else {
        this.failed = true;
      }
    },
    verifInfos() {
      const emailRe = /^([A-z])+@([A-z])+.([A-z])+$/g;
      const telRe = /^[0-9]{10}$/g;

      if (emailRe.exec(this.email) === null) {
        this.emailfailed = true;
      } else {
        this.emailfailed = false;
      }

      if (this.nom.length === 0 || this.prenom.length === 0) {
        this.champsfailed = true;
      } else {
        this.champsfailed = false;
      }

      if (telRe.exec(this.telephone) === null) {
        this.telfailed = true;
      } else {
        this.telfailed = false;
      }

      if (this.repeatPwd !== this.pwd) {
        this.pwdfailed = true;
      } else {
        this.pwdfailed = false;
      }

      if (!this.isCGUAccepted) {
        this.cguFailed = true;
      } else {
        this.cguFailed = false;
      }

      if (!this.isPoliticAccepted) {
        this.polFailed = true;
      } else {
        this.polFailed = false;
      }

      let verif =
        this.nom.length !== 0 &&
        this.prenom.length !== 0 &&
        emailRe.exec(this.email) !== null &&
        telRe.exec(this.telephone) !== null &&
        this.pwd.length >= 8 &&
        this.repeatPwd === this.pwd &&
        this.isCGUAccepted &&
        this.isPoliticAccepted;

      return !verif;
    }
  }
};
</script>

<style scoped>
.main {
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
  padding: 50px 0 50px 0;
}

.google {
  display: flex;
  margin-top: 20px;
}

.google > a {
  text-decoration: none;
}

.form {
  border: 1px solid;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  width: 600px;
  padding: 50px 0 50px;
  border-radius: 15px;
}

.inputs {
  display: flex;
  justify-content: center;
  align-items: start;
  flex-direction: column;
  margin: 20px 0 20px;
}

.droit {
  display: flex;
}

.droit.politic {
  align-items: start;
}

.input {
  margin-top: 20px;
  padding: 10px;
  width: 90%;
}

.titlediv {
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}

.title {
  display: flex;
  justify-content: center;
  align-items: center;
}

.title > img {
  width: 50px;
}

.title > h1 {
  color: #1b4397;
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
}

.border {
  width: 100%;
  border: 1px solid #1b4397;
  margin-top: 20px;
}

.creer {
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
}

.creer > button {
  padding: 10px;
  font-size: 1.5em;
  background-color: #1b4397;
  border: 1px solid #1b4397;
  color: white;
  font-weight: bold;
}

.creer > button:hover {
  cursor: pointer;
}

input,
select {
  border: 1px solid black;
}
</style>
