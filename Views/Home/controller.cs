using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Airline.Models; // Namespace containing sql_helper class
using System.Collections.Generic;

namespace Airline.Controllers
{
    public class AuthController : Controller
    {
        private readonly string connectionFile = "secret.json";
        private readonly string connectionKey = "connstr";

        // Login Action
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Message = "Username and Password are required.";
                return View("login");
            }

            string query = "SELECT * FROM EMPLOYEE WHERE username_ID = @username AND Pass_word = @password";
            SqlConnection conn = sql_helper.GetConnectionObject(connectionFile, connectionKey);

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@username", username),
                new SqlParameter("@password", password) // Ideally, use hashed passwords
            };

            var result = sql_helper.MakeCommandWithReturn(query, conn, parameters);

            if (result.Count > 0)
            {
                ViewBag.Message = "Login successful!";
                return RedirectToAction("Dashboard", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid credentials.";
                return View("login");
            }
        }

        // Register Action
        public IActionResult Register(string role, string username, string password, string name, string contactInfo, string nationality, string additionalField)
        {
            if (string.IsNullOrEmpty(role) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Message = "Required fields are missing.";
                return View("register");
            }

            SqlConnection conn = sql_helper.GetConnectionObject(connectionFile, connectionKey);

            string query = role == "employee" 
                ? "INSERT INTO EMPLOYEE (emp_id, name_, role_, Contact_info, nationality, username_ID, Pass_word) VALUES (@id, @name, @role, @contact, @nationality, @username, @password)"
                : "INSERT INTO PASSENGER (Passport_No, name_, contact_info, nationality, username_ID, Pass_word) VALUES (@id, @name, @contact, @nationality, @username, @password)";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", additionalField),
                new SqlParameter("@name", name),
                new SqlParameter("@role", role),
                new SqlParameter("@contact", contactInfo),
                new SqlParameter("@nationality", nationality),
                new SqlParameter("@username", username),
                new SqlParameter("@password", password) // Hash password before storing
            };

            try
            {
                sql_helper.MakeCommandWithoutReturn(query, parameters, conn);
                ViewBag.Message = "Registration successful!";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error: {ex.Message}";
                return View("register");
            }
        }
    }
}
