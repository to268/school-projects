<template>
  <DroitLayout title="La réglementation relative aux cookies">
    <h1>1 - Notre utilisation des cookies</h1>
    <br />
    <p>
      Lors de votre navigation sur le présent site internet (ci-après « Site internet »), des
      cookies (ci-après « Cookies ») de Club Med ou de tiers peuvent être déposés sur votre terminal
      (ordinateur, mobile, tablette), sous réserve des choix que vous avez exprimés et que vous
      pourrez modifier à tout moment.
      <br />
      Ces Cookies sont gérés par le navigateur internet de l'utilisateur.
      <br />
      Pour en savoir plus sur les cookies et leur incidence, nous vous invitons à consulter la
      rubrique « Comment ça marche » du site de la CNIL accessible
      <a href="https://www.cnil.fr/fr/cookies-et-autres-traceurs" target="_blank">ici</a>.
    </p>
    <br />
    <h1>2 - Qu’est ce qu’un cookie</h1>
    <br />
    <p>
      Lors de la consultation du site Internet Club Med (le « Site »), un petit fichier texte appelé
      « cookie » peut être déposé sur votre terminal (ordinateur, tablette, smartphone par ex.),
      grâce à votre logiciel de navigation, sous réserve des choix que vous auriez exprimés
      concernant les cookies et que vous pouvez modifier à tout moment. Ce fichier permet, pendant
      sa durée d’enregistrement, d’identifier votre terminal lors de vos prochaines visites. Ainsi,
      chaque fois que vous vous rendez sur le Site, les données de votre précédente visite peuvent
      être conservées. Il existe deux types de cookies :
    </p>
    <ul>
      <li>
        Les cookies first party, déposés par le Club Med pour les besoins de la navigation et du
        fonctionnement du site.
      </li>
      <li>
        Des cookies third party déposés par des sociétés partenaires tierces afin d’identifier vos
        centres d’intérêt et de vous adresser des offres personnalisées. Ces cookies third party
        sont directement gérés par les sociétés qui les éditent et qui doivent également respecter
        la réglementation sur la protection des données.
      </li>
    </ul>
    <p>
      Seul l’émetteur d’un cookie est susceptible de lire ou de modifier les informations qui y sont
      contenues.
    </p>
    <br />
    <Cookies_3 />
    <br />
    <h1>4 - Durée de conservation des cookies</h1>
    <br />
    <p>
      Vos choix, concernant l’acceptation ou le refus des cookies sur nos services, sont conservés
      pendant 6 mois, au terme desquels vous serez à nouveau sollicité pour exprimer un accord, un
      refus ou un paramétrage des cookies. Les données recueillies par l’intermédiaire des cookies,
      dont celles relatives à la navigation, sont conservées en fonction des finalités pour une
      durée maximum de 25 mois.
    </p>
    <br />
    <Cookies_5 />
  </DroitLayout>
</template>

<script>
import Cookies_3 from '@/components/Cookies/3.vue';
import Cookies_5 from '@/components/Cookies/5.vue';
import DroitLayout from '@/views/Droit/DroitLayout.vue';

export default {
  name: 'Cookies',
  components: {
    Cookies_3,
    Cookies_5,
    DroitLayout
  }
};
</script>

<style>
p {
  margin-left: 20px;
}

ul {
  margin-left: 50px;
}
</style>
