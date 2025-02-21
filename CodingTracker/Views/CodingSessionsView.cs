using System.Globalization;
using CodingTracker.model;
using Microsoft.VisualBasic;
using Spectre.Console;

namespace CodingTracker.Views;

public class CodingSessionsView
{
    public readonly string DateFormat = "yyyy/MM/dd HH:mm";

    public string ShowMenu()
    {
        var response = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Welcome to [green]Coding Sessions tracker[/]")
                .PageSize(5)
                .AddChoices(new[] { "Add session", "Show all sessions", "Update session", "Delete session", "Exit" }));

        return response;
    }

    public void ShowCodingSessions(List<CodingSession> codingSessions)
    {
        var table = new Table();
        table.AddColumns("Session ID", "Start Time", "End Time", "Duration");

        foreach (CodingSession codingSession in codingSessions)
        {
            table.AddRow(codingSession.Id.ToString(), codingSession.StartTime.ToString(DateFormat),
                codingSession.EndTime.ToString(DateFormat),
                codingSession.Duration.ToString("F2")
            );
        }

        table.Columns[0].Alignment(Justify.Right);
        AnsiConsole.Write(table);
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
