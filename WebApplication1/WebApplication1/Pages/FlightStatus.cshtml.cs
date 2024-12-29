using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class FlightStatus : PageModel
{
    private readonly FlightStatusModel _flightStatusModel;
    public void OnGet()
    {
        
    }
    
    public FlightStatus()
    {
        _flightStatusModel = new FlightStatusModel();
    }
    
    
    public IActionResult OnGetGetFlightDetails(string FlightID, DateTimeOffset DepartTime)
    {
        try
        {
            var flightDetails = _flightStatusModel.GetFlightDetails(FlightID, DepartTime)[0];
            
            // Assuming flightDetails contains the necessary information
            var result = new
            {
                success = new
                {
                    flightNumber = flightDetails.FlightID,
                    status = flightDetails.FlightStatus,
                    departureAirport = flightDetails.FlightDepartTime,
                    arrivalAirport = flightDetails.FlightArrivalTime,
                    departureTime = flightDetails.FlightDepartTime.ToString("HH:mm"),
                    arrivalTime = flightDetails.FlightArrivalTime.ToString("HH:mm"),
                    progress = CalculateFlightProgress(flightDetails),
                    // conditions = new[]
                    // {
                    //     new { status = "success", message = "Weather conditions favorable" },
                    //     new { status = "success", message = "All systems operational" },
                    //     new { status = GetFlightConditionStatus(flightDetails), 
                    //           message = GetFlightConditionMessage(flightDetails) }
                    // }
                }
            };
            
            return new JsonResult(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to get flight details: {ex.Message}");
            return new JsonResult(new { error = "Failed to get flight details" }) 
            { 
                StatusCode = 500 
            };
        }
    }

    private int CalculateFlightProgress(FlightStatusContainerObject flight)
    {
        // Add your progress calculation logic here
        return 0; // Placeholder
    }

    private string GetFlightConditionStatus(FlightStatusContainerObject flight)
    {
        // Add your condition status logic here
        return "success"; // Placeholder string GetFlightConditionMessage(FlightStatusContainerObject flight)
    
        // Add your condition message logic here
        return "Flight proceeding as scheduled"; // Placeholder
    }

}