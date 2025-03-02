using Spectre.Console;

namespace CodingTracker.Views;

public static class ReportView
{
    public static void Show(Range period, double durationSumInPeriod, double totalDuration)
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[green]REPORT[/]"));
        AnsiConsole.Write(new Rows(
            new Markup(
                $"* Between [green]{period.From.ToString(DateTimeHelper.DateTimeFormat)}[/] and [green]{period.To.ToString(DateTimeHelper.DateTimeFormat)}[/] you spent [green]{durationSumInPeriod}[/] minutes coding ([green]{(Math.Round(durationSumInPeriod / 60, 2))}[/] hours)"),
            new Markup(
                $"* In total you spent [green]{totalDuration}[/] minutes coding ({Math.Round(totalDuration / 60, 2)} hours)")
        ));
        PressKeyToContinue();
    }

    private static void PressKeyToContinue()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }
}
