using System.Globalization;
using CodingTracker.model;
using Microsoft.VisualBasic;

namespace CodingTracker.Views;

public class CodingSessionsView
{
    public readonly string DateFormat = "yyyy/MM/dd HH:mm";

    public void ShowMenu()
    {
        Console.WriteLine("\n1. Add session \n2. Show all sessions\n3. Update session\n4. Delete session\n5. Exit");
    }

    public void ShowCodingSessions(List<CodingSession> codingSessions)
    {
        Console.WriteLine("\n          Coding Sessions");
        Console.WriteLine("------------------------------------");
        Console.WriteLine("Id\tStart Time\t\tEndTime\t\tDuration[h]");
        foreach (CodingSession codingSession in codingSessions)
        {
            Console.WriteLine(
                $"{codingSession.Id}\t{codingSession.StartTime.ToString(DateFormat)}\t{codingSession.EndTime.ToString(DateFormat)}\t{codingSession.Duration:F2}");
        }
    }

    public void ShowMessage(string message) => Console.WriteLine(message);

    public void PressKeyToContinue()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }

    public void Clear()
    {
        Console.Clear();
    }

    public string? GetString(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }

    public int GetId(string prompt)
    {
        ShowMessage(prompt);
        string? userInput = Console.ReadLine();
        int id;

        while (userInput is null || !int.TryParse(userInput, out id))
        {
            ShowMessage("Invalid input, please try again: ");
            userInput = Console.ReadLine();
        }

        return id;
    }

    public int GetExistingId(string prompt, List<int> sessionIds)
    {
        var inputId = GetId(prompt);

        while (!sessionIds.Any(id => id == inputId))
        {
            inputId = GetId("There's no session with this id. Please try again: ");
        }

        return inputId;
    }

    public DateTime GetDateTime(string prompt)
    {
        Console.Write(prompt);
        string? userInput = Console.ReadLine();
        DateTime dateTime;

        while (userInput is null ||
               !DateTime.TryParseExact(userInput, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None,
                   out dateTime) || dateTime.Year < 1900)
        {
            ShowMessage("Invalid format, please try again: ");
            userInput = Console.ReadLine();
        }

        return dateTime;
    }
}
