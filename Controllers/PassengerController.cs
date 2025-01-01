using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Airline.Models;

public class PassengerController : Controller
{
    string connStr = "server=localhost;port=3306;user=root;password=mradwan#1MySql;database=airline;";


    [HttpGet("passenger/profile/{Passport_No}")]
    public IActionResult PassengerProfile(string Passport_No)
    {
        Passenger passenger = null;


        using (var conn = new MySqlConnection(connStr))
        {
            conn.Open();
            string query = "SELECT * FROM passenger WHERE Passport_No = @Passport_No";
            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Passport_No", Passport_No);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        passenger = new Passenger
                        {
                            Passport_No = reader["Passport_No"].ToString(),
                            Name_ = reader["name_"].ToString(),
                            Gender = reader["gender"].ToString(),
                            Address = reader["Address"].ToString(),
                            Birth_Date = reader.GetDateTime("birth_date"),
                            Contact_Info = reader["contact_info"].ToString(),
                            Email = reader["Email"].ToString(),
                            Nationality = reader["Nationality"].ToString(),
                        };
                    }
                }
            }
        }

        if (passenger == null)
        {
            return NotFound(); // Return a 404 if no passenger is found
        }


        return View(passenger);
    }


    [HttpGet("passenger/TicketRecord/{Passport_No}")]
    public IActionResult TicketRecord(string Passport_No)
    {
        var viewModel = new TicketRecordViewModel
        {
            Tickets = new List<Ticket>(),
            Flights = new List<Flight>(),
            Routes = new List<Route>()
        };

        using (var conn = new MySqlConnection(connStr))
        {
            conn.Open();
            string query = """
                           SELECT 
                               t.Ticket_ID, 
                               t.Class, 
                               t.Pay_Status, 
                               f.depart_time, 
                               r.start_airport, 
                               r.end_airport, 
                               r.base_price
                           FROM 
                               Ticket t
                           JOIN 
                               Flight f ON t.Flight_ID = f.fid
                           JOIN 
                               Route r ON f.route_id = r.ro_id
                           WHERE 
                               t.Passport_No = @Passport_No;
                           """;

            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Passport_No", Passport_No);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        viewModel.Tickets.Add(new Ticket
                        {
                            Ticket_ID = reader["Ticket_ID"].ToString(),
                            Class = reader["Class"].ToString(),
                            Pay_Status = reader["Pay_Status"].ToString()
                        });

                        viewModel.Flights.Add(new Flight
                        {
                            depart_time = reader.GetDateTime("depart_time")
                        });

                        viewModel.Routes.Add(new Route
                        {
                            start_airport = reader["start_airport"].ToString(),
                            end_airport = reader["end_airport"].ToString(),
                            base_price = reader.GetDecimal("base_price")
                        });
                    }
                }
            }
        }

        return View(viewModel);
    }

    [HttpGet("passenger/TicketDetails/{id}")]
    public IActionResult TicketDetails(string id)
    {
        Airline.Models.TicketDetails ticketDetails = new Airline.Models.TicketDetails();

        using (var conn = new MySqlConnection(connStr))
        {
            conn.Open();
            string query = """
                           SELECT *
                           FROM 
                               Ticket t
                           JOIN 
                               Flight f ON t.Flight_ID = f.fid
                           JOIN 
                               Route r ON f.route_id = r.ro_id
                           WHERE 
                               t.Ticket_ID = @Ticket_ID;
                           """;

            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Ticket_ID", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Handle Discount
                        string discountStr = reader["discount"].ToString();
                        decimal discountValue = 0;
                        if (!string.IsNullOrWhiteSpace(discountStr) && discountStr.EndsWith("%"))
                        {
                            discountStr = discountStr.TrimEnd('%');
                            discountValue = decimal.Parse(discountStr) / 100;
                        }

                        // Convert duration to TimeSpan
                        TimeSpan flightDuration;
                        if (reader["duration"] is int durationInt)
                        {
                            flightDuration = TimeSpan.FromMinutes(durationInt); // Adjust this based on your data
                        }
                        else if (reader["duration"] is TimeSpan durationTimeSpan)
                        {
                            flightDuration = durationTimeSpan;
                        }
                        else
                        {
                            throw new InvalidCastException("Invalid duration data type");
                        }

                        ticketDetails.ticket = new Ticket
                        {
                            Ticket_ID = reader["Ticket_ID"].ToString(),
                            Passport_No = reader["Passport_No"].ToString(),
                            Class = reader["Class"].ToString(),
                            Pay_Status = reader["Pay_Status"].ToString(),
                            Payment_method = reader["Payment_method"].ToString(),
                            Seat_id = reader["seat_id"].ToString(),
                            Discount = discountValue,
                            Is_cancelled = reader.GetBoolean("is_cancelled")
                        };

                        ticketDetails.flight = new Flight
                        {
                            fid = reader["fid"].ToString(),
                            depart_time = reader.GetDateTime("depart_time"),
                            status_ = reader["status_"].ToString(),
                            arrival_time = reader.GetDateTime("arrival_time"),
                            duration = flightDuration // Correctly set the duration
                        };

                        ticketDetails.route = new Route
                        {
                            ro_id = reader["ro_id"].ToString(),
                            start_airport = reader["start_airport"].ToString(),
                            end_airport = reader["end_airport"].ToString(),
                            distance = reader.GetDouble("distance"),
                            duration_in_hours = reader.GetDouble("duration_in_hours"),
                            base_price = reader.GetDecimal("base_price")
                        };
                    }
                }
            }
        }

        return View(ticketDetails);
    }


    public IActionResult RefundRequest(string id)
    {
        var refundRequest = new RefundRequest
        {
            Ticket_ID = id,
            date_ = DateTime.Now
        };

        return View(refundRequest);
    }


    public IActionResult SubmitRefund(RefundRequest request)
    {
        // Setting a success message to TempData
        TempData["SuccessMessage"] = "Your refund request has been recorded. We will inform you of any updates.";

        // Define the query to insert the refund request into the refund table
        string query = @"
        INSERT INTO refund (Ticket_ID, Amount, date_, Status_, description_)
        VALUES (@Ticket_ID, NULL, @RequestDate, 'Pending', @Description)";

        using (var conn = new MySqlConnection(connStr))
        {
            try
            {
                conn.Open();

             
                using (var cmd = new MySqlCommand(query, conn))
                {
                   
                    cmd.Parameters.AddWithValue("@Ticket_ID", request.Ticket_ID);
                    cmd.Parameters.AddWithValue("@RequestDate", DateTime.Now); 
                    cmd.Parameters.AddWithValue("@Description",
                        request.description_); 

                 
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
               
                TempData["ErrorMessage"] = "You Already Have A Pending Request ";
            }
        }

        
        return RedirectToAction("TicketDetails", new { id = request.Ticket_ID });
    }


    [HttpGet("passenger/BaggageRecord/{Passport_No}")]

    public IActionResult BaggageRecord(string Passport_No)
    {
        var baggageList = new List<Baggage>();

        using (var conn = new MySqlConnection(connStr))
        {
            conn.Open();
            string query = "SELECT * FROM baggage WHERE Passport_No = @Passport_No";
            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Passport_No", Passport_No);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        baggageList.Add(new Baggage
                        {
                            TAG = reader["TAG"].ToString(),
                            Cargo_ID = reader["Cargo_ID"].ToString(),
                            Weight_ = reader.GetDouble("Weight_"),
                            Type_ = reader["Type_"].ToString(),
                            Board_No = reader["Board_No"].ToString(),
                            Passport_No = reader["Passport_No"].ToString()
                        });
                    }
                }
            }
        }

        return View(baggageList);
    }
}