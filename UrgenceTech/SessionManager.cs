using System;
using System.Timers;
using Timer = System.Timers.Timer;

public static class SessionManager
{
    public static string Token { get; private set; }
    public static bool IsAuthenticated => Token != null;

    private static Timer inactivityTimer;
    private static TimeSpan SessionTimeout = TimeSpan.FromMinutes(1); //(test) idle pour juste 1 minute, va être changer
    private static DateTime lastActivity;

    public static event Action SessionExpired;

    static SessionManager()
    {
        inactivityTimer = new Timer(1000); // vérification chaque seconde
        inactivityTimer.Elapsed += CheckSessionStatus;
        inactivityTimer.Start();
    }

    public static void StartSession()
    {
        Token = Guid.NewGuid().ToString();
        ResetActivity();
    }

    public static void EndSession()
    {
        Token = null;
    }

    public static void ResetActivity()
    {
        lastActivity = DateTime.Now;
    }

    private static void CheckSessionStatus(object sender, ElapsedEventArgs e)
    {
        if (Token == null) return;

        if (DateTime.Now - lastActivity > SessionTimeout)
        {
            Token = null;
            SessionExpired?.Invoke();
        }
    }
}