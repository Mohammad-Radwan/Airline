public class Flight
{
    
    // fid, aircraft_id, depart_time, status_, route_id, arrival_time, duration
    
    public string fid { get; set; }
    
    public string aircraft_id { get; set; }
    
    public DateTime depart_time { get; set; }
    
    public string status_ { get; set; }
    
    public string route_id { get; set; }
    
    public DateTime arrival_time { get; set; }
    
    public TimeSpan duration { get; set; }
}