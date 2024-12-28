namespace WebApplication1.Models;
using Microsoft.Data.SqlClient;
using static SqlQueryHelper;
using System.Data;
using System.Collections.Generic;
public class FlightSchedulesModel 
{
    public List<AirportContainerObject> GetAirports()
    {
        var airportNames = new List<AirportContainerObject>();
        var readers = MakeCommandWithReturn(
            "SELECT name_ FROM AIRPORT;", 
            GetConnectionObject(), 
            null,
            "");
        
        foreach(SqlDataReader reader in readers)
        {
            Console.WriteLine(reader.ToString());
            airportNames.Add(new AirportContainerObject { AirportName = reader[0].ToString() });
            
        }
    
        return airportNames;
    }

    public List<object> GetFlightSchedules(string airport_from , string airport_to , DateTimeOffset date)
    {
    
        List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@airport_from", SqlDbType.VarChar, 50) { Value = airport_from },
            new SqlParameter("@airport_to", SqlDbType.VarChar, 50) { Value = airport_to },
            new SqlParameter("@date", SqlDbType.DateTimeOffset) { Value = date }
        };

        string query =  "SELECT * "
                        + "FROM (FLIGHT_SCHEDULE INNER JOIN ROUTE ON route_id = ro_id) AS T1 "
                        + "INNER JOIN AIRPORT ON T1.start_airport = AIRPORT.airport_id AND T1.end_airport = AIRPORT.airport_id "
                        + "WHERE start_airport = @airport_from AND end_airport = @airport_to AND depart_time = @date;";

        return MakeCommandWithReturn(
            query,
            GetConnectionObject(),
            parameters,
            ""
        );

    }

}