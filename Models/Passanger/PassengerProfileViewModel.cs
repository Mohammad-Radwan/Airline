namespace Airline.Models;

public class PassengerProfileViewModel
{
    public string ProfilePicture { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Nationality { get; set; }
    public string PassportNumber { get; set; }
    public string FrequentFlyerNumber { get; set; }
    
    // Loyalty Program Properties
    public string LoyaltyTier { get; set; }
    public int LoyaltyProgress { get; set; }
    public int PointsToNextTier { get; set; }
    public int TotalMiles { get; set; }
    public int AvailablePoints { get; set; }
}