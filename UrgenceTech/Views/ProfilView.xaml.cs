using System.Windows.Controls;
using UrgenceTech.Models;
using UrgenceTech.ViewModels;

namespace UrgenceTech.Views
{
    public partial class ProfilView : UserControl
    {
        public ProfilView(Utilisateur utilisateur)
        {
            InitializeComponent();
            DataContext = new ProfilViewModel(utilisateur);
        }
    }
}