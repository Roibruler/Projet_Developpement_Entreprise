using System;
using System.ComponentModel.DataAnnotations;
using System.Timers;
using UrgenceTech.Models;
using Timer = System.Timers.Timer;

namespace UrgenceTech
{
    public class SessionManager
    {

        private TimeSpan SessionTimeout = TimeSpan.FromMinutes(30); //(test) idle pour juste 1 minute, va être changer
        private Timer inactivityTimer;
        public event Action SessionExpired;
        private bool expired;
        private UserSession _userSession;

        public SessionManager()
        {
            _userSession = new UserSession();
            inactivityTimer = new Timer(1000); // vérification chaque seconde
            inactivityTimer.Elapsed += CheckSessionStatus;
        }

        public UserSession StartSession()
        {
            _userSession.Token = Guid.NewGuid().ToString();
           _userSession.LastActivity = DateTime.Now;
            _userSession.IsActive = true;
            expired = false;
            inactivityTimer.Start();

            return _userSession;
        }

        public void EndSession()
        {
            _userSession.IsActive = false;
            _userSession.Token = null;
            inactivityTimer.Stop();

        }

        public void ResetActivity()
        {
            if (_userSession.IsActive)
            {
                _userSession.LastActivity = DateTime.Now;
            }
        }

        private void CheckSessionStatus(object sender, ElapsedEventArgs e)
        {
            if (_userSession.Token == null || expired) return;

            if (DateTime.Now - _userSession.LastActivity > SessionTimeout)
            {
                expired = true;
                _userSession.IsActive = false;
                _userSession.Token = null;
                inactivityTimer.Stop();
                SessionExpired?.Invoke();
            }
        }
    }
}