using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Airline.Models;
using System.Diagnostics;

namespace Airline.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly string connectionStr = "Server=DESKTOP-U99FBKT\\Ihab;Database=AIRLINE;Trusted_Connection=True;";

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string Email, string Password)
    {
        bool isEmployee = false;
        bool isAuthenticated = false;

        using (SqlConnection conn = new SqlConnection(connectionStr))
        {
            conn.Open();

            // Check Employee Credentials
            string queryEmployee = "SELECT * FROM EMPLOYEE WHERE username_ID = @Email AND Pass_word = @Password";
            using (SqlCommand cmd = new SqlCommand(queryEmployee, conn))
            {
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Password", Password);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        isEmployee = true;
                        isAuthenticated = true;
                    }
                }
            }

            // Check Passenger Credentials if not authenticated as employee
            if (!isAuthenticated)
            {
                string queryPassenger = "SELECT * FROM PASSENGER WHERE username_ID = @Email AND Pass_word = @Password";
                using (SqlCommand cmd = new SqlCommand(queryPassenger, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@Password", Password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            isAuthenticated = true;
                        }
                    }
                }
            }
        }

        if (isAuthenticated)
        {
            ViewBag.Message = isEmployee ? "Welcome, Employee!" : "Welcome, Passenger!";
            return RedirectToAction("Index");
        }
        else
        {
            ViewBag.Error = "Invalid credentials. Please try again.";
            return View();
        }
    }

    [HttpPost]
    public IActionResult ChangePassword(string OldPassword, string NewPassword)
    {
        bool isUpdated = false;
        string username = HttpContext.Request.Form["username_ID"];

        using (SqlConnection conn = new SqlConnection(connectionStr))
        {
            conn.Open();

            // Update Employee Password
            string queryEmployee = "UPDATE EMPLOYEE SET Pass_word = @NewPassword WHERE username_ID = @Username AND Pass_word = @OldPassword";
            using (SqlCommand cmd = new SqlCommand(queryEmployee, conn))
            {
                cmd.Parameters.AddWithValue("@NewPassword", NewPassword);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@OldPassword", OldPassword);

                if (cmd.ExecuteNonQuery() > 0)
                {
                    isUpdated = true;
                }
            }

            // Update Passenger Password if not updated as employee
            if (!isUpdated)
            {
                string queryPassenger = "UPDATE PASSENGER SET Pass_word = @NewPassword WHERE username_ID = @Username AND Pass_word = @OldPassword";
                using (SqlCommand cmd = new SqlCommand(queryPassenger, conn))
                {
                    cmd.Parameters.AddWithValue("@NewPassword", NewPassword);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@OldPassword", OldPassword);

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        isUpdated = true;
                    }
                }
            }
        }

        if (isUpdated)
        {
            ViewBag.Message = "Password updated successfully.";
        }
        else
        {
            ViewBag.Error = "Failed to update password. Please check your credentials.";
        }

        return View("Login");
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(string userType, string FullName, string Gender, DateTime BirthDate, string Nationality, string Address, string Email, string PhoneNumber, string Username, string Password, string PassportNumber, string LicenseNumber, string SSN, decimal Salary)
    {
        using (SqlConnection conn = new SqlConnection(connectionStr))
        {
            conn.Open();

            if (userType == "passenger")
            {
                // Insert Passenger
                string queryPassenger = "INSERT INTO PASSENGER (Passport_No, name_, gender, birth_date, contact_info, nationality, username_ID, Pass_word) VALUES (@PassportNo, @Name, @Gender, @BirthDate, @ContactInfo, @Nationality, @Username, @Password)";
                using (SqlCommand cmd = new SqlCommand(queryPassenger, conn))
                {
                    cmd.Parameters.AddWithValue("@PassportNo", PassportNumber);
                    cmd.Parameters.AddWithValue("@Name", FullName);
                    cmd.Parameters.AddWithValue("@Gender", Gender);
                    cmd.Parameters.AddWithValue("@BirthDate", BirthDate);
                    cmd.Parameters.AddWithValue("@ContactInfo", PhoneNumber);
                    cmd.Parameters.AddWithValue("@Nationality", Nationality);
                    cmd.Parameters.AddWithValue("@Username", Username);
                    cmd.Parameters.AddWithValue("@Password", Password);

                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                // Insert Employee
                string queryEmployee = "INSERT INTO EMPLOYEE (emp_id, name_, role_, Contact_info, license_number, birth_date, gender, nationality, salary, username_ID, Pass_word) VALUES (NEWID(), @Name, @Role, @ContactInfo, @LicenseNumber, @BirthDate, @Gender, @Nationality, @Salary, @Username, @Password)";
                using (SqlCommand cmd = new SqlCommand(queryEmployee, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", FullName);
                    cmd.Parameters.AddWithValue("@Role", userType);
                    cmd.Parameters.AddWithValue("@ContactInfo", PhoneNumber);
                    cmd.Parameters.AddWithValue("@LicenseNumber", LicenseNumber);
                    cmd.Parameters.AddWithValue("@BirthDate", BirthDate);
                    cmd.Parameters.AddWithValue("@Gender", Gender);
                    cmd.Parameters.AddWithValue("@Nationality", Nationality);
                    cmd.Parameters.AddWithValue("@Salary", Salary);
                    cmd.Parameters.AddWithValue("@Username", Username);
                    cmd.Parameters.AddWithValue("@Password", Password);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        ViewBag.Message = "Registration successful! Please login.";
        return RedirectToAction("Login");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
