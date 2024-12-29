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
        
        public IActionResult OnGetSendReport(string flight_id , string incident_location , DateTime date , int casualities_count , int survivors_count , string incident_cause , string penalties)
        {
            try
            {
                Console.WriteLine($"Flight ID: {flight_id}, Incident Location: {incident_location}, IncidentDate: {date}, Casualities Count: {casualities_count}, Survivors Count: {survivors_count}, Incident Cause: {incident_cause}, Penalties: {penalties}");
                var result = _reportIncidentModel.SendReport(
                    flight_id, incident_location, date, casualities_count, survivors_count, incident_cause, penalties
                );
                return new JsonResult(new { success = result });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send report: {ex.Message}");
                return new JsonResult(new { error = "Failed to send report" }) 
                { 
                    StatusCode = 500 
                };
            }
        }
        
    }
}
