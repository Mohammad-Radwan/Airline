using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace Airline.WebApplication1.WebApplication1.Pages;
using Airline.WebApplication1.WebApplication1.Pages;
using System.Data.SqlClient;
using System;
using System.Data;


public class TrackCargoModel : PageModel
{
    private readonly string conString = "Server=YOUSSEF_MOHAMAD\\MYMSSQLSERVER;Database=airline_3;Trusted_Connection=True;TrustServerCertificate=True;";
    public List<Cargo> cargos {get; set;} = new List<Cargo>();
    public double sum=0;
    public int count = 0;
    public double avg=0;

    public void OnGet()
    {
        SqlConnection con = new SqlConnection(conString);
        con.Open();
        string Query = "SELECT Cargo_ID, Cargo.capacity, model, AIRCRAFT.capacity FROM (Cargo JOIN AIRCRAFT ON Aircraft_ID = aid);";
        SqlCommand cmd = new SqlCommand(Query, con);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            sum= sum +(double.Parse(reader[1].ToString())/double.Parse(reader[3].ToString())) * 100;
            if( (double.Parse(reader[1].ToString())/double.Parse(reader[3].ToString())) > 0.9 ){
                count=count +1 ;
            }
            cargos.Add(new Cargo{
                CargoID =reader[0].ToString(),
                CargoCap = double.Parse(reader[1].ToString()),
                AircraftID = reader[2].ToString(),
                AircraftCap = double.Parse(reader[3].ToString()),
                Utilization = (double.Parse(reader[1].ToString())/double.Parse(reader[3].ToString())) * 100
            });
        }
        avg = (sum / cargos.Count);
        con.Close();
    }
}
