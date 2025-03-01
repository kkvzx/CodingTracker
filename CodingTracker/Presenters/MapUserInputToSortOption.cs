namespace CodingTracker.Presenters;

public static class MapUserInputToSortOption
{
    public static string Parse(char? userInput)
    {
        switch (userInput)
        {
            case 'i': return "Id";
            case 's': return "StartTime";
            case 'e': return "EndTime";
            default: return "Id";
        }
    }
}
