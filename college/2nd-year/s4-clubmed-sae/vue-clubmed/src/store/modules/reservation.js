import axios from 'axios';

export default {
  namespaced: true,
  state: {
    reservations: [],
    currentReservation: {
      transportId: 1,
      clientId: undefined,
      resortId: undefined,
      prix: 0,
      confirmation: false,
      validation: false,
      dateDebut: undefined,
      dateFin: undefined,
      lesActiviteCartes: [],
      lesActiviteEnfantCartes: [],
      lesAutreParticipants: []
    },
    lesTypeChambres: [],
    isResaFailed: false
  },
  mutations: {
    UPDATE_RESERVATIONS(state, reservations) {
      state.reservations.push(reservations);
    },
    RESA_FAILED(state) {
      state.isResaFailed = true;
    },
    RESA_SUCCESS(state) {
      state.isResaFailed = false;
    }
  },
  actions: {
    async reserver({ commit, state, rootState }) {
      let resa = state.currentReservation;
      resa.lesActiviteEnfantCartes.foreach((item) => {
        resa.prix += item.prix;
      });
      resa.lesActiviteCartes.foreach((item) => {
        resa.prix += item.prix;
      });

      let verif =
        resa.clientId &&
        resa.resortId &&
        resa.prix &&
        resa.dateDebut &&
        resa.dateFin &&
        state.lesTypesChambres.length === 1;
      if (verif) {
        let resa = await axios
          .post('/api/reservation/', resa, { headers: rootState.headers })
          .catch((e) => {
            console.log('erreur reservation');
            console.log(e);
          });

        if (state.lesParticipants.length !== 0) {
          await axios.post('/api/autreparticipant/', state.lesParticipants, {
            headers: rootState.headers
          });
        }

        if (state.lesParticipants.length !== 0) {
          await axios.post('/api/autreparticipant/', state.lesParticipants, {
            headers: rootState.headers
          });
        }
      } else {
        commit('RESA_FAILED');
      }
    },
    fetchReservations({ commit, rootState }, obj) {
      if (obj) {
        const headers = rootState.headers;
        for (let i of obj) {
          axios
            .get('/api/reservation/GetById/' + i.reservationId, { headers })
            .then((data) => {
              commit('UPDATE_RESERVATIONS', data.data);
            })
            .catch((e) => console.log(e));
        }
      }
    }
  },
  getters: {
    getById: (state) => (id) =>
      state.typeActivites.find((item) => item.typeActiviteId === id)
  }
};
