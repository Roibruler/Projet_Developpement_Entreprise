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
using UrgenceTech.Data;
using UrgenceTech.ViewModels;
using static System.Collections.Specialized.BitVector32;

namespace UrgenceTech.Views
{
    /// <summary>
    /// Interaction logic for VoirUtilisateur.xaml
    /// </summary>
    internal partial class VoirUtilisateurView : UserControl
    {


        public VoirUtilisateurView()
        {
            InitializeComponent();

            var context = new AppDbContext();
            DataContext = new VoirUtilisateurViewModel(context);

        }

   

    }

}
