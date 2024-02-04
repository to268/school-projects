/// Router index.js

import { createRouter, createWebHistory } from 'vue-router';
import ResortsView from '@/views/ResortsView.vue';
import ResortLayout from '@/views/Resort/ResortLayout.vue';
import Reservation from '@/views/Reservation.vue';
import Profile from '@/views/Profile.vue';
import SignIn from '@/views/SignIn.vue';
import LogIn from '@/views/LogIn.vue';
import Cookies from '@/views/Droit/Cookies.vue';
import Politique from '@/views/Droit/Politique.vue';
import Legal from '@/views/Droit/Legal.vue';
import NotFound from '@/views/NotFound/NotFound.vue';
import ActiviteResort from '@/views/Resort/ActiviteResort.vue';
import TypeChambre from '@/views/Resort/TypeChambre.vue';
import Restauration from '@/views/Resort/Restauration.vue';
import AvisResort from '@/views/Resort/AvisResort.vue';
import DetailsResort from '@/views/Resort/DetailsResort.vue';
import DomaineSkiable from '@/views/Resort/DomaineSkiable.vue';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'Resorts',
      component: ResortsView
    },
    {
      path: '/resort/:resortId',
      name: 'ResortLayout',
      component: ResortLayout,
      props: true,
      children: [
        {
          path: '',
          name: 'DetailsResort',
          component: DetailsResort
        },
        {
          path: 'activite',
          name: 'ActiviteResort',
          component: ActiviteResort
        },
        {
          path: 'chambres',
          name: 'TypeChambreResort',
          component: TypeChambre
        },
        {
          path: 'restauration',
          name: 'RestaurationResort',
          component: Restauration
        },
        {
          path: 'avis',
          name: 'AvisResort',
          component: AvisResort
        },
        {
          path: 'domaine',
          name: 'DomaineSkiableResort',
          component: DomaineSkiable
        }
      ]
    },
    {
      path: '/reservation/:resortId',
      name: 'Reservation',
      component: Reservation,
      props: true
    },
    {
      path: '/profile',
      name: 'Profile',
      component: Profile
    },
    {
      path: '/signin',
      name: 'SignIn',
      component: SignIn
    },
    {
      path: '/login',
      name: 'LogIn',
      component: LogIn
    },
    {
      path: '/cookies',
      name: 'Cookies',
      component: Cookies
    },
    {
      path: '/politic',
      name: 'Politic',
      component: Politique
    },
    {
      path: '/legal',
      name: 'Legal',
      component: Legal
    },
    {
      path: '/:catchAll(.*)',
      name: 'NotFound',
      component: NotFound
    }
  ]
});

export default router;
