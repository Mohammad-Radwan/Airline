using Airline.Models;
public class Flight
{
    public string FlightNumber { get; set; }
    public string AircraftModel { get; set; }
    public string DepartureTime { get; set; }
    public string ArrivalTime { get; set; }
    public int Duration { get; set; }
    public string Route { get; set; }
}