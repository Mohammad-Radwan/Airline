public class BookingViewModel
{
    public Flight Flight { get; set; }
    public Route Route { get; set; }
    public List<TicketClassInfo> AvailableClasses { get; set; }
}