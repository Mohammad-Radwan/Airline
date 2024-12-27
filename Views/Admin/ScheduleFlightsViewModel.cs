namespace Airline.Models.Admin;
using System.Data.SqlClient;
public class ScheduleFlightsViewModel
{
    private readonly string conString = "Server=localhost,1433;Database=AIRLINE;User Id=SA;Password=Mohamed365;TrustServerCertificate=True;";
    [BindProperty]
    public DateTime searchDate { get; set; }=DateTime.Now.Date;
    [BindProperty]
    public string? crewSSN;
    [BindProperty]
    public TimeSpan? new_time;
    [BindProperty]
    public string? from;
    [BindProperty]
    public string? to;
    public List<Flight> flights;

    public IActionResult OnPost(){

    }
    public void OnGet()
    {
        string sqlQ = "select route_id, CAST(depart_time AS TIME), duration from Flight WHERE CAST(depart_time AS DATE) =" +searchDate+ ";";
        SqlConnection con= new SqlConnection(conString);
        try
        {
            con.Open();
            Console.WriteLine("Connection Opened");
            SqlCommand cmd = new SqlCommand(sqlQ, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("reading database...");
                string part1 = reader[0].ToString().Substring(0, 3);
                string part2 = reader[0].ToString().Substring(3, 3);

                string route = $"{part1} -> {part2}";
                flights.Add(new Flight {
                    Route = route,
                    DepartureTime = reader[1].ToString(),
                    Duration = reader[2].ToString()
                });
                Console.WriteLine(flights[0].Name);
            }
        }
        catch (SqlException ex)
        { 
            Console.WriteLine(ex.ToString());
        }
        finally{
            con.Close();
            Console.ReadKey();
        }
    }
}