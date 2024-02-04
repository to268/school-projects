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
    /// Logique d'interaction pour ModifyView.xaml
    /// </summary>
    public partial class ModifyView : UserControl
    {
        private List<Affectation> affectations;

        public ModifyView()
        {
            InitializeComponent();
            affectations = ApplicationData.ListeAffectation;
            dg.ItemsSource = affectations;
        }
    }
}
