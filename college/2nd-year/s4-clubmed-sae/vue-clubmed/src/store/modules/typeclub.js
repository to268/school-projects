import axios from 'axios';

export default {
  namespaced: true,
  state: {
    typeclubs: []
  },
  mutations: {
    UPDATE_TYPECLUBS(state, typeclubs) {
      state.typeclubs = typeclubs;
    }
  },
  actions: {
    fetchTypeclubs({ commit, rootState }) {
      const headers = rootState.headers;
      axios
        .get('/api/typeclub', { headers })
        .then((data) => {
          commit('UPDATE_TYPECLUBS', data.data);
        })
        .catch((e) => {
          console.log(e);
        });
    }
  },
  getters: {
    getById: (state) => (id) =>
      state.typeclubs.find((item) => item.typeClubId === id)
  }
};
