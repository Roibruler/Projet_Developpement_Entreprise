using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UrgenceTech
{
    // <summary>
    // Logique d'interaction pour MainWindow.xaml
    // </summary>
    public partial class SessionWindow : Window
    {
        public SessionWindow()
        {
            InitializeComponent();

            SessionManager.SessionExpired += OnSessionExpired;
            this.MouseMove += ActivityDetected;
            this.KeyDown += ActivityDetected;

        }

        //prolonge la session aka continue la session
        private void ActivityDetected(object sender, EventArgs e)
        {
            SessionManager.ResetActivity();
            txtStatus.Text = "Session recommacé depuis : " + DateTime.Now.ToLongTimeString() + "\nfaut attendre 1 minute (test) sans rien faire";
        }


        private void OnSessionExpired()
        {

            //créee la page de login
            Dispatcher.Invoke(async () =>
            {
                await Task.Delay(2000);
                MessageBox.Show("Votre session a expiré.");
                new MainWindow().Show();
                this.Close();
                await Task.Delay(1000);
            });
        }

        //boutton disconnect
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            SessionManager.EndSession();
            new MainWindow().Show();
            this.Close();
        }

    }

}
