using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace Airline.Views.Admin;
using Airline.Models;
// using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

public class SeeAmenitiesModel : PageModel
{
    public List<Supplier> Sliers {get; set;} 
    public void OnGet()
    {
        Sliers = new List<Supplier>();
        SqlConnection con = sql_helper.GetConnectionObject();
        string Query = "select * from supplier";
        List<object> res = sql_helper.MakeCommandWithReturn(Query, con, Mode:"Other");
        for(int i = 0; i< res.Count; i++){
            Sliers.Add(new Supplier{
                ID = res[0].ToString(),
                Content = res[1].ToString()
            });
        }
    }
}