using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;



namespace WebApplication1
{
    public class ReportIncident : PageModel
    {
        private readonly ReportIncidentModel _reportIncidentModel;
        
        public ReportIncident()
        {
            _reportIncidentModel = new ReportIncidentModel();
        }
        
        [BindProperty]
        public IncidentContainerObject Incident { get; set; }

        public void OnGet()
        {
            // Initialize any required data
        }
        
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                DateTime incidentDate;
                int casualtiesCount, survivorsCount;

                if (!DateTime.TryParse(Incident.IncidentDate, out incidentDate))
                {
                    ModelState.AddModelError("Incident.IncidentDate", "Invalid date format");
                    return Page();
                }

                if (!int.TryParse(Incident.IncidentCasualitiesCount, out casualtiesCount))
                {
                    ModelState.AddModelError("Incident.IncidentCasualitiesCount", "Invalid number format");
                    return Page();
                }

                if (!int.TryParse(Incident.IncidentSurvivorsCount, out survivorsCount))
                {
                    ModelState.AddModelError("Incident.IncidentSurvivorsCount", "Invalid number format");
                    return Page();
                }

                bool success = _reportIncidentModel.SendReport(
                    Incident.FlightID,
                    Incident.IncidentLocation,
                    incidentDate,
                    casualtiesCount,
                    survivorsCount,
                    Incident.IncidentCause,
                    Incident.IncidentPenalities
                );

                if (success)
                {
                    TempData["Success"] = "Incident report submitted successfully.";
                    return RedirectToPage("/Index");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to report incident.");
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error reporting incident: {ex.Message}");
                return Page();
            }
        }
    }
}
