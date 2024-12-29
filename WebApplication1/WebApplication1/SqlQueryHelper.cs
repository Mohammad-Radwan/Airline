namespace WebApplication1;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
public  class SqlQueryHelper
{
    public SqlConnection GetConnectionObject(string file_name = "secret.json", string ConnStrKey = "connstr")
{
    try
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile(file_name)
            .Build();
        Console.WriteLine($"Configuration Object Created ======>>>>>>{configuration.GetSection(ConnStrKey).Value}");
        SqlConnection conn = new SqlConnection(configuration.GetSection(ConnStrKey).Value);

        Console.WriteLine($"Connection Object Created ==================>>>>>>>>>>>>>>>{conn}");
        return conn;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while creating the connection object: {ex.Message}");
        return null;
    }
}
    public List<List<object>> MakeCommandWithReturn(string Query, SqlConnection conn_object, List<SqlParameter> parameters = null, string Mode = "QuickRetreival", int HowManyColums = 1)
{
    List<List<object>> return_list = new List<List<object>>();
    try
    {
        SqlCommand cmd = new SqlCommand(Query, conn_object);
        conn_object.Open();
        
        if (Mode == "QuickRetreival")
        {
            // QuickRetreival is the default mode and returns a list of rows of the table
            Console.WriteLine("QuickRetreival Mode");
            cmd.CommandType = CommandType.TableDirect;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                List<object> row = new List<object>();
                for (int i = 0; i < HowManyColums; i++)
                {
                    row.Add(reader[i]);
                }
                return_list.Add(row);
            }
        }
        else
        {
            if (parameters != null)
            {
                foreach (SqlParameter param in parameters)
                {
                    cmd.Parameters.Add(param);
                }
            }
            cmd.CommandType = CommandType.Text;
            Console.WriteLine($"Custom Mode ===>>> {Query}");
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                List<object> row = new List<object>();
                for (int i = 0; i < HowManyColums; i++)
                {
                    Console.WriteLine(reader[i]);
                    row.Add(reader[i]);
                }
                return_list.Add(row);
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
    public  int MakeCommandWithoutReturn(string Query,List<SqlParameter> parameters, SqlConnection conn_object)
    {
        SqlCommand cmd = new SqlCommand(Query, conn_object);
        cmd.CommandType = CommandType.Text;
        conn_object.Open();
        Console.WriteLine($"Query ===>>> {Query}");
        foreach (SqlParameter param in parameters)
        {
            cmd.Parameters.Add(param);
        }
        int result = cmd.ExecuteNonQuery();//for multiple crud operations

        conn_object.Close();
        Console.WriteLine($"Result ===>>> {result}");
        return result;    
    }
}