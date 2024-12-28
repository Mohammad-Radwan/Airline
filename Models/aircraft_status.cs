namespace Airline.Models;
using Microsoft.Extensions.Configuration;
using System;

public class aircraft_status
{
    IConfiguration configuration = new ConfigurationBuilder()
        .AddJsonFile("secret.json")
        .Build();

    public aircraft_status()
    {
        Console.WriteLine(configuration.GetSection("connstr").Value);
    }

    public int Id { get; set; }
    public string Status { get; set; }
    public DateTime LastUpdated { get; set; }
}

