import axios from 'axios';

export default {
  namespaced: true,
  state: {
    pays: []
  },
  mutations: {
    UPDATE_PAYS(state, pays) {
      state.pays = pays;
    }
  },
  actions: {
    fetchPays({ commit, rootState }) {
      const headers = rootState.headers;
      axios
        .get('/api/pays', { headers })
        .then((data) => {
          commit('UPDATE_PAYS', data.data);
        })
        .catch((e) => console.log(e));
    }
  },
  getters: {
    getById: (state) => (id) => state.pays.find((item) => item.paysId === id)
  }
};
