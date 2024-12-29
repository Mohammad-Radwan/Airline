using System.Data;
using System.Data.SqlClient;
namespace WebApplication1.Models;

public class ReportIncidentModel
{
public bool SendReport(string flight_id , string incident_location , DateTime date , int casualities_count , int survivors_count , string incident_cause , string penalties)
    {
        SqlQueryHelper sqh = new SqlQueryHelper();

        List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@flight_id", SqlDbType.VarChar, 50) { Value = flight_id },
            new SqlParameter("@incident_location", SqlDbType.VarChar, 50) { Value = incident_location },
            new SqlParameter("@date", SqlDbType.DateTime) { Value = date },
            new SqlParameter("@casualities_count", SqlDbType.Int) { Value = casualities_count },
            new SqlParameter("@survivors_count", SqlDbType.Int) { Value = survivors_count },
            new SqlParameter("@incident_cause", SqlDbType.VarChar, 50) { Value = incident_cause },
            new SqlParameter("@penalties", SqlDbType.VarChar, 50) { Value = penalties }
        };

        string query = @"INSERT INTO Incident (Flight_ID, Incident_location, date_, No_of_Casualties, No_of_Survivors, Cause_of_Incident, Penalties) 
                        VALUES (@flight_id, @incident_location, @date, @casualities_count, @survivors_count, @incident_cause, @penalties);";
        
        var affectedLines = sqh.MakeCommandWithoutReturn(
            query,            
            parameters,
            sqh.GetConnectionObject()
        );
        
        
        return affectedLines > 0  ? true : false;
    }
    
}