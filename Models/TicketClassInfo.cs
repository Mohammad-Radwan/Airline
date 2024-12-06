public class TicketClassInfo
{
    public string Name { get; set; }
    public decimal PriceMultiplier { get; set; }
    
    public decimal CalculatePrice(decimal basePrice)
    {
        return basePrice * PriceMultiplier;
    }
}