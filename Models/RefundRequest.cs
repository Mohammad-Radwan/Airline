public class RefundRequest
{
    // Ticket_ID, Amount, date_, Status_, description_
    public string Ticket_ID { get; set; }
    
    public int Amount { get; set; }
    
    public DateTime date_ { get; set; }
    
    public string Status_ { get; set; } = "Pending";
    
    public string description_ { get; set; }

}