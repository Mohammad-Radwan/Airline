public class Ticket
{
    public Ticket(string ticketId, string passengerId, string paymentStatus, decimal price, DateTime purchaseDate, string ticketClass, string paymentMethod, decimal discount, Flight flight, BoardingPass boardingPass)
    {
        TicketId = ticketId;
        PassengerId = passengerId;
        PaymentStatus = paymentStatus;
        Price = price;
        PurchaseDate = purchaseDate;
        TicketClass = ticketClass;
        PaymentMethod = paymentMethod;
        Discount = discount;
        Flight = flight;
        BoardingPass = boardingPass;
    }

    public Ticket(Ticket other)
    {
        TicketId = other.TicketId;
        PassengerId = other.PassengerId;
        PaymentStatus = other.PaymentStatus;
        Price = other.Price;
        PurchaseDate = other.PurchaseDate;
        TicketClass = other.TicketClass;
        PaymentMethod = other.PaymentMethod;
        Discount = other.Discount;
        Flight = other.Flight;
        BoardingPass = other.BoardingPass;
    }

    public Ticket()
    {
    }

    public string TicketId { get; set; }
    public string PassengerId { get; set; }
    public string PaymentStatus { get; set; }
    public decimal Price { get; set; }
    public DateTime PurchaseDate { get; set; }
    public string TicketClass { get; set; }
    public string PaymentMethod { get; set; }
    public decimal Discount { get; set; }
    public Flight Flight { get; set; }
    public BoardingPass BoardingPass { get; set; }
}