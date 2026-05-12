using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrgenceTech.Models;

namespace UrgenceTech.ViewModels
{
    public partial class SessionManagerViewModel : ObservableObject
    {
        [ObservableProperty]
        private SessionManager _sessionManager;
        [ObservableProperty]
        private UserSession _currentSession;
        [ObservableProperty]
        public string statut;


        public event Action StatusChanged;
        public event Action SessionExpired;
        public event Action LogoutRequested;


        public SessionManagerViewModel(SessionManager sessionManager)
        {
            _sessionManager = sessionManager;
            _sessionManager.SessionExpired += OnSessionExpired;

            _currentSession = _sessionManager.StartSession();
            statut = "Session démarrée";
        }

        public void UserActivity()
        {
            _sessionManager.ResetActivity();
            statut = "Activité détectée : " + DateTime.Now.ToLongTimeString();
            StatusChanged?.Invoke();
        }

        public void Logout()
        {
            _sessionManager?.EndSession();
            LogoutRequested?.Invoke();
        }

        private void OnSessionExpired()
        {
            SessionExpired?.Invoke();
        }


    }

}