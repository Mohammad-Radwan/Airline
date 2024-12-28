using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace Airline.Views;
using System.Data.SqlClient;
public class ScheduleFlightsModel : PageModel
{
    private readonly string conString = "Server=localhost,1433;Database=AIRLINE;User Id=SA;Password=Mohamed365;TrustServerCertificate=True;";
    // [BindProperty]
    // public DateTime searchDate { get; set; }
    // [BindProperty]
    // public string crewSSN { get; set; }
    // [BindProperty]
    // public TimeSpan new_time { get; set; }
    // [BindProperty]
    // public string from { get; set; }
    // [BindProperty]
    // public string to { get; set; }
    // [BindProperty]
    // public TimeSpan Dtime {get; set;}

    public List<FlightA> flights { get; set; }

    // public IActionResult OnPost(){

    // }
    
    // public void OnGet()
    // {
    //     flights = new List<FlightA>();
    //     searchDate = DateTime.Now;
    //     crewSSN = "XXXXXX";
    //     from = "XXX";
    //     to = "XXX";
    //     Dtime = TimeSpan.Zero;

    //     string sqlQ = "select route_id, CAST(depart_time AS TIME), duration from FLIGHT WHERE CAST(depart_time AS DATE) =@searchDate;";
    //     SqlConnection con= new SqlConnection(conString);
    //     try
    //     {
    //         con.Open();
    //         Console.WriteLine("Connection Opened");
    //         SqlCommand cmd = new SqlCommand(sqlQ, con);
    //         cmd.Parameters.AddWithValue("@SearchDate", searchDate.Date);
    //         SqlDataReader reader = cmd.ExecuteReader();
    //         while (reader.Read())
    //         {
    //             Console.WriteLine("reading database...");
    //             string part1 = reader[0].ToString().Substring(0, 3);
    //             string part2 = reader[0].ToString().Substring(3, 3);
    //             new_time = (TimeSpan)reader[1];
    //             string route = $"{part1} -> {part2}";
    //             flights.Add(new FlightA {
    //                 Route = route,
    //                 DepartureTime = reader[1].ToString(),
    //                 Duration = int.Parse(reader[2].ToString())
    //             });
    //         }
    //     }
    //     catch (SqlException ex)
    //     { 
    //         Console.WriteLine(ex.ToString());
    //     }
    //     finally{
    //         con.Close();
    //         Console.ReadKey();
    //     }
    // }
}