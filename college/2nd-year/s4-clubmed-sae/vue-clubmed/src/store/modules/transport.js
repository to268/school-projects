import axios from 'axios';

export default {
  namespaced: true,
  state: {
    transports: []
  },
  mutations: {
    UPDATE_TRANSPORTS(state, transports) {
      state.transports = transports;
    }
  },
  actions: {
    fetchTransports({ commit, rootState }) {
      const headers = rootState.headers;
      axios
        .get('/api/transport', { headers })
        .then((data) => {
          commit('UPDATE_TRANSPORTS', data.data);
        })
        .catch((e) => console.log(e));
    }
  },
  getters: {
    getById: (state) => (id) =>
      state.transports.find((item) => item.transportId === id)
  }
};
