namespace Airline.Models;

public class SessionID
{
    private static SessionID _instance;
    private static readonly object _lock = new object();

    // Variable to store
    public string passengerID { get; set; }

    private SessionID() { }

    public static SessionID Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new SessionID();
                }
                return _instance;
            }
        }
    }
}
