using System.Windows;
using System.Windows.Controls;
using UrgenceTech.ViewModels;

namespace UrgenceTech.Views
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            DataContext = new LoginViewModels();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModels vm && sender is PasswordBox pb)
                vm.MotDePasse = pb.Password;
        }
    }
}