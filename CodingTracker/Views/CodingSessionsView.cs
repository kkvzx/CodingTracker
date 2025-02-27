using System.Globalization;
using CodingTracker.model;
using Microsoft.VisualBasic;
using Spectre.Console;

namespace CodingTracker.Views;

public class CodingSessionsView
{
    private static readonly string DateFormat = "yyyy/MM/dd";
    private static readonly string TimeFormat = "HH:mm";
    private static readonly string DateTimeFormat = "yyyy/MM/dd HH:mm";

    public void ShowInfo(string message) => AnsiConsole.MarkupLine($"[blue]{message}[/]");

    public void ShowError(string message) => AnsiConsole.MarkupLine($"[red]{message}[/]");

    public void ShowSuccess(string message) => AnsiConsole.MarkupLine($"[green]{message}[/]");

    public string ShowMenu(bool isSessionInProgress)
    {
        var choices = isSessionInProgress
            ? new[]
            {
                "Stop running session", "Add session", "Show all sessions", "Update session", "Delete session",
                "Exit"
            }
            : new[]
            {
                "Start new session", "Add session", "Show all sessions", "Update session", "Delete session", "Exit"
            };

        string response = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Welcome to [green]Coding Sessions tracker[/]")
                .PageSize(10)
                .AddChoices(choices));

        return response;
    }

    public void ShowCodingSessions(List<CodingSession> codingSessions)
    {
        var table = new Table();
        table.AddColumns("Session ID", "Start Time", "End Time", "Duration (h)");

        foreach (CodingSession codingSession in codingSessions)
        {
            table.AddRow(codingSession.Id.ToString(), codingSession.StartTime.ToString(DateTimeFormat),
                codingSession.EndTime.ToString(DateTimeFormat),
                codingSession.Duration.ToString("F2")
            );
        }

        table.Columns[0].Alignment(Justify.Right);
        AnsiConsole.Write(table);
    }

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

    private int GetId(TextPrompt<string> prompt)
    {
        var userInput = AnsiConsole.Prompt(prompt);
        int id;

        while (!int.TryParse(userInput, out id))
        {
            userInput = AnsiConsole.Prompt(new TextPrompt<string>("[red]Invalid format, please try again![/]"));
        }

        return id;
    }

    public int GetExistingId(string message, List<int> sessionIds)
    {
        var inputId = GetId(new TextPrompt<string>(message));

        while (!sessionIds.Any(id => id == inputId))
        {
            inputId = GetId(new TextPrompt<string>("[red]There's no session with this id. Please try again: [/]"));
        }

        return inputId;
    }

    private DateTime GetDateOnly()
    {
        var userInput =
            AnsiConsole.Prompt(
                new TextPrompt<string>($"desired format {DateFormat}").DefaultValue(
                    DateTime.Now.ToString(DateFormat)));
        DateTime date;

        while (!DateTime.TryParseExact(userInput, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None,
                   out date) || date.Year < 1900)
        {
            userInput = AnsiConsole.Prompt(new TextPrompt<string>("[red]Invalid date format, please try again![/]"));
        }

        return date;
    }

    private TimeSpan GetTimeOnly()
    {
        var userInput =
            AnsiConsole.Prompt(
                new TextPrompt<string>($"desired format {TimeFormat}").DefaultValue(
                    DateTime.Now.ToString(TimeFormat)));
        TimeSpan time;

        while (!TimeSpan.TryParseExact(userInput, "hh\\:mm", CultureInfo.InvariantCulture, out time))
        {
            userInput = AnsiConsole.Prompt(new TextPrompt<string>("[red]Invalid time format, please try again![/]"));
        }

        return time;
    }

    private DateTime GetDateTime(string message)
    {
        AnsiConsole.Markup($"{message}\n");
        DateTime dateTime = GetDateOnly() + GetTimeOnly();

        return dateTime;
    }

    public Range GetDateRange()
    {
        var from = GetDateTime("Enter start date");
        var to = GetDateTime("Enter end date");

        while (to < from)
        {
            to = GetDateTime("[red]End date must be after start date[/]");
        }

        return new Range(from, to);
    }
}
