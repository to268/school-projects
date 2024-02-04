using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DeeVizion.MVVM.Model;

namespace DeeVizion.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour AddView.xaml
    /// </summary>
    public partial class AddView : UserControl
    {
        private const String NEW_ITEM = "<nouvelle>";
        private bool isNewMission;
        private bool isNewDivision; 
        private String missionStr;
        private String divisionStr;
        private DateTime dateTimeMission;
        
        public AddView()
        {
            InitializeComponent();
            
            boxMission.Visibility = Visibility.Hidden;
            boxCorps.Visibility = Visibility.Hidden;
            boxDivision.Visibility = Visibility.Hidden;

            foreach (Mission mission in ApplicationData.ListeMissions)
            {
                cbMission.Items.Add(mission.LibelleMission);
            }
            cbMission.Items.Add(NEW_ITEM);

            for (int i = 0; i < ApplicationData.ListeCorpsArmees.Count; i++)
            {
                for (int j = 0; j < ApplicationData.ListeDivisions.Count; j++)
                {
                    String line = ApplicationData.ListeCorpsArmees[i].LibelleCorps + "." +
                                  ApplicationData.ListeDivisions[j].LibelleDivision;
                    cbDivision.Items.Add(line);
                }
            }
            cbDivision.Items.Add(NEW_ITEM);
            
            isNewMission = false;
            isNewDivision = false;
        }

        private void butAjouter_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckFields()) return;

            CorpsArmee corps = ApplicationData.ListeCorpsArmees.Find(x => x.LibelleCorps == divisionStr.Split('.')[0]);
            if (corps == null)
            {
                corps = new CorpsArmee(ApplicationData.ListeCorpsArmees.Count + 1,
                                        divisionStr.Split('.')[0]);
                corps.Create();
            }
            
            
            Division division = ApplicationData.ListeDivisions.Find(x => x.LibelleDivision == divisionStr.Split('.')[1]);;
            if (division == null)
            {
                division = new Division(ApplicationData.ListeDivisions.Count + 1,
                                        ApplicationData.ListeCorpsArmees.Count + 1,
                                        divisionStr.Split('.')[1]);
                division.Create();
            }

            Mission mission = ApplicationData.ListeMissions.Find(x => x.LibelleMission == missionStr);
            if (mission == null)
            {
                mission = new Mission(ApplicationData.ListeMissions.Count + 1, missionStr);
                mission.Create();
            }

            DateMission dateMission = ApplicationData.ListeDateMissions.Find(x => x.LaDateMission == dateTimeMission);
            if (dateMission == null)
            {
                dateMission = new DateMission(ApplicationData.ListeDateMissions.Count + 1, dateTimeMission);
                dateMission.Create();
            }

            Affectation affectation = new Affectation(division, mission, dateMission, txtCommentaire.Text);
            affectation.Create();
            
            ApplicationData.ListeCorpsArmees.Add(corps);
            ApplicationData.ListeDivisions.Add(division);
            ApplicationData.ListeMissions.Add(mission);
            ApplicationData.ListeDateMissions.Add(dateMission);
            ApplicationData.ListeAffectation.Add(affectation);
            
            MessageBox.Show("L'affectation a bien été enregistrée", 
                            "Ajout affectation", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool CheckFields()
        {
            if (cbMission.Text == NEW_ITEM)
            {
                if (String.IsNullOrWhiteSpace(boxMission.Text) || boxMission.Text == "Nom de la mission")
                {
                    MessageBox.Show("Le nom de la nouvelle division doit etre renseigné",
                        "Erreur d'ajout", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                missionStr = boxMission.Text;
            }
            else
            {
                missionStr = cbMission.Text;
            }

            if (cbDivision.Text == NEW_ITEM)
            {
                if (String.IsNullOrWhiteSpace(boxCorps.Text) || String.IsNullOrWhiteSpace(boxDivision.Text) ||
                    boxCorps.Text == "Non du corps" || boxDivision.Text == "Nom de la division")
                {
                    MessageBox.Show("Le nom du corps ou de la nouvelle division doit etre renseigné",
                        "Erreur d'ajout", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                divisionStr = boxCorps.Text + "." + boxDivision.Text;
            }
            else
            {
                divisionStr = cbDivision.Text;
            }

            if (datePicker.SelectedDate == null)
            {
                MessageBox.Show("La date de la mission doit etre renseigné",
                    "Erreur d'ajout", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            dateTimeMission = (DateTime)datePicker.SelectedDate;
            return true;
        }

        private void cbMission_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String text = (((sender as ComboBox)!).SelectedItem.ToString());
            
            if (text == NEW_ITEM)
            {
                boxMission.Visibility = Visibility.Visible;
                isNewMission = true;
            }
            else if(isNewMission)
            {
                boxMission.Visibility = Visibility.Hidden;
                isNewMission = false;
            }
        }

        private void cbDivision_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String text = (((sender as ComboBox)!).SelectedItem.ToString());
            
            if (text == NEW_ITEM)
            {
                boxCorps.Visibility = Visibility.Visible;
                boxDivision.Visibility = Visibility.Visible;
                isNewDivision = true;
            }
            else if(isNewDivision)
            {
                boxCorps.Visibility = Visibility.Hidden;
                boxDivision.Visibility = Visibility.Hidden;
                isNewDivision = false;
            }
        }
    }
}
