using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace Airline.WebApplication1.WebApplication1.Pages;
using Airline.WebApplication1.WebApplication1.Pages;
using System.Data.SqlClient;
using System;
using System.Data;

public class ScheduleFlightsModel : PageModel
{
    private readonly string conString = "Server=localhost,1433;Database=AIRLINE;User Id=SA;Password=Mohamed365;TrustServerCertificate=True;";
    [BindProperty]
    public DateTime searchDate { get; set; } = DateTime.Now;
    [BindProperty]
    public string crewSSN { get; set; }
    [BindProperty]
    public TimeSpan new_time { get; set; }
    [BindProperty]
    public string from { get; set; }
    [BindProperty]
    public string to { get; set; }
    [BindProperty]
    public TimeSpan Dtime {get; set;}

    public List<FlightA> flights { get; set; }
    
    public void loadFlights(){
        flights = new List<FlightA>();

        string sqlQ = $"select fid, CAST(depart_time AS TIME), duration, fid from FLIGHT WHERE CAST(depart_time AS DATE) ='{searchDate.Date.ToString("yyyy-MM-dd")}';";
        SqlConnection con= new SqlConnection(conString);
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(sqlQ, con);
            // cmd.Parameters.Add(new SqlParameter("@searchDate", SqlDbType.Date) { Value = searchDate.Date });
            
            Console.WriteLine(searchDate.Date.ToString("yyyy-MM-dd"));
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("reading database...");
                Console.WriteLine($"Row Data: Route = {reader[0]}, Time = {reader[1]}, Duration = {reader[2]}");
                string part1 = reader[0].ToString().Substring(0, 3);
                string part2 = reader[0].ToString().Substring(3, 3);
                
                flights.Add(new FlightA {
                    Route =  part1 +" -> "+part2,
                    DepartureTime= reader.GetFieldValue<TimeSpan>(1).ToString(@"hh\:mm"),
                    Duration = int.Parse(reader[2].ToString()),
                    fid = reader[3].ToString()
                });
            }
            Console.WriteLine("End of query");
        }
        catch (SqlException ex)
        { 
            Console.WriteLine(ex.ToString());
        }
        finally{
            con.Close();
            Console.WriteLine("Conection closed");
        }
    }

    public void changeTime(string flightID){
        Console.WriteLine($"{flightID}\t{new_time.ToString(@"hh\:mm\:ss")}");
        string sqlQ = $"UPDATE FLIGHT SET depart_time = '{DateTime.Now.Date.ToString("yyyy-MM-dd")} {new_time.ToString(@"hh\:mm\:ss")} -02:00' WHERE fid = '{flightID}';";
        SqlConnection con= new SqlConnection(conString);
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(sqlQ, con);
            cmd.ExecuteNonQuery();
            Console.WriteLine("End of insert query");
        }
        catch (SqlException ex)
        { 
            Console.WriteLine(ex.ToString());
        }
        finally{
            con.Close();
            Console.WriteLine("Conection closed");
        }
    }
    public void addSSN(string flightID){
        string sqlQ = $"INSERT INTO WORK (Flight_ID, Employee_ID) VALUES ('{flightID}', '{crewSSN}');";
        SqlConnection con= new SqlConnection(conString);
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(sqlQ, con);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Added Employee");
        }
        catch (SqlException ex)
        { 
            Console.WriteLine(ex.ToString());
        }
        finally{
            con.Close();
            Console.WriteLine("Conection closed");
        }
    }

    public void OnGet()
    {
        loadFlights();
    }

    public IActionResult OnPost(string flightID){
        var action = Request.Form["Action"];
        if (string.IsNullOrEmpty(action)){
            return Page();
        }
        else if(action == "date"){
            loadFlights();
            return Page();
        }
        else if(action == "Schedule"){
            changeTime(flightID);
            loadFlights();
            return Page();
        }
        else if(action == "Add"){
            addSSN(flightID);
            loadFlights();
            return Page();
        }
        else if(action == "Add New Flight"){
            return Page();
        }
        return Page();
    }
}
