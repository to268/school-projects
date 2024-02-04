import axios from 'axios';

export default {
  namespaced: true,
  state: {
    isLoading: true,
    activiteCartes: [],
    activiteEnfantCartes: [],
    activiteIncluses: [],
    activiteEnfantIncluses: []
  },
  mutations: {
    UPDATE_ACTIVITES(state, obj) {
      state[obj.prop] = obj.activites;
      state.isLoading = false;
    }
  },
  actions: {
    fetchActivites({ commit, rootState }, obj) {
      const headers = rootState.headers;
      axios
        .get('/api/' + obj.link, { headers })
        .then((data) => {
          commit('UPDATE_ACTIVITES', { data: data.data, obj: obj });
        })
        .catch((e) => console.log(e));
    }
  },
  getters: {
    getById: (state) => (id, prop, list) =>
      state[list].find((item) => item[prop] === id)
  }
};
