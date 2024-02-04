import axios from 'axios';

export default {
  namespaced: true,
  state: {
    photos: []
  },
  mutations: {
    UPDATE_PHOTOS(state, photos) {
      state.photos = photos;
    }
  },
  actions: {
    fetchPhotos({ commit, rootState }) {
      const headers = rootState.headers;
      axios
        .get('/api/photos', { headers })
        .then((data) => {
          commit('UPDATE_PHOTOS', data.data);
        })
        .catch((e) => {
          console.error('error' + e);
        });
    }
  },
  getters: {
    getById: (state) => (id) => state.photos.find((item) => item.photoId === id)
  }
};
