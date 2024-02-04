import axios from 'axios';

export default {
  namespaced: true,
  state: {
    domaineSkiables: []
  },
  mutations: {
    UPDATE_DOMAINES(state, domaineSkiables) {
      state.domaineSkiables = domaineSkiables;
    }
  },
  actions: {
    fetchDomaines({ commit, rootState }) {
      const headers = rootState.headers;
      axios
        .get('/api/domaineskiable', { headers })
        .then((data) => {
          commit('UPDATE_DOMAINES', data.data);
        })
        .catch((e) => console.log(e));
    }
  },
  getters: {
    getById: (state) => (id) =>
      state.domaineSkiables.find((item) => item.domaineSkiableId === id)
  }
};
