import axios from 'axios';

export default {
  namespaced: true,
  state: {
    isLoading: true,
    resort: undefined,
    sejourResorts: [],
    strFilter: '',
    locFilter: undefined,
    typeFilter: undefined,
    resorts: []
  },
  mutations: {
    UPDATE_CURRENT_RESORT(state, resort) {
      state.resort = resort;
    },
    UPDATE_RESORTS(state, resorts) {
      state.resorts.push(...resorts);
      state.isLoading = false;
    },
    UPDATE_FILTERS_STR(state, str) {
      state.strFilter = str;
      state.locFilter = undefined;
      state.typeFilter = undefined;
    },
    UPDATE_FILTERS_LOC(state, loc) {
      state.locFilter = loc;
      state.strFilter = '';
    },
    UPDATE_FILTERS_TYPE(state, type) {
      state.typeFilter = type;
      state.strFilter = '';
    },
    UPDATE_SEJOURS_RESORTS(state, resort) {
      state.sejourResorts.push(resort);
    }
  },
  actions: {
    addAvis({ rootState }, obj) {
      if (obj) {
        axios
          .post('/api/avis', obj.avis, { headers: rootState.headers })
          .then((data) => {
            rootState.currentClient.lesAvis.push(data.data);
            obj.resort.lesAvis.push(data.data);
          })
          .catch((e) => {
            console.log(e);
          });
      }
    },
    filter({ commit }, filter) {
      if (filter) {
        if (typeof filter === 'string') {
          commit('UPDATE_FILTERS_STR', filter);
        } else if (filter.localisationId) {
          commit('UPDATE_FILTERS_LOC', filter);
        } else {
          commit('UPDATE_FILTERS_TYPE', filter);
        }
      }
    },
    fetchSejourResorts({ commit, rootState }, obj) {
      if (obj) {
        const headers = rootState.headers;
        for (var i of obj) {
          axios
            .get('/api/Resort/GetById/' + i, { headers })
            .then((data) => {
              commit('UPDATE_SEJOURS_RESORTS', data.data);
            })
            .catch((e) => console.log(e));
        }
      }
    },
    async fetchResorts({ commit, rootState }, obj) {
      if (obj) {
        const headers = rootState.headers;
        let resorts = [];
        for (var i = obj.min; i <= obj.max; i++) {
          var data = await axios.get('/api/Resort/GetById/' + i, { headers });
          resorts.push(data.data);
        }
        commit('UPDATE_RESORTS', resorts);
      }
    },
    updateCurrentResort({ commit }, resort) {
      if (resort) {
        commit('UPDATE_CURRENT_RESORT', resort);
      }
    }
  },
  getters: {
    getFiltered: (state) => {
      let resorts = [...state.resorts];
      if (state.strFilter !== '') {
        resorts = resorts.filter((item) => {
          return item.nom.includes(state.strFilter);
        });
        return resorts;
      } else if (state.locFilter) {
        resorts = resorts.filter((item) => {
          return item.localisationId === state.locFilter.localisationId;
        });
      } else if (state.typeFilter) {
        resorts = resorts.filter((item) => {
          return (
            item.lesTypeClubs.find(
              (item1) => item1.typeClubId === state.typeFilter
            ) !== undefined
          );
        });
      }
      return resorts;
    },
    getById: (state) => (id) => {
      let resort = state.resorts.find((item) => item.resortId === id);
      return resort;
    },
    sejourGetById: (state) => (id) => {
      let resort = state.sejourResorts.find((item) => item.resortId === id);
      return resort;
    }
  }
};
