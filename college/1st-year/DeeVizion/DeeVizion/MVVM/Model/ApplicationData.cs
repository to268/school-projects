using System;
using System.Collections.Generic;

namespace DeeVizion.MVVM.Model
{
    public class ApplicationData
    {
        private static List<Mission> listeMissions;

        /// <summary>
        /// Liste de missions
        /// </summary>
        public static List<Mission> ListeMissions
        {
            get { return listeMissions; }
            set { listeMissions = value; }
        }

        private static List<DateMission> listeDateMissions;

        /// <summary>
        /// Liste des dates de missions
        /// </summary>
        public static List<DateMission> ListeDateMissions
        {
            get { return listeDateMissions; }
            set { listeDateMissions = value; }
        }

        private static List<Division> listeDivisions;

        /// <summary>
        /// Liste des divisions
        /// </summary>
        public static List<Division> ListeDivisions
        {
            get { return listeDivisions; }
            set { listeDivisions = value; }
        }

        private static List<CorpsArmee> listeCorpsArmees;

        /// <summary>
        /// Liste des corps d'armée
        /// </summary>
        public static List<CorpsArmee> ListeCorpsArmees
        {
            get { return listeCorpsArmees; }
            set { listeCorpsArmees = value; }
        }

        private static List<Affectation> listeAffectation;

        /// <summary>
        /// Liste des affectations
        /// </summary>
        public static List<Affectation> ListeAffectation
        {
            get { return listeAffectation; }
            set { listeAffectation = value; }
        }

        private static ResponsableHumanitaire responsable;

        /// <summary>
        /// La definition du responsable humanitaire
        /// </summary>
        public static ResponsableHumanitaire Responsable
        {
            get { return responsable; }
            set { responsable = value; }
        }

        /// <summary>
        /// Chargement des données necessaires à l'application depuis la base de données
        /// </summary>
        public static void LoadApplicationData()
        {
            Mission mission = new Mission();
            DateMission date = new DateMission();
            Division division = new Division();
            CorpsArmee corpsArmee = new CorpsArmee();

            ListeMissions = mission.FindAll();
            ListeDateMissions = date.FindAll();
            ListeDivisions = division.FindAll();
            ListeCorpsArmees = corpsArmee.FindAll();

            Affectation affectation = new Affectation(ListeDivisions, ListeMissions, ListeDateMissions);
            ListeAffectation = affectation.FindAll();

            ResponsableHumanitaire responsableHumanitaire = new ResponsableHumanitaire(listeAffectation);
            responsableHumanitaire.Nom = "Dupond";
            responsableHumanitaire.Prenom = "Gerard";
            responsableHumanitaire.DateNaissance = DateTime.Today.Subtract(new TimeSpan(18*365, 0, 0, 0));
            responsableHumanitaire.Affectations = ListeAffectation;
        }
    }
}
