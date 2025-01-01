public class Route
{
    
    // ro_id, start_airport, end_airport, distance, duration_in_hours, base_price
    
    public string ro_id { get; set; }
    
    public string start_airport { get; set; }
    
    public string end_airport { get; set; }
    
    public double distance { get; set; }
    
    public double duration_in_hours { get; set; }
    
    public decimal base_price { get; set; }
}