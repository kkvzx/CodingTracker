namespace CodingTracker.model;

public class CodingSession(int id, string startDate, string endDate)
{
    // public CodingSession(string startDate, string endDate)
    // {
    //     Id = Guid.NewGuid().ToString();
    //     StartTime = startDate;
    //     EndTime = endDate;
    // }


    public string Id { get; set; } = id.ToString();
    public string StartTime { get; set; } = startDate;

    public string EndTime { get; set; } = endDate;
    // public double Duration
    // {
    //     get
    //     {
    //         TimeSpan duration = EndTime - StartTime;
    //         return duration.TotalHours;
    //     }
    // }
}
