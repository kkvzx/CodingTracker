using System.Globalization;
using CodingTracker.model;
using Microsoft.VisualBasic;
using Spectre.Console;

namespace CodingTracker.Views;

public class CodingSessionsView
{
    public readonly string DateFormat = "yyyy/MM/dd HH:mm";

    public void ShowInfo(string message) => AnsiConsole.MarkupLine($"[blue]{message}[/]");

    public void ShowError(string message) => AnsiConsole.MarkupLine($"[red]{message}[/]");

    public void ShowSuccess(string message) => AnsiConsole.MarkupLine($"[green]{message}[/]");

    public string ShowMenu()
    {
        string response = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Welcome to [green]Coding Sessions tracker[/]")
                .PageSize(5)
                .AddChoices("Add session", "Show all sessions", "Update session", "Delete session", "Exit"));

        return response;
    }

    public void ShowCodingSessions(List<CodingSession> codingSessions)
    {
        var table = new Table();
        table.AddColumns("Session ID", "Start Time", "End Time", "Duration (h)");

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

    private DateTime GetDateTime(TextPrompt<string> prompt)
    {
        var userInput = AnsiConsole.Prompt(prompt);
        DateTime dateTime;

        while (!DateTime.TryParseExact(userInput, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None,
                   out dateTime) || dateTime.Year < 1900)
        {
            userInput = AnsiConsole.Prompt(new TextPrompt<string>("[red]Invalid format, please try again![/]"));
        }

        return dateTime;
    }

    public Range GetDateRange()
    {
        var from = GetDateTime(
            new TextPrompt<string>($"Enter start date (format ({DateFormat}))").DefaultValue(
                DateTime.Now.ToString(DateFormat)));
        var to = GetDateTime(
            new TextPrompt<string>($"Enter end date (format ({DateFormat}))").DefaultValue(
                DateTime.Now.ToString(DateFormat)));

        while (to < from)
        {
            to = GetDateTime(new TextPrompt<string>("[red]End date must be after start date: [/]"));
        }

        return new Range(from, to);
    }
}
