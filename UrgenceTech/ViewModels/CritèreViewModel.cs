using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UrgenceTech.ViewModels
{
    public class CritèreViewModel : INotifyPropertyChanged
    {

        private string _motDePasse;

        public string MotDePasse
        {
            get => _motDePasse;
            set
            {
                _motDePasse = value;
                ValidateMotDePasse();
                OnPropertyChanged();
            }

        }


        public bool MinLength { get; set; }
        public bool Majuscule { get; set; }
        public bool Minuscule { get; set; }
        public bool Nombre { get; set; }
        public bool SpecialCharactère { get; set; }



        public void ValidateMotDePasse()
        {
            string pwd = MotDePasse ?? "";

            MinLength = pwd.Length >= 8;
            Majuscule = Regex.IsMatch(pwd, "[A-Z]");
            Minuscule = Regex.IsMatch(pwd, "[a-z]");
            Nombre = Regex.IsMatch(pwd, "[0-9]");
            SpecialCharactère = Regex.IsMatch(pwd, "[^a-zA-Z0-9]");


            OnPropertyChanged(nameof(MinLength));
            OnPropertyChanged(nameof(Majuscule));
            OnPropertyChanged(nameof(Minuscule));
            OnPropertyChanged(nameof(Nombre));
            OnPropertyChanged(nameof(SpecialCharactère));

        }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
                    => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));


    }


}
