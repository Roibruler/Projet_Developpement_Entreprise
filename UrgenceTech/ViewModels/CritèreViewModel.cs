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

        //vérifie si le mot de passe est bon
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
        private bool _minLength;
        //vérifie si le min mot est bon
        public bool MinLength 
        { 
            get => _minLength;
            set
            {
                if (_minLength != value)
                {
                    _minLength = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsPasswordValid));
                }
            }
        }

        private bool _majuscule;
        //vérifie si le max mot est bon
        public bool Majuscule 
        { 
            get => _majuscule;
            set
            {
                if (_majuscule != value)
                {
                    _majuscule = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsPasswordValid));
                }
            }
        }

        private bool _minuscule;
        //vérifie s'il y a minuscule est bon

        public bool Minuscule 
        { 
            get => _minuscule;
            set
            {
                if (_minuscule != value)
                {
                    _minuscule = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsPasswordValid));
                }
            }
        }
        private bool _nombre;
        //vérifie s'il un nombre est bon
        public bool Nombre 
        { 
            get => _nombre;
            set
            {
                if (_nombre != value)
                {
                    _nombre = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsPasswordValid));
                }
            }
        }
        private bool _specialCharactère;
        //vérifie s'il une lettre spécial est bon
        public bool SpecialCharactère 
        { 
            get => _specialCharactère;
            set
            {
                if (_specialCharactère != value)
                {
                    _specialCharactère = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsPasswordValid));
                }
            }
        }
        //vérifie si le mot de passe est valide
        public bool IsPasswordValid => MinLength && Majuscule && Minuscule && Nombre && SpecialCharactère;
        public void ValidateMotDePasse()
        {
            string pwd = MotDePasse ?? "";

            //regade si les valeurs respecte les constraints
            MinLength = pwd.Length >= 8;
            Majuscule = Regex.IsMatch(pwd, "[A-Z]");
            Minuscule = Regex.IsMatch(pwd, "[a-z]");
            Nombre = Regex.IsMatch(pwd, "[0-9]");
            SpecialCharactère = Regex.IsMatch(pwd, "[^a-zA-Z0-9]");
        }

        //updates
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
                    => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));


    }


}
