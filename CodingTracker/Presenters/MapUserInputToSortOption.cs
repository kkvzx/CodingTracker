namespace CodingTracker.Presenters;

public static class MapUserInputToSortOption
{
    public static string Parse(string? userInput)
    {
        switch (userInput)
        {
            case "Id": return "Id";
            case "Start Time": return "StartTime";
            case "End Time": return "EndTime";
            default: return "Id";
        }
    }
}
