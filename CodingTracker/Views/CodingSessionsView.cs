using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CodingTracker.model;
using Spectre.Console;

namespace CodingTracker.Views;

public class CodingSessionsView
{
    public void ShowInfo(string message) => AnsiConsole.MarkupLine($"[blue]{message}[/]");

    public void ShowError(string message) => AnsiConsole.MarkupLine($"[red]{message}[/]");

    public void ShowSuccess(string message) => AnsiConsole.MarkupLine($"[green]{message}[/]");

    public string ShowMenu(bool isSessionInProgress, int sessionCount)
    {
        AnsiConsole.Write(new Rule("Welcome to [green]Coding Sessions tracker[/]"));
        string response = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .PageSize(10)
                .AddChoices(GetMenuOptions(isSessionInProgress, sessionCount)));

        return response;
    }

    public void ShowCodingSessions(List<CodingSession> codingSessions)
    {
        var table = new Table();
        table.AddColumns("Session ID", "Start Time", "End Time", "Duration (min)");

        foreach (CodingSession codingSession in codingSessions)
        {
            table.AddRow(codingSession.Id.ToString(), codingSession.StartTime.ToString(DateTimeHelper.DateTimeFormat),
                codingSession.EndTime.ToString(DateTimeHelper.DateTimeFormat),
                codingSession.Duration.ToString("F2")
            );
        }

        table.Columns[0].Alignment(Justify.Right);
        AnsiConsole.Write(table);
    }

    public string ShowSortOptions()
    {
        AnsiConsole.Write(new Rule("Sort by:"));
        string response = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .AddChoices(["Exit", "Id", "Start Time", "End Time"]));

        return response;
    }

    public void ShowReport(Range period, double durationSumInPeriod, double totalDuration) =>
        ReportView.Show(period, durationSumInPeriod, totalDuration);

    public Range GetDateRange(string? message = null) => DateTimeHelper.GetDateRange(message);

    public void PressKeyToContinue()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }

    public void Clear()
    {
        AnsiConsole.Clear();
    }

    public int GetExistingId(string message, List<int> sessionIds) => UserInputHandler.GetExistingId(message, sessionIds);

    private string[] GetMenuOptions(bool isSessionInProgress, int sessionCount)
    {
        string liveSessionOption = isSessionInProgress ? "Stop running session" : "Start new session";

        if (sessionCount == 0)
        {
            return [liveSessionOption, "Add session", "Exit"];
        }

        return
        [
            liveSessionOption, "Add session", "Show all sessions", "Update session", "Delete session",
            "Show report", "Exit"
        ];
    }
}
