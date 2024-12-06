using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

public class FlightController : Controller
{
    public IActionResult Search()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SearchResults(DateTime searchDate)
    {
        // Sample data - in a real application, this would come from a database
        var flights = new List<Flight>
        {
            new Flight
            {
                FlightNumber = "FL789",
                AircraftModel = "Boeing 787",
                DepartureTime = searchDate.Date.AddHours(8),
                ArrivalTime = searchDate.Date.AddHours(16),
                Duration = TimeSpan.FromHours(8),
                Route = new Route
                {
                    DepartureAirport = "JFK",
                    ArrivalAirport = "LHR",
                    Distance = 5541.00,
                    BasePrice = 399.99m
                }
            },
            new Flight
            {
                FlightNumber = "FL456",
                AircraftModel = "Airbus A380",
                DepartureTime = searchDate.Date.AddHours(14),
                ArrivalTime = searchDate.Date.AddHours(22),
                Duration = TimeSpan.FromHours(8),
                Route = new Route
                {
                    DepartureAirport = "JFK",
                    ArrivalAirport = "LHR",
                    Distance = 5541.00,
                    BasePrice = 449.99m
                }
            }
        };

        return View(flights);
    }

    public IActionResult Book(string flightNumber, DateTime departureTime)
    {
        // In a real application, you would fetch the flight from a database
        var flight = new Flight
        {
            FlightNumber = flightNumber,
            AircraftModel = "Boeing 787",
            DepartureTime = departureTime,
            ArrivalTime = departureTime.AddHours(8),
            Duration = TimeSpan.FromHours(8),
            Route = new Route
            {
                DepartureAirport = "JFK",
                ArrivalAirport = "LHR",
                Distance = 5541.00,
                BasePrice = 399.99m
            }
        };

        var bookingModel = new BookingViewModel
        {
            Flight = flight,
            AvailableClasses = new List<TicketClassInfo>
            {
                new TicketClassInfo { Name = "Economy", PriceMultiplier = 1.0m },
                new TicketClassInfo { Name = "First", PriceMultiplier = 1.5m },
                new TicketClassInfo { Name = "Business", PriceMultiplier = 2.0m }
            }
        };

        return View(bookingModel);
    }
    
    public IActionResult ConfirmBooking(string flightNumber, string ticketClass)
    {
        // Your confirmation logic here...

        TempData["BookingMessage"] = "Your request is pending.";
        return RedirectToAction("PassengerProfile", "Profile"); // Redirect to passenger profile
    }
}