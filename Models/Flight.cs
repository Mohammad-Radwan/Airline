public class Flight
{
    public string FlightNumber { get; set; }
    public string AircraftModel { get; set; }
    public DateTime DepartureTime { get; set; }
    public DateTime ArrivalTime { get; set; }
    public TimeSpan Duration { get; set; }
    public Route Route { get; set; }
}