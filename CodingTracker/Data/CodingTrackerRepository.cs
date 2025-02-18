using CodingTracker.model;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace CodingTracker.Data;

public class CodingTrackerRepository
{
    private static readonly IConfigurationRoot s_config = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("appsettings.json", false, true)
        .Build();

    private static readonly string? s_connectionString = s_config.GetSection("ConnectionStrings:CodingTracker").Value;
    private static readonly string? s_tableName = s_config.GetSection("TableName").Value;


    public void CreateTable()
    {
        using (SqliteConnection connection = new(s_connectionString))
        {
            connection.Open();

            SqliteCommand tableCommand = connection.CreateCommand();
            tableCommand.CommandText =
                $"CREATE TABLE IF NOT EXISTS {s_tableName} (Id INTEGER PRIMARY KEY AUTOINCREMENT,StartTime TEXT,EndTime TEXT)";

            tableCommand.ExecuteNonQuery();

            connection.Close();
        }
    }

    public void Insert(CodingSession codingSession)
    {
        try
        {
            using (SqliteConnection connection = new(s_connectionString))
            {
                connection.Open();
                string command = $"INSERT INTO {s_tableName} (StartTime, EndTime) VALUES (@StartTime, @EndTime)";
                int rowsAffected = connection.Execute(command, codingSession);
                Console.WriteLine($"{rowsAffected} row(s) inserted.");

                connection.Close();
            }
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
        }
    }

    public void Update(CodingSession codingSession) => throw new NotImplementedException();

    public void Delete(string id) => throw new NotImplementedException();

    public List<CodingSession> GetAll()
    {
        try
        {
            using (SqliteConnection connection = new(s_connectionString))
            {
                connection.Open();

                string command = $"SELECT Id, StartTime, EndTime FROM {s_tableName}";
                var codingSessions = connection.Query<CodingSession>(command).ToList();

                connection.Close();

                return codingSessions;
            }
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
        }

        return new List<CodingSession>();
    }
}
