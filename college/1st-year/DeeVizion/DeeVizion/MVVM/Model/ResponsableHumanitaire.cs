using System;
using System.Collections.Generic;

namespace DeeVizion.MVVM.Model
{
    public class ResponsableHumanitaire
    {
        private String nom;

        public String Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        private String prenom;

        public String Prenom
        {
            get { return prenom; }
            set { prenom = value; }
        }

        private DateTime dateNaissance;

        public DateTime DateNaissance
        {
            get { return dateNaissance; }
            set { dateNaissance = value; }
        }

        private List<Affectation> affectations;

        public List<Affectation> Affectations
        {
            get { return affectations; }
            set { affectations = value; }
        }

        public ResponsableHumanitaire(List<Affectation> affectations)
        {
            Nom = "";
            Prenom = "";
            DateNaissance = DateTime.Today;
            Affectations = affectations;
        }
    }
}
