public class RefundRequest
{
    public string TicketId { get; set; }
    public string PassengerId { get; set; }
    public string Complaint { get; set; }
    public DateTime RequestDate { get; set; }
    public string Status { get; set; } = "Pending";
}