namespace Airline.Models;
using Microsoft.Data.SqlClient;
using static Airline.Models.sql_helper;
using System.Data;
using System.Collections.Generic;
public class flight_schedules
{
    public List<object> GetAirports()
    {
        return MakeCommandWithReturn("SELECT name_; FROM AIRPORT;" 
            , GetConnectionObject()
            , null
            ,"");
    
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