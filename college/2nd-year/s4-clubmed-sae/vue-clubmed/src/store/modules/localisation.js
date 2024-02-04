import axios from 'axios';

export default {
  namespaced: true,
  state: {
    localisations: []
  },
  mutations: {
    UPDATE_LOCALISATION(state, localisations) {
      state.localisations = localisations;
    }
  },
  actions: {
    fetchLocalisations({ commit, rootState }) {
      const headers = rootState.headers;
      axios
        .get('/api/localisation', { headers })
        .then((data) => {
          commit('UPDATE_LOCALISATION', data.data);
        })
        .catch((e) => console.log(e));
    }
  },
  getters: {
    getById: (state) => (id) =>
      state.localisations.find((item) => item.localisationId === id)
  }
};
