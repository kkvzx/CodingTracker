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

    public bool Insert(CodingSession codingSession)
    {
        using (SqliteConnection connection = new(s_connectionString))
        {
            connection.Open();

            string command = $"INSERT INTO {s_tableName} (StartTime, EndTime) VALUES (@StartTime, @EndTime)";
            int affectedRows = connection.Execute(command, codingSession);

            connection.Close();

            return affectedRows > 0;
        }
    }

    public bool Update(CodingSession codingSession)
    {
        using (SqliteConnection connection = new(s_connectionString))
        {
            connection.Open();

            string command = $"UPDATE {s_tableName} SET StartTime = @StartTime, EndTime = @EndTime WHERE Id = @Id";
            int affectedRows = connection.Execute(command, codingSession);

            connection.Close();

            return affectedRows > 0;
        }
    }

    public bool Delete(int id)
    {
        using (SqliteConnection connection = new(s_connectionString))
        {
            connection.Open();
            string command = $"DELETE FROM {s_tableName} WHERE Id = {id}";
            int affectedRows = connection.Execute(command);

            connection.Close();

            return affectedRows > 0;
        }
    }

    public CodingSession Get(string id)
    {
        using (SqliteConnection connection = new(s_connectionString))
        {
            connection.Open();

            var command = $"SELECT Id, StartTime, EndTime FROM {s_tableName} WHERE Id = @Id";
            var codingSession = connection.QuerySingle<CodingSession>(command, new { Id = id });

            connection.Close();

            return codingSession;
        }
    }

    public List<CodingSession> GetAll()
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
}
