using Microsoft.AspNetCore.Mvc;

namespace Airline.Controllers
{
    public class Flight_Crew_MemberController : Controller
    {
        public IActionResult aircraft_status()
        {
            return View(); // Returns the 'aircraft.cshtml' page
        }
        
        public IActionResult report_incident()
        {
            return View(); 
        }
        public IActionResult routes()
        {
            return View(); 
        }
        public IActionResult flight_schedules()
        {
            return View(); 
        }
        public IActionResult seat_selection()
        {
            return View(); 
        }
    }

}




