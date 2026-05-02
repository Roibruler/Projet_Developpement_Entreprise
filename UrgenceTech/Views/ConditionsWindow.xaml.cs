using System.Windows;

namespace UrgenceTech.Views
{

    public partial class ConditionsWindow : Window
    {
        public ConditionsWindow()
        {
            InitializeComponent();
        }

        private void FermerBTN_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
