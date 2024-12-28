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
        Query = "SELECT fid, CAST(depart_time AS TIME), Supplier_ID, content FROM (Supply JOIN FLIGHT ON Flight_ID= fid) WHERE CAST(depart_time AS DATE) = @currentD;";
        SqlCommand cmd2 = new SqlCommand(Query, con);
        cmd2.Parameters.Add(new SqlParameter("@currentD", SqlDbType.Date) { Value = DateTime.Now.Date });
        
        reader = cmd2.ExecuteReader();
        while (reader.Read())
        {
            string f = reader[0].ToString().Substring(0, 3);
            string t = reader[0].ToString().Substring(3, 3);

            Suplies.Add(new Supply {
                FlightID = f+ " -> " + t,
                DepTime = reader.GetFieldValue<DateTimeOffset>(1).TimeOfDay.ToString(),
                SliersID = reader [2].ToString(),
                Content = reader[3].ToString()
            });
        }
        con.Close();
    }
}