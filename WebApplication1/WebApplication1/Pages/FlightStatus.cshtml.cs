using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Pages
{
    public class FlightStatus : PageModel
    {
        private readonly FlightStatusModel _flightStatusModel;

        [BindProperty]
        public FlightStatusContainerObject FlightStatusVM { get; set; }

        public FlightStatus()
        {
            _flightStatusModel = new FlightStatusModel();
            FlightStatusVM = new FlightStatusContainerObject();
        }

        public void OnGet()
        {
            FlightStatusVM.SearchPerformed = false;
        }

        public IActionResult OnPost()
        {
            try
            {
                var flightDetails = _flightStatusModel.GetFlightDetails(FlightStatusVM.FlightID);

                if (flightDetails != null && flightDetails.Count > 0)
                {
                    // Store the first flight found
                    FlightStatusVM = flightDetails[0];
                }
                else
                {
                    FlightStatusVM.ErrorMessage = "No flight found with this ID.";
                }
            }
            catch (Exception ex)
            {
                FlightStatusVM.ErrorMessage = "An error occurred while retrieving flight details.";
            }

            FlightStatusVM.SearchPerformed = true;
            return Page();
        }
    }
}