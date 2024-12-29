using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using System.Collections.Generic;
using Microsoft.Identity.Client.Region;

namespace WebApplication1.Pages
{
    public class FlightSchedules : PageModel
    {
        private readonly FlightSchedulesModel _flightSchedulesModel;

        public FlightSchedules()
        {
            _flightSchedulesModel = new FlightSchedulesModel();
        }

        [BindProperty]
        public List<AirportContainerObject> Airports { get; set; }

        [BindProperty]
        public List<FlightScheduleContainerObject> Schedules { get; set; }

        public void OnGet()
        {
            try
            {
                Airports = _flightSchedulesModel.GetAirports();
                // return Page();
            }
            catch
            {
                ViewData["Error"] = "Failed to load airports data.";
                // return Page();
            }
        }

        public IActionResult OnGetFlightSchedules(string fromAirport, string toAirport, DateOnly flightDate, TimeOnly flightTime, TimeSpan utcOffset)
        {
            try
            {
                Console.WriteLine($"From Airport: {fromAirport}, To Airport: {toAirport}, Flight Date: {flightDate}, Flight Time: {flightTime}, UTC Offset: {utcOffset}");
                 Schedules = _flightSchedulesModel.GetFlightSchedules(
                    fromAirport, toAirport,
                    new DateTimeOffset(flightDate.Year, flightDate.Month, flightDate.Day, 
                        flightTime.Hour, flightTime.Minute, flightTime.Second, utcOffset)
                );
                return new JsonResult(Schedules);            
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to fetch flight schedules: {ex.Message}");
                return new JsonResult(new { error = "Failed to fetch flight schedules" }) 
                { 
                    StatusCode = 500 
                };
            }
        }

    }
}
