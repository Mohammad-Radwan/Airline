using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace Airline.Views.Admin;
using Airline.Models;
using System.Data.SqlClient;
// using Microsoft.Data.SqlClient;

public class SeeAmenitiesModel : PageModel
{
    private readonly string conString = "Server=localhost,1433;Database=AIRLINE;User Id=SA;Password=Mohamed365;TrustServerCertificate=True;";
    public List<Supplier> Sliers {get; set;} = new List<Supplier>();
    
    public void OnGet()
    {
        Console.WriteLine("OnGet");
        // Sliers = new List<Supplier>();
        SqlConnection con= new SqlConnection(conString);
        // SqlConnection con = sql_helper.GetConnectionObject();
        string Query = "select * from supplier";
        // List<object> res = sql_helper.MakeCommandWithReturn(Query, con, Mode:"Other");
        SqlCommand cmd = new SqlCommand(Query, con);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine("reading database...");
            Sliers.Add(new Supplier {
                ID = reader[0].ToString(),
                Content = reader[1].ToString()
            });
        }
    }
}
