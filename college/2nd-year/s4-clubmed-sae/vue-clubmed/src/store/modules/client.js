import axios from 'axios';

export default {
  namespaced: true,
  state: {
    clients: []
  },
  mutations: {
    UPDATE_CLIENT(state, client) {
      state.clients.push(client);
    }
  },
  actions: {
    fetchClientById({ commit, rootState }, id) {
      const headers = rootState.headers;
      axios
        .get('/api/client/GetById/' + id, { headers })
        .then((data) => {
          commit('UPDATE_CLIENT', data.data);
        })
        .catch((e) => console.log(e));
    }
  },
  getters: {
    getById: (state) => (id) =>
      state.clients.find((item) => item.clientId === id)
  }
};
