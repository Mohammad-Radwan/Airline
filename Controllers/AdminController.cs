using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Airline.Models;

namespace Airline.Controllers;

public class AdminController : Controller
{
    private readonly ILogger<AdminController> _logger;

    public AdminController(ILogger<AdminController> logger)
    {
        _logger = logger;
    }

    public IActionResult ScheduleFlights()
    {
        return View();
    }

    public IActionResult SeeAmenities()
    {
        return View();
    }

    public IActionResult TrackCargo()
    {
        return View();
    }

    public IActionResult MonitorIncd()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}