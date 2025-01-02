using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Airline.Models;

namespace Airline.Pages;

public class Routes : PageModel
{
    private readonly RoutesModel _routesModel;

    public Routes()
    {
        _routesModel = new RoutesModel();
    }
    
    [BindProperty]
    public List<RouteContainerObject> RoutesList { get; set; }

    [BindProperty]
    public List<AirCraftContainerObject> AircraftList { get; set; }
    public void OnGet()
    {
        try
        {
            RoutesList = _routesModel.GetRoutes();
            AircraftList = _routesModel.GetAircrafts();
            // return Page();
        }
        catch
        {
            ViewData["Error"] = "Failed to load airports data.";
            // return Page();
        }
    }
}