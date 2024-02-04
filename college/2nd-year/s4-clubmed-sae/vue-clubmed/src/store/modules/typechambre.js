import axios from 'axios';

export default {
  namespaced: true,
  state: {
    typeChambres: []
  },
  mutations: {
    UPDATE_TYPECHAMBRES(state, typeChambres) {
      state.typeChambres = typeChambres;
    }
  },
  actions: {
    fetchTypeChambres({ commit, rootState }) {
      const headers = rootState.headers;
      axios
        .get('/api/typechambres', { headers })
        .then((data) => {
          commit('UPDATE_TYPECHAMBRES', data.data);
        })
        .catch((e) => {
          console.log('typechambre error');
          console.log(e);
        });
    }
  },
  getters: {
    getById: (state) => (id) =>
      state.typeChambres.find((item) => item.typeChambreId === id),
    getByResortId: (state) => (id) =>
      state.typeChambres.find((item) => item.resortId === id)
  }
};
