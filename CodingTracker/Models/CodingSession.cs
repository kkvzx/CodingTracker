namespace CodingTracker.model;

public class CodingSession
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public CodingSession()
    {
        StartTime = new DateTime();
        EndTime = new DateTime();
    }

    public CodingSession(int id, DateTime startDate, DateTime endDate)
    {
        Id = id;
        StartTime = startDate;
        EndTime = endDate;
    }

    public CodingSession(DateTime startDate, DateTime endDate)
    {
        Id = 0;
        StartTime = startDate;
        EndTime = endDate;
    }

    public double Duration
    {
        get
        {
            TimeSpan duration = EndTime - StartTime;
            return duration.TotalHours;
        }
    }
}
