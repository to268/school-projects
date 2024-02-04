using DeeVizion.Core;
using System;


namespace DeeVizion.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        /// <summary>
        /// Initialisation d'un objet ConsultViewModel
        /// </summary>
        public ConsultViewModel ConsultVM { get; set; }

        /// <summary>
        /// Initialisation de la commande de redirection vers la vue ConsultView
        /// </summary>
        public RelayCommand ConsultViewCommand { get; set; }

        /// <summary>
        /// Initialisation d'un objet AddViewModel
        /// </summary> 
        public AddViewModel AddVM { get; set; }

        /// <summary>
        /// Initialisation de la commande de redirection vers la vue AddView
        /// </summary>
        public RelayCommand AddViewCommand { get; set; }

        /// <summary>
        /// Initialisation d'un objet DeleteViewModel
        /// </summary>
        public DeleteViewModel DeleteVM { get; set; }

        /// <summary>
        /// Initialisation de la commande de redirection vers la vue DeleteView
        /// </summary>
        public RelayCommand DeleteViewCommand { get; set; }

        /// <summary>
        /// Initialisation d'un objet ModifyViewModel
        /// </summary>
        public ModifyViewModel ModifyVM { get; set; }

        /// <summary>
        /// Initialisation de la commande de redirection vers la vue ModifyView
        /// </summary>
        public RelayCommand ModifyViewCommand { get; set; }

        private object _curentView;

        /// <summary>
        /// Initialisation d'un objet CurrentView lié à l'objet XAML ContentControl
        /// </summary>
        public object CurrentView
        {
            get { return _curentView; }
            set
            {
                _curentView = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Constructeur permettant de changer de vue via les command
        /// </summary>
        public MainViewModel()
        {
            ConsultVM = new ConsultViewModel();
            AddVM = new AddViewModel();
            DeleteVM = new DeleteViewModel();
            ModifyVM = new ModifyViewModel();

            CurrentView = ConsultVM;

            ConsultViewCommand = new RelayCommand(o => { CurrentView = ConsultVM; });

            AddViewCommand = new RelayCommand(o => { CurrentView = AddVM; });

            DeleteViewCommand = new RelayCommand(o => { CurrentView = DeleteVM; });

            ModifyViewCommand = new RelayCommand(o => { CurrentView = ModifyVM; });
        }    
    }
}

