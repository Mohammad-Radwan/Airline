using Microsoft.AspNetCore.Mvc;
using System;
using MySql.Data.MySqlClient;
using Airline.Models;

public class AccountController : Controller
{
    private string connStr = "server=localhost;port=3306;user=root;password=mradwan#1MySql;database=airline;";

    public IActionResult AcountLogin()
    {
        return View();
    }

    public IActionResult PassengerLogin()
    {
        return View();
    }

    public IActionResult FlightAttendantLogin()
    {
        return View();
    }

    public IActionResult AdminLogin()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AuthenticatePassenger(string email, string password)
    {
        using (var conn = new MySqlConnection(connStr))
        {
            try
            {
                conn.Open();
                string query = "SELECT Passport_No FROM passenger WHERE Email = @Email AND password = @Password";
                
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    var result = cmd.ExecuteScalar();
                    
                    if (result != null)
                    {
                        SessionID.Instance.passengerID = result.ToString();
                        return RedirectToAction("PassengerProfile", "Passenger", new { Passport_No = result.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Login failed: " + ex.Message;
            }
        }
        
        TempData["ErrorMessage"] = "Invalid email or password";
        return RedirectToAction("PassengerLogin");
    }

    [HttpPost]
    public IActionResult AuthenticateFlightAttendant(string email, string password)
    {
        using (var conn = new MySqlConnection(connStr))
        {
            try
            {
                conn.Open();
                string query = "SELECT staff_id FROM flight_attendant WHERE email = @Email AND password = @Password";
                
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    var result = cmd.ExecuteScalar();
                    
                    if (result != null)
                    {
                        return RedirectToAction("Dashboard", "FlightAttendant", new { id = result.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Login failed: " + ex.Message;
            }
        }
        
        TempData["ErrorMessage"] = "Invalid email or password";
        return RedirectToAction("FlightAttendantLogin");
    }

    [HttpPost]
    public IActionResult AuthenticateAdmin(string email, string password)
    {
        using (var conn = new MySqlConnection(connStr))
        {
            try
            {
                conn.Open();
                string query = "SELECT admin_id FROM admin WHERE email = @Email AND password = @Password";
                
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    var result = cmd.ExecuteScalar();
                    
                    if (result != null)
                    {
                        return RedirectToAction("Dashboard", "Admin", new { id = result.ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Login failed: " + ex.Message;
            }
        }
        
        TempData["ErrorMessage"] = "Invalid email or password";
        return RedirectToAction("AdminLogin");
    }
    
    public IActionResult RegisterPassenger()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddPassenger(string Passport_No, string name_, string gender,
        DateTime birth_date, string contact_info, string nationality,
        string Email, string Address, string password)
    {
        using (var conn = new MySqlConnection(connStr))
        {
            try
            {
                conn.Open();
                string query = @"INSERT INTO passenger 
                    (Passport_No, name_, gender, birth_date, contact_info, nationality, Email, Address, password)
                    VALUES
                    (@Passport_No, @name_, @gender, @birth_date, @contact_info, @nationality, @Email, @Address, @password)";

                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Passport_No", Passport_No);
                    cmd.Parameters.AddWithValue("@name_", name_);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.Parameters.AddWithValue("@birth_date", birth_date);
                    cmd.Parameters.AddWithValue("@contact_info", contact_info);
                    cmd.Parameters.AddWithValue("@nationality", nationality);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@Address", Address);
                    cmd.Parameters.AddWithValue("@password", password);

                    cmd.ExecuteNonQuery();
                    TempData["SuccessMessage"] = "Registration successful! Please login.";
                    return RedirectToAction("PassengerLogin");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Registration failed: " + ex.Message;
                return RedirectToAction("RegisterPassenger");
            }
        }
    }

}