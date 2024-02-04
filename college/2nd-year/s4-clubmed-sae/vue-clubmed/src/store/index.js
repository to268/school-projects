import axios from 'axios';

import { createStore } from 'vuex';

import resort from './modules/resort';
import typeactivite from './modules/typeactivite';
import typeclub from './modules/typeclub';
import localisation from './modules/localisation';
import typechambre from './modules/typechambre.js';
import restauration from './modules/restauration.js';
import photo from './modules/photo.js';
import client from './modules/client.js';
import domaineSkiable from './modules/domaineSkiable.js';
import pays from './modules/pays.js';
import reservation from './modules/reservation.js';
import transport from './modules/transport.js';
import civilite from './modules/civilite.js';

export default createStore({
  state: {
    currentClient: undefined,
    headers: undefined,
    adminProfile: undefined,
    token: undefined
  },
  mutations: {
    UPDATE_CLIENT(state, data) {
      state.currentClient = data;
    },
    DISCONNECT(state) {
      state.currentClient = undefined;
    },
    UPDATE_TOKEN(state, data) {
      state.adminProfile = data.userDetails;
      state.token = data.token;
      state.headers = {
        Authorization: 'Bearer ' + data.token
      };
    }
  },
  actions: {
    registerClient({ commit, rootState }, client) {
      const headers = rootState.headers;
      axios
        .post('/api/client', client, { headers })
        .then((data) => {
          commit('UPDATE_CLIENT', data.data);
        })
        .catch((e) => console.log(e));
    },
    fetchClient({ commit, state }, id) {
      const headers = state.headers;
      axios.get('/api/client/GetById/' + id, { headers }).then((data) => {
        commit('UPDATE_CLIENT', data.data);
      });
    },
    fetchToken({ commit, state }) {
      const headers = state.headers;
      axios
        .post(
          '/api/login',
          {
            Email: 'accesadmin@gmail.com',
            Password: 'gfdffFDGDF4154d561SDFGD45111sd1FDS153',
            UserRole: 'Admin'
          },
          { headers }
        )
        .then((data) => {
          commit('UPDATE_TOKEN', data.data);
        });
    },
    anonymisation({ commit, state }) {
      axios
        .put('/api/client/anonymisation/' + state.currentClient.clientId)
        .then((data) => {
          commit('UPDATE_CLIENT', data.data);
        });
    },
    changeInfos({ commit, state }) {
      state.currentClient.laCivilite.civiliteId = 0;
      state.currentClient.laCivilite.libelle = '';
      state.currentClient.laCivilite.lesAutreParticipants = [];
      state.currentClient.laCivilite.lesClients = [];

      state.currentClient.lePays.paysId = 0;
      state.currentClient.lePays.nom = '';

      axios
        .put('/api/client/' + state.currentClient.clientId, state.currentClient)
        .then(async (item) => {
          var client = axios.get('/api/client/GetById/' + id, { headers });
          commit('UPDATE_CLIENT', item.data);
        })
        .catch((e) => {
          console.warn(e);
        });
    },
    disconnect({ commit }) {
      commit('DISCONNECT');
    }
  },
  modules: {
    resort,
    typeactivite,
    localisation,
    typeclub,
    typechambre,
    restauration,
    photo,
    client,
    domaineSkiable,
    pays,
    reservation,
    transport,
    civilite
  }
});
