using CodingTracker.model;

namespace CodingTracker.Views;

public class CodingSessionsView : ICodingSessionsView
{
    public void ShowCodingSessions(List<CodingSession> codingSessions)
    {
        Console.WriteLine("\n          Coding Sessions");
        Console.WriteLine("------------------------------------");
        Console.WriteLine("Id\tStart Time\t\tEndTime\t\tDuration[h]");
        foreach (CodingSession codingSession in codingSessions)
        {
            Console.WriteLine(
                $"{codingSession.Id}\t{codingSession.StartTime}\t{codingSession.EndTime}\t{codingSession.Duration}");
        }
    }

    public void ShowMessage(string message) => Console.WriteLine(message);

    public void PressKeyToContinue()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }

    public string? GetDateTime(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }
}
