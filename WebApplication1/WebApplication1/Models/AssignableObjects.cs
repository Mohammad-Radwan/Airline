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
    public string Airline { get; set; }
    public string Aircraft { get; set; }
    public string RouteID { get; set; }
}