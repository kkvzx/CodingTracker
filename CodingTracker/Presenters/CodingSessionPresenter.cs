using CodingTracker.Data;
using CodingTracker.model;
using CodingTracker.Views;

namespace CodingTracker.Presenters;

public class CodingSessionPresenter(CodingSessionsView view, CodingTrackerRepository repository)
{
    public void Run()
    {
        repository.CreateTable();

        while (true)
        {
            view.ShowMenu();

            string? choice = view.GetString("Choose an option: ");
            switch (choice)
            {
                case "1": HandleAddCodingSessionSelect(); break;
                case "2": HandleShowAllSessionsSelect(); break;
                case "3": HandleUpdateSessionSelect(); break;
                case "4": HandleDeleteSession(); break;
                case "5": return;
                default: HandleInvalidChoice(); break;
            }
        }
    }

    private void HandleAddCodingSessionSelect()
    {
        try
        {
            view.Clear();

            var startTime = view.GetDateTime("Enter start date time in format (yyyy-mm-dd HH:mm:ss): ");
            var endTime = view.GetDateTime("Enter end date time in format (yyyy-mm-dd HH:mm:ss): ");

            repository.Insert(new CodingSession(
                startTime, endTime
            ));

            view.ShowMessage("User added successfully.");
        }
        catch (Exception error)
        {
            view.ShowMessage(error.Message);
        }

        view.PressKeyToContinue();
    }

    private void HandleShowAllSessionsSelect()
    {
        try
        {
            view.Clear();

            var codingSessions = repository.GetAll();

            view.ShowCodingSessions(codingSessions);
        }
        catch (Exception error)
        {
            view.ShowMessage(error.Message);
        }

        view.PressKeyToContinue();
    }

    private void HandleUpdateSessionSelect()
    {
        try
        {
            view.Clear();

            var codingSessions = repository.GetAll();

            view.ShowCodingSessions(codingSessions);

            var id = view.GetExistingId("Enter id of session to update: ", codingSessions.Select(c => c.Id).ToList());
            var startTime = view.GetDateTime("Enter start date time in format (yyyy-mm-dd HH:mm:ss): ");
            var endTime = view.GetDateTime("Enter end date time in format (yyyy-mm-dd HH:mm:ss): ");

            var isSuccess = repository.Update(new CodingSession(id, startTime, endTime));
            if (isSuccess)
            {
                view.ShowMessage("Session updated successfully.");
            }
            else
            {
                view.ShowMessage($"Failed to update session with '{id}' id.");
            }
        }
        catch (Exception error)
        {
            view.ShowMessage(error.Message);
        }

        view.PressKeyToContinue();
    }

    private void HandleDeleteSession()
    {
        try
        {
            view.Clear();

            var codingSessions = repository.GetAll();

            view.ShowCodingSessions(codingSessions);

            var idToDelete =
                view.GetExistingId("Enter session Id to delete: ", codingSessions.Select(c => c.Id).ToList());
            var isSuccess = repository.Delete(idToDelete);

            if (isSuccess)
            {
                view.ShowMessage("Session deleted successfully.");
            }
            else
            {
                view.ShowMessage($"There is no record with '{idToDelete}' id");
            }
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
        }

        view.PressKeyToContinue();
    }

    private void HandleInvalidChoice()
    {
        view.ShowMessage("Invalid choice.");
        view.PressKeyToContinue();
    }
}
