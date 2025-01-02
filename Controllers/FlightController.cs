using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using Airline.Models;

public class FlightController : Controller
{
    
    string connStr = "server=localhost;port=3306;user=root;password=mradwan#1MySql;database=airline;";
    public IActionResult Search()
    {
        return View();
    }

    [HttpPost]public IActionResult SearchResults(DateTime searchDate)
{
    var searchResults = new List<SearchViewModelel>();


    string formattedDate = searchDate.ToString("yyyy-MM-dd");

    string query = @"
        SELECT 
            f.fid, 
            a.model, 
            f.depart_time, 
            f.duration, 
            r.start_airport, 
            r.end_airport, 
            r.base_price 
        FROM 
            flight f
        JOIN 
            aircraft a ON f.aircraft_id = a.aid
        JOIN 
            route r ON r.ro_id = f.route_id
        WHERE 
            f.depart_time LIKE @SearchDate";

    using (var conn = new MySqlConnection(connStr))
    {
        try
        {
            conn.Open();
            using (var cmd = new MySqlCommand(query, conn))
            {

                cmd.Parameters.AddWithValue("@SearchDate", $"{formattedDate}%");

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var result = new SearchViewModelel
                        {
                            flight = new Flight
                            {
                                fid = reader["fid"].ToString(),
                                aircraft_id = reader["model"].ToString(),
                                depart_time = reader.GetDateTime("depart_time"),


                                duration = TimeSpan.FromMinutes(reader.GetInt32("duration"))
                            },
                            route = new Route
                            {
                                start_airport = reader["start_airport"].ToString(),
                                end_airport = reader["end_airport"].ToString(),
                                base_price = reader.GetDecimal("base_price"),
                            }
                        };

                        searchResults.Add(result);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "An error occurred while fetching search results: " + ex.Message;
        }
    }


    return View(searchResults);
}


    public IActionResult Book(string searchResult, DateTime departureTime)
    {
        if (string.IsNullOrEmpty(searchResult))
        {
            TempData["ErrorMessage"] = "Invalid flight selection.";
            return RedirectToAction("Search");
        }

        var bookingModel = new BookingViewModel
        {
            Flight = new Flight(),
            Route = new Route(),
            AvailableClasses = new List<TicketClassInfo>
            {
                new TicketClassInfo { Name = "Economy", PriceMultiplier = 1.0m },
                new TicketClassInfo { Name = "First", PriceMultiplier = 1.5m },
                new TicketClassInfo { Name = "Business", PriceMultiplier = 2.0m }
            }
        };

        string formattedDate = departureTime.ToString("yyyy-MM-dd");
        
        string query = @"
            SELECT 
                f.fid, 
                f.depart_time, 
                f.duration, 
                a.model AS aircraft_model, 
                r.start_airport, 
                r.end_airport, 
                r.base_price 
            FROM 
                flight f
            JOIN 
                aircraft a ON f.aircraft_id = a.aid
            JOIN 
                route r ON f.route_id = r.ro_id
            WHERE 
                DATE(f.depart_time) = @DepartureTime 
                AND f.fid = @FlightID";

        using (var conn = new MySqlConnection(connStr))
        {
            try
            {
                conn.Open();
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DepartureTime", formattedDate);
                    cmd.Parameters.AddWithValue("@FlightID", searchResult);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bookingModel.Flight = new Flight
                            {
                                fid = reader["fid"].ToString(),
                                depart_time = reader.GetDateTime("depart_time"),
                                duration = TimeSpan.FromMinutes(Convert.ToInt32(reader["duration"])),
                                aircraft_id = reader["aircraft_model"].ToString()
                            };

                            bookingModel.Route = new Route
                            {
                                start_airport = reader["start_airport"].ToString(),
                                end_airport = reader["end_airport"].ToString(),
                                base_price = reader.GetDecimal("base_price")
                            };
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Flight not found.";
                            return RedirectToAction("Search");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while fetching the flight details: " + ex.Message;
                return RedirectToAction("Search");
            }
        }

        if (bookingModel.Flight.fid == null)
        {
            TempData["ErrorMessage"] = "Unable to load flight details.";
            return RedirectToAction("Search");
        }

        return View(bookingModel);
    }

    public IActionResult BookingSuccess()
    {
        TempData["BookingMessage"] = "Your request is pending.";
        return RedirectToAction("PassengerProfile", "Passenger", new { Passport_No = SessionID.Instance.passengerID });
    }

    public IActionResult BookingError(string message)
    {
        TempData["BookingMessage"] = "Your are unable to purchase a ticket at this time.";
        return RedirectToAction("PassengerProfile", "Passenger", new { Passport_No = SessionID.Instance.passengerID });
    }
}