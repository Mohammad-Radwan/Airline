namespace WebApplication1.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
public class FlightSchedulesModel 
{
    public List<AirportContainerObject> GetAirports()
    {
        var airportNames = new List<AirportContainerObject>();
        SqlQueryHelper sqh = new SqlQueryHelper();
        var readers = sqh.MakeCommandWithReturn(
            "SELECT name_ FROM AIRPORT;", 
            sqh.GetConnectionObject(), 
            null,
            "");
        
        foreach(List<object> reader in readers)
        {
            // Console.WriteLine(reader.ToString());
            airportNames.Add(new AirportContainerObject { AirportName = reader[0].ToString() });
            
        }
    
        return airportNames;
    }

    public List<FlightScheduleContainerObject> GetFlightSchedules(string airport_from , string airport_to , DateTimeOffset date)
    {
        var FlightsLista = new List<FlightScheduleContainerObject>();

        SqlQueryHelper sqh = new SqlQueryHelper();

        List<SqlParameter> parameters = new List<SqlParameter>
        {
            new SqlParameter("@airport_from", SqlDbType.VarChar, 50) { Value = airport_from },
            new SqlParameter("@airport_to", SqlDbType.VarChar, 50) { Value = airport_to },
            new SqlParameter("@date", SqlDbType.DateTimeOffset) { Value = date }
        };

        string query =  @"SELECT 
            f.fid AS FlightNumber,
            a1.name_ AS DepartureAirport, 
            a2.name_ AS ArrivalAirport,
            f.depart_time AS DepartureTime,
            f.arrival_time AS ArrivalTime,
            'AirlineName' AS Airline,  -- Assuming you would have this data from another table or API
            f.aircraft_id AS Aircraft,
            r.ro_id As RouteID,
            FROM 
            FLIGHT f
            INNER JOIN 
            ROUTE r ON f.route_id = r.ro_id
            INNER JOIN 
            AIRPORT a1 ON r.start_airport = a1.airport_id
            INNER JOIN 
            AIRPORT a2 ON r.end_airport = a2.airport_id
            WHERE 
            r.start_airport = @airport_from
            AND r.end_airport = @airport_to
            AND f.depart_time >= @date 
            AND f.depart_time < DATEADD(DAY, 1, @date);"; //  -- If you want all flights on the given date, this ensures a date range

        var readers = sqh.MakeCommandWithReturn(
            query,
            sqh.GetConnectionObject(),
            parameters,
            "" ,
            8
        );
        foreach(List<object> reader in readers)
        {
            // Console.WriteLine(reader.ToString());
            FlightsLista.Add(new FlightScheduleContainerObject
            {
                FlightNumber = reader[0].ToString(),
                DepartureAirport = reader[1].ToString(),
                ArrivalAirport = reader[2].ToString(),
                DepartureTime = reader[3].ToString(),
                ArrivalTime = reader[4].ToString(),
                Airline = reader[5].ToString(),
                Aircraft = reader[6].ToString(),
                RouteID = reader[7].ToString()
            });        
        }

        return FlightsLista;
    }

}