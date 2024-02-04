import axios from 'axios';

export default {
  namespaced: true,
  state: {
    typeActivites: []
  },
  mutations: {
    UPDATE_TYPEACTIVITES(state, typeActivites) {
      state.typeActivites = typeActivites;
    }
  },
  actions: {
    fetchTypeActivites({ commit, rootState }) {
      const headers = rootState.headers;
      axios
        .get('/api/typeactivite', { headers })
        .then((data) => {
          commit('UPDATE_TYPEACTIVITES', data.data);
        })
        .catch((e) => console.log(e));
    }
  },
  getters: {
    getById: (state) => (id) =>
      state.typeActivites.find((item) => item.typeActiviteId === id)
  }
};
