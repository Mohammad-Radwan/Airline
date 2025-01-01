namespace Airline.Models;

public class TicketDetails
{
    public Ticket ticket { get; set; }
    public Flight flight { get; set; }
    public Route route { get; set; }
}