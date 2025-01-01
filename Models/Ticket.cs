public class Ticket
{

    public Ticket()
    {
    }

    // Ticket_ID, Passport_No, Class, Pay_Status, Flight_id, Payment_method, seat_id, discount, is_cancelled
    public string Ticket_ID { get; set; }
    
    public string Passport_No { get; set; }

    public string Class { get; set; }

    public string Pay_Status { get; set; }
    
    public string Flight_id { get; set; }
    
    public string Payment_method { get; set; }
    
    public string Seat_id { get; set; }
    
    public decimal Discount { get; set; }
    
    public bool Is_cancelled { get; set; }
}