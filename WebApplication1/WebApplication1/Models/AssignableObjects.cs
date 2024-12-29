namespace WebApplication1.Models;

public class AirportContainerObject
{
    public string AirportName { get; set; }
}

public class FlightScheduleContainerObject
{
    public string FlightNumber { get; set; }
    public string DepartureAirport { get; set; }
    public string ArrivalAirport { get; set; }
    public string DepartureTime { get; set; }
    public string ArrivalTime { get; set; }
    public string Aircraft { get; set; }
    public string RouteID { get; set; }
}


public class RouteContainerObject
{
    public string RouteID { get; set; }
    public string DepartureAirport { get; set; }
    public string ArrivalAirport { get; set; }
    public string Distance { get; set; }
    public string Duration { get; set; }
    public string BasePrice { get; set; }
}


public class AirCraftContainerObject()
{
    public string AircraftID { get; set; }
    public string Model { get; set; }
    public string Manufacturer { get; set; }
    public string Capacity { get; set; }
    public string JoinDate { get; set; }
    public string Speed { get; set; }

}


public class IncidentContainerObject()
{
    public string FlightID { get; set; }
    public string IncidentLocation { get; set; }
    
    public DateTime IncidentDateTime { get; set; }
    public int IncidentCasualitiesCount { get; set; }
    public int IncidentSurvivorsCount { get; set; }
    public string IncidentCause { get; set; }
    public string IncidentPenalities { get; set; }
    
}