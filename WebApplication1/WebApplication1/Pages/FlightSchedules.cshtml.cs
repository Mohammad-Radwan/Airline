// using Microsoft.AspNetCore.Mvc.RazorPages;
//
// namespace WebApplication1.Pages;
//
// public class FlightSchedules : PageModel
// {
//     public void OnGet()
//     {
//         
//     }
// }
//
//
// ﻿using Microsoft.AspNetCore.Mvc.RazorPages;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.RazorPages;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.ViewFeatures;
// using Microsoft.AspNetCore.Mvc.ModelBinding;
// using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
//
// namespace Airline.Views.Flight_Crew_Member
// {
//     public class flight_schedules : PageModel
//     {
//         private readonly Airline.Models.flight_schedules _flightSchedules;
//         private readonly ILogger<flight_schedules> _logger;
//     
//         public List<object> Airports { get; private set; }
//         public string ErrorMessage { get; private set; }
//
//         public flight_schedules(ILogger<flight_schedules> logger)
//         {
//             _flightSchedules = new Airline.Models.flight_schedules();
//             _logger = logger;
//         }
//
//         public void OnGet()
//         {
//             try
//             {
//                 _logger.LogInformation("Getting airports...");
//                 Airports = _flightSchedules.GetAirports();
//                 ViewData["Airports"] = Airports;
//                 _logger.LogInformation($"Found {Airports?.Count ?? 0} airports");
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error getting airports");
//                 Airports = new List<object>();
//                 ViewData["Airports"] = Airports;
//                 ErrorMessage = "Unable to load airports. Please try again later.";
//                 ViewData["Error"] = ErrorMessage;
//             }
//         }
//     
//
//         
//         public IActionResult OnPostGetSchedules([FromBody] ScheduleRequest request)
//         {
//             try 
//             {
//                 var schedules = _flightSchedules.GetFlightSchedules(
//                     request.fromAirport,
//                     request.toAirport,
//                     request.date
//                 );
//                 foreach (var sch in schedules)
//                 {
//                     Console.WriteLine(sch);
//                 }
//                 return new JsonResult(schedules);
//             }
//             catch (Exception ex)
//             {
//                 return new JsonResult(new { error = ex.Message }) { StatusCode = 500 };
//             }
//         }
//     }
//
//     public class ScheduleRequest
//     {
//         public string fromAirport { get; set; }
//         public string toAirport { get; set; }
//         public DateTimeOffset date { get; set; }
//     }
// }