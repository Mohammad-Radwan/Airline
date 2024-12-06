using Microsoft.AspNetCore.Mvc;

public class ProfileController : Controller
{
    public IActionResult PassengerProfile()
    {
        // This is a sample data. In real application, you would fetch this from your database
        var profile = new PassengerProfile
        {
            PassengerId = "P123456",
            Name = "Jurica Koletić",
            Email = "Jurica.Koletić@email.com",
            PhoneNumber = "+1-234-567-8900",
            DateOfBirth = new DateTime(1990, 1, 1),
            PassportNumber = "AB1234567",
            Nationality = "Russia",
            Address = "123 Aviation Street, Skytown, ST 12345",
            MileagePoints = 50000,
            MembershipTier = "Gold"
        };

        return View(profile);
    }

    public IActionResult TicketRecord()
    {
        // Sample data - in a real application, this would come from a database
        var tickets = new List<Ticket>
        {
            new Ticket
            {
                TicketId = "T123456",
                PassengerId = "P123456",
                PaymentStatus = "Paid",
                Price = 499.99m,
                PurchaseDate = DateTime.Now.AddDays(-30),
                TicketClass = "Business",
                PaymentMethod = "Credit Card",
                Discount = 50.00m,
                Flight = new Flight
                {
                    FlightNumber = "FL789",
                    AircraftModel = "Boeing 787",
                    DepartureTime = DateTime.Now.AddDays(15),
                    ArrivalTime = DateTime.Now.AddDays(15).AddHours(8),
                    Duration = TimeSpan.FromHours(8),
                    Route = new Route()
                    {
                        DepartureAirport = "JFK",   
                        ArrivalAirport = "CAI",
                        Distance = 5541.00,
                        BasePrice = 399.99m
                    }
                },
                BoardingPass = new BoardingPass
                {
                    TicketId = "T123456",
                    SeatNumber = "12A",
                    CheckInGate = "B12",
                    CheckOutGate = "C45"
                }
            },
            
            new Ticket
            {
                TicketId = "T224336",
                PassengerId = "P123456",
                PaymentStatus = "Paid",
                Price = 149.99m,
                PurchaseDate = DateTime.Now.AddDays(-50),
                TicketClass = "Economy",
                PaymentMethod = "Credit Card",
                Discount = 50.00m,
                Flight = new Flight
                {
                    FlightNumber = "FL777",
                    AircraftModel = "Boeing 747",
                    DepartureTime = DateTime.Now.AddDays(-15),
                    ArrivalTime = DateTime.Now.AddDays(-15).AddHours(8),
                    Duration = TimeSpan.FromHours(8),
                    Route = new Route()
                    {
                    DepartureAirport = "CAI",   
                    ArrivalAirport = "SAE",
                    Distance = 6345.00,
                    BasePrice = 99.99m
                }
                },
                BoardingPass = new BoardingPass
                {
                    TicketId = "T224336",
                    SeatNumber = "144c",
                    CheckInGate = "E36",
                    CheckOutGate = "B05"
                }
            },
            
            
            // Add more sample tickets as needed
        };

        return View(tickets);
    }

    public IActionResult TicketDetails(string id)
    {
        // Sample data - in a real application, this would come from a database
        var ticket = new Ticket
        {
            TicketId = id,
            PassengerId = "P123456",
            PaymentStatus = "Paid",
            Price = 499.99m,
            PurchaseDate = DateTime.Now.AddDays(-30),
            TicketClass = "Business",
            PaymentMethod = "Credit Card",
            Discount = 50.00m,
            Flight = new Flight
            {
                FlightNumber = "FL789",
                AircraftModel = "Boeing 787",
                DepartureTime = DateTime.Now.AddDays(15),
                ArrivalTime = DateTime.Now.AddDays(15).AddHours(8),
                Duration = TimeSpan.FromHours(8),
                Route = new Route()
                {
                    DepartureAirport = "JFK",   
                    ArrivalAirport = "CAI",
                    Distance = 5541.00,
                    BasePrice = 399.99m
                }
            },
            BoardingPass = new BoardingPass
            {
                TicketId = id,
                SeatNumber = "12A",
                CheckInGate = "B12",
                CheckOutGate = "C45"
            }
        };

        return View(ticket);
    }

    public IActionResult RefundRequest(string id)
    {
        var refundRequest = new RefundRequest
        {
            TicketId = id,
            PassengerId = "P123456", // In a real app, this would come from the logged-in user
            RequestDate = DateTime.Now
        };

        return View(refundRequest);
    }

    [HttpPost]
    public IActionResult SubmitRefund(RefundRequest request)
    {
        // In a real application, save the refund request to the database
        
        // Add a success message to TempData
        TempData["SuccessMessage"] = "Your refund request has been recorded. We will inform you of any updates.";
        
        // Redirect back to the ticket details page
        return RedirectToAction("TicketDetails", new { id = request.TicketId });
    }

    
    public IActionResult BaggageRecord()
    {
        // Sample data - in a real application, this would come from a database
        var baggageList = new List<Baggage>
        {
            new Baggage
            {
                BaggageId = "B123456",
                PassengerId = "P123456",
                BaggageTag = "TAG001",
                CargoId = "CARGO789",
                Weight = 23.5,
                Type = "Checked Baggage",
                FlightNumber = "FL789",
                CheckInDate = DateTime.Now.AddDays(-1),
                Status = "In Transit"
            },
            new Baggage
            {
                BaggageId = "B123457",
                PassengerId = "P123456",
                BaggageTag = "TAG002",
                CargoId = "CARGO790",
                Weight = 18.2,
                Type = "Checked Baggage",
                FlightNumber = "FL456",
                CheckInDate = DateTime.Now.AddDays(-30),
                Status = "Delivered"
            }
        };

        return View(baggageList);
    }
}