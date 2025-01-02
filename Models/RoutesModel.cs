namespace Airline.Models;

public class RoutesModel
{
    public List<RouteContainerObject> GetRoutes()
    {
        var Routes = new List<RouteContainerObject>();
        SqlQueryHelper sqh = new SqlQueryHelper();
        var readers = sqh.MakeCommandWithReturn(
            "SELECT * FROM ROUTE;", 
            sqh.GetConnectionObject(), 
            null,
            "",
            6);
        
        foreach(List<object> reader in readers)
        {
            // Console.WriteLine(reader.ToString());
            Routes.Add(new RouteContainerObject
            {
                RouteID = reader[0].ToString()
                , DepartureAirport = reader[1].ToString()
                , ArrivalAirport = reader[2].ToString()
                , Distance = reader[3].ToString()
                , Duration = reader[4].ToString()
                , BasePrice = reader[5].ToString()
                
            });
            
        }
    
        return Routes;
    }
    
    public List<AirCraftContainerObject> GetAircrafts()
    {
        var Routes = new List<AirCraftContainerObject>();
        SqlQueryHelper sqh = new SqlQueryHelper();
        var readers = sqh.MakeCommandWithReturn(
            "SELECT * FROM AIRCRAFT;", 
            sqh.GetConnectionObject(), 
            null,
            "",
            6);
        
        foreach(List<object> reader in readers)
        {
            // Console.WriteLine(reader.ToString());
            Routes.Add(new AirCraftContainerObject
            {
                AircraftID = reader[0].ToString()
                , Model = reader[1].ToString()
                , Manufacturer = reader[2].ToString()
                , Capacity = reader[3].ToString()
                , JoinDate = reader[4].ToString()
                , Speed = reader[5].ToString()
                
            });
            
        }
    
        return Routes;
    }
}