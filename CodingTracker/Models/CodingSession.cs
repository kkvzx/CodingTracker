namespace CodingTracker.model;

public class CodingSession
{
    public int Id { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }

    public CodingSession()
    {
        StartTime = "";
        EndTime = "";
    }

    public CodingSession(int id, string startDate, string endDate)
    {
        Id = id;
        StartTime = startDate;
        EndTime = endDate;
    }

    public CodingSession(string startDate, string endDate)
    {
        Id = 0;
        StartTime = startDate;
        EndTime = endDate;
    }
    public double Duration
    {
        get
        {
            TimeSpan duration = DateTime.Parse(EndTime) - DateTime.Parse(StartTime);
            return duration.TotalHours;
        }
    }
}
