using System.Globalization;
using Spectre.Console;

namespace CodingTracker.Views;

public static class DateTimeHelper
{
    private const string DateFormat = "yyyy/MM/dd";
    private const string TimeFormat = "HH:mm";
    public const string DateTimeFormat = "yyyy/MM/dd HH:mm";

    private static DateTime GetDateOnly()
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

    private static TimeSpan GetTimeOnly()
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

    private static DateTime GetDateTime(string message)
    {
        AnsiConsole.Markup($"{message}\n");
        DateTime dateTime = GetDateOnly() + GetTimeOnly();

        return dateTime;
    }

    public static Range GetDateRange(string? message = null)
    {
        if (message is not null)
        {
            AnsiConsole.Markup($"{message}\n");
        }

        var from = GetDateTime("Enter start date");
        var to = GetDateTime("Enter end date");

        while (to < from)
        {
            to = GetDateTime("[red]End date must be after start date[/]");
        }

        return new Range(from, to);
    }
}
