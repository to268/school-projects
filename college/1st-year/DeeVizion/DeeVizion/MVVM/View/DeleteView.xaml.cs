using DeeVizion.MVVM.Model;
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

namespace DeeVizion.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour DeleteView.xaml
    /// </summary>
    public partial class DeleteView : UserControl
    {
        private List<Affectation> affectations;

        public DeleteView()
        {
            InitializeComponent();
            affectations = ApplicationData.ListeAffectation;
            dg.ItemsSource = affectations;
        }

        private void butSupprimer_Click(object sender, RoutedEventArgs e)
        {
            ((Affectation)dg.SelectedItem).Delete();
            ApplicationData.ListeAffectation.Remove((Affectation)dg.SelectedItem);

            dg.ItemsSource = null;
            dg.ItemsSource = affectations;
        }
    }
}
