using System.Windows.Controls;
using UrgenceTech.ViewModels;

namespace UrgenceTech.Views
{
    public partial class TableauDeBordView : UserControl
    {
        public TableauDeBordView()
        {
            InitializeComponent();
            DataContext = new TableauDeBordViewModel();
        }
    }
}