import axios from 'axios';

export default {
  namespaced: true,
  state: {
    civilites: []
  },
  mutations: {
    UPDATE_CIVILITES(state, civilites) {
      state.civilites = civilites;
    }
  },
  actions: {
    fetchCivilites({ commit, rootState }) {
      const headers = rootState.headers;
      axios
        .get('/api/civilites', { headers })
        .then((data) => {
          commit('UPDATE_CIVILITES', data.data);
        })
        .catch((e) => console.log(e));
    }
  },
  getters: {
    getById: (state) => (id) =>
      state.civilites.find((item) => item.civiliteId === id)
  }
};
