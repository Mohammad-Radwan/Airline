using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace Airline.WebApplication1.WebApplication1.Pages;
using Airline.WebApplication1.WebApplication1.Pages;
using System.Data.SqlClient;
using System;
using System.Data;

public class MonitorIncdModel : PageModel
{
    private readonly string conString = "Server=YOUSSEF_MOHAMAD\\MYMSSQLSERVER;Database=airline_3;Trusted_Connection=True;TrustServerCertificate=True;";
    public List<Inc> incs {get; set;} = new List<Inc>();
    public int count = 0;
    public int total_cas = 0;
    public int total_sur = 0;
    public int sur_rate = 0;

    public void OnGet()
    {
        SqlConnection con = new SqlConnection(conString);
        con.Open();
        string Query = "SELECT Flight_ID, Incident_location,date_,No_of_Casualties ,No_of_Survivors ,Cause_of_Incident, Penalties FROM Incident ORDER BY date_ dsc;";
        SqlCommand cmd = new SqlCommand(Query, con);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            incs.Add(new Inc{
                Route= reader[0].ToString().Substring(0, 3)+" -> "+ reader[0].ToString().Substring(3, 3) ,
                Location = reader[1].ToString() ,
                Date =  reader.GetFieldValue<DateTime>(2).ToString(),
                Casualties =  int.Parse(reader[3].ToString()) ,
                Survivors =   int.Parse(reader[4].ToString()) ,
                Cause = reader[5].ToString()   ,
                Details =   reader[6].ToString()
                });
        }
        con.Close();

        SqlConnection con2 = new SqlConnection(conString);
        con2.Open();
        Query = "select Count(*),SUM(No_of_Casualties), SUM(No_of_Survivors) from Incident;";
        SqlCommand cmd2 = new SqlCommand(Query, con2);

        SqlDataReader reader2 = cmd2.ExecuteReader();
        if (reader2.Read())
        {
            count = int.Parse(reader2[0].ToString());
            total_cas = int.Parse(reader2[1].ToString());
            total_sur = int.Parse(reader2[2].ToString());
        }
        con2.Close();
        sur_rate = total_sur / (total_cas+total_sur) ;
        sur_rate = sur_rate * 100;
    }
}


/*
-- Insert random data into Incident table
insert into Incident (Flight_ID, Incident_location, date_, No_of_Casualties, No_of_Survivors, Cause_of_Incident, Penalties)
values
('LAXJFK001', 'New York', '2024-01-01 12:30:00', 0, 150, 'Bird Strike', 'None'),
*/
