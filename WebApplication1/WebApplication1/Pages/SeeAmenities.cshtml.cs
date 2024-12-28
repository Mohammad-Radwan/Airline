using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace Airline.WebApplication1.WebApplication1.Pages;
using Airline.WebApplication1.WebApplication1.Pages;
using System.Data.SqlClient;
using System;
using System.Data;


public class SeeAmenitiesModel : PageModel
{
    private readonly string conString = "Server=localhost,1433;Database=AIRLINE;User Id=SA;Password=Mohamed365;TrustServerCertificate=True;";
    public List<Supplier> Sliers {get; set;} = new List<Supplier>();
    public List<Supply> Suplies {get;set;} = new List<Supply>();

    public void OnGet()
    {
        SqlConnection con = new SqlConnection(conString);
        con.Open();
        string Query = "select * from supplier";
        SqlCommand cmd = new SqlCommand(Query, con);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Sliers.Add(new Supplier {
                ID = reader[0].ToString(),
                Content = reader[1].ToString()
            });
        }
        con.Close();
        Query = @"
            SELECT fid, 
                   CAST(depart_time AS TIME) AS depart_time, 
                   Supply.Supplier_ID, 
                   Content 
            FROM (Supply 
            JOIN FLIGHT ON FLIGHT.fid = Supply.Flight_ID )
            JOIN supplier ON supplier.Supplier_ID = Supply.Supplier_ID 
            WHERE CAST(depart_time AS DATE) = @currentD;";

        SqlConnection con2 = new SqlConnection(conString);
        con2.Open();
        SqlCommand cmd2 = new SqlCommand(Query, con2);
        cmd2.Parameters.Add(new SqlParameter("@currentD", SqlDbType.Date) { Value = DateTime.Now.Date });

        SqlDataReader reader2 = cmd2.ExecuteReader();
        while (reader2.Read())
        {
            string f = reader2[0].ToString().Substring(0, 3);
            string t = reader2[0].ToString().Substring(3, 3);

            Suplies.Add(new Supply {
                FlightID = f+ " -> " + t,
                DepTime = reader2.GetFieldValue<TimeSpan>(1).ToString(),
                SliersID = reader2 [2].ToString(),
                Content = reader2[3].ToString()
            });
        }
        con2.Close();
    }
}