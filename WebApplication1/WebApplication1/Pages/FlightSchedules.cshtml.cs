using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using System.Collections.Generic;

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
        public List<object> FlightSchedulesList { get; set; }

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

        public IActionResult OnGetFlightSchedules(string fromAirport, string toAirport, DateTimeOffset date)
        {
            try
            {
                var schedules = _flightSchedulesModel.GetFlightSchedules(fromAirport, toAirport, date);
                return new JsonResult(schedules);
            }
            catch
            {
                return new JsonResult(new { error = "Failed to fetch flight schedules" });
            }
        }
    }
}
