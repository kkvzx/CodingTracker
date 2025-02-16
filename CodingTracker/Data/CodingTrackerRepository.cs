using CodingTracker.model;
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

    public void Insert(CodingSession codingSession) => throw new NotImplementedException();

    public void Update(CodingSession codingSession) => throw new NotImplementedException();

    public void Delete(string id) => throw new NotImplementedException();

    public List<CodingSession> GetAll() => throw new NotImplementedException();
}
