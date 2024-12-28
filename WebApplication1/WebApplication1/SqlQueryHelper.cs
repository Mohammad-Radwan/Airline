namespace WebApplication1;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
public static class SqlQueryHelper
{
    public static SqlConnection GetConnectionObject(string file_name = "secret.json" , string ConnStrKey = "connstr")
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile(file_name)
            .Build();
    
       
        SqlConnection conn = new SqlConnection(configuration.GetSection(ConnStrKey).Value); 

        Console.WriteLine($"Connection Object Created ==================>>>>>>>>>>>>>>>{conn}");
        return conn;
    }

    public static List<object> MakeCommandWithReturn(string Query, SqlConnection conn_object, List<SqlParameter> parameters = null, string Mode = "QuickRetreival")
{
    List<object> return_list = new List<Object>();
    try
    {
        SqlCommand cmd = new SqlCommand(Query, conn_object);
        conn_object.Open();
        
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
            if (parameters != null)
            {
                foreach(SqlParameter param in parameters)
                {
                    cmd.Parameters.Add(param);
                }
            }
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                return_list.Add(reader);
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
    finally
    {
        conn_object.Close();
    }
    return return_list;
}
    public static int MakeCommandWithoutReturn(string Query,List<SqlParameter> parameters, SqlConnection conn_object)
    {
        SqlCommand cmd = new SqlCommand(Query, conn_object);
        cmd.CommandType = CommandType.StoredProcedure;
        conn_object.Open();
        foreach (SqlParameter param in parameters)
        {
            cmd.Parameters.Add(param);
        }
        conn_object.Close();
        return cmd.ExecuteNonQuery();//for multiple crud operations
    }
}