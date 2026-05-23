using System.Windows.Controls;
using UrgenceTech.ViewModels;

namespace UrgenceTech.Views
{
    public partial class UrgenceView : UserControl
    {
        private readonly UrgenceViewModel _vm;

        public UrgenceView()
        {
            InitializeComponent();
            _vm = new UrgenceViewModel();
            DataContext = _vm;
        }

        private void OuvrirCreerUrgence_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var fenetre = new CreerUrgenceView();
            fenetre.ShowDialog();
            _vm.ChargerUrgencesEnCoursCommand.Execute(null);
        }
    }
}