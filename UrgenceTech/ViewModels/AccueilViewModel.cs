using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrgenceTech.Models;

namespace UrgenceTech.ViewModels
{
    public class AccueilViewModel
    {
        private SessionManager _sessionManager;
        private UserSession _currentSession;

        public string Statut { get; set; }


        public event Action StatusChanged;
        public event Action SessionExpired;
        public event Action LogoutRequested;


        public AccueilViewModel(SessionManager sessionManager)
        {
            _sessionManager = sessionManager;
            _sessionManager.SessionExpired += OnSessionExpired;

            _currentSession = _sessionManager.StartSession();
            Statut = "Session démarrée";
        }

        public void UserActivity()
        {
            _sessionManager.ResetActivity();
            Statut = "Activité détectée : " + DateTime.Now.ToLongTimeString();
            StatusChanged.Invoke();
        }

        public void Logout()
        {
            _sessionManager.EndSession();
            LogoutRequested.Invoke();
        }

        private void OnSessionExpired()
        {
            SessionExpired?.Invoke();
        }


    }

}