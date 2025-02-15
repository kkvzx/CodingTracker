using System.Collections.Specialized;
using Microsoft.Extensions.Configuration;
using ConfigurationManager = System.Configuration.ConfigurationManager;

/*
 * - Configuration file,
 * - model,
 * - database/table creation,
 * - CRUD controller (where operations will happen)
 * - Table visualization engine,
 * - Validation of data
 */


// Load configuration from appsettings.json
var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Get the connection string
string? connectionString = config.GetSection("ConnectionStrings:DefaultConnection").Value;

Console.WriteLine("Connection String: " + connectionString);
