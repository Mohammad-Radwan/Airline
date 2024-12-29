using System.Data;
using System.Data.SqlClient;
namespace WebApplication1.Models;

public class FlightStatusModel
{
    public List<FlightStatusContainerObject> GetFlightDetails(string FlightID, DateTimeOffset DepartTime)
    {
        SqlQueryHelper sqh = new SqlQueryHelper();

        List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@FlightID", SqlDbType.VarChar, 50) { Value = FlightID },
            new SqlParameter("@DepartTime", SqlDbType.DateTimeOffset) { Value = DepartTime }
        };

        string query = @"SELECT * FROM FLIGHT WHERE fid = @FlightID AND depart_time = @DepartTime;";

        var result = sqh.MakeCommandWithReturn(
            query,
            sqh.GetConnectionObject(),
            parameters,
            "",
            7
        );

        List<FlightStatusContainerObject> flightStatuses = new List<FlightStatusContainerObject>();

        foreach (List<object> reader in result)
        {
            Console.WriteLine($"-------------->>>>>>>>>{reader[0].ToString()}");

            flightStatuses.Add(new FlightStatusContainerObject
            {
                FlightID = reader[0].ToString(),
                FlightAirCraft = reader[1].ToString(),
                FlightDepartTime = DateTimeOffset.Parse(reader[2].ToString()),
                FlightStatus = reader[3].ToString(),
                FlightRoute = reader[4].ToString(),
                FlightArrivalTime = DateTimeOffset.Parse(reader[5].ToString()),
                FlightDuration = int.Parse(reader[6].ToString())
            });
        }

        return flightStatuses;
    }
}