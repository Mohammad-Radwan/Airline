namespace Airline.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
public class sql_helper
{
    public SqlConnection GetConnectionObject(string file_name = "secret.json" , string ConnStrKey = "connstr")
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile(file_name)
            .Build();
    
        //var object of type SqlConnection
        SqlConnection conn = new SqlConnection(configuration.GetSection(ConnStrKey).Value); 
        return conn;
    }

    public List<object> MakeCommand(string Query, SqlConnection conn_object, string Mode = "QuickRetreival")
    {
        SqlCommand cmd = new SqlCommand(Query, conn_object);
        List<object> return_list = [];
        if (Mode == "QuickRetreival")
        {
            //QuickRetreival is the default mode and returns a list of rows of the table
            cmd.CommandType = CommandType.TableDirect;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                return_list.Add(reader);
            }
        }

        else
        {
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                return_list.Add(reader);
            }
        }

        return return_list;
    }
}