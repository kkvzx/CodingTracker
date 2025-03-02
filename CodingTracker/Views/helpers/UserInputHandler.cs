using Spectre.Console;

namespace CodingTracker.Views;

public static class UserInputHandler
{
    private static int GetId(TextPrompt<string> prompt)
    {
        var userInput = AnsiConsole.Prompt(prompt);
        int id;

        while (!int.TryParse(userInput, out id))
        {
            userInput = AnsiConsole.Prompt(new TextPrompt<string>("[red]Invalid format, please try again![/]"));
        }

        return id;
    }

    public static int GetExistingId(string message, List<int> sessionIds)
    {
        var inputId = GetId(new TextPrompt<string>(message));

        while (!sessionIds.Any(id => id == inputId))
        {
            inputId = GetId(new TextPrompt<string>("[red]There's no session with this id. Please try again: [/]"));
        }

        return inputId;
    }}
