import axios from 'axios';

export default {
  namespaced: true,
  state: {
    restaurants: [],
    bars: []
  },
  mutations: {
    UPDATE_RESTAURATION(state, { restaurants, bars }) {
      state.restaurants = restaurants;
      state.bars = bars;
    }
  },
  actions: {
    fetchRestauration({ commit, rootState }) {
      let obj = {
        restaurants: [],
        bars: []
      };
      const headers = rootState.headers;
      axios
        .get('/api/restaurants', { headers })
        .then((data) => {
          obj.restaurants = data.data;
          axios
            .get('/api/bars', { headers })
            .then((data) => {
              obj.bars = data.data;
              commit('UPDATE_RESTAURATION', obj);
            })
            .catch((e) => console.log(e));
        })
        .catch((e) => console.log(e));
    }
  },
  getters: {
    getRestaurantById: (state) => (id) =>
      state.restaurants.find((item) => item.restaurantId === id),
    getBarById: (state) => (id) => state.bars.find((item) => item.barId === id),
    getRestaurantsByResortId: (state) => (id) =>
      state.restaurants.filter((item) => item.resortId === id),
    getBarsByResortId: (state) => (id) =>
      state.bars.filter((item) => item.resortId === id)
  }
};
