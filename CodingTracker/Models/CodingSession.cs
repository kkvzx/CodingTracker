namespace CodingTracker.model;

public class CodingSession(string id, DateTime startDate, DateTime endDate)
{
    public string Id { get; set; } = id;
    public DateTime StartTime { get; set; } = startDate;
    public DateTime EndTime { get; set; } = endDate;

    public double Duration
    {
        get
        {
            TimeSpan duration = EndTime - StartTime;
            return duration.TotalHours;
        }
    }
}
