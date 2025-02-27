using CodingTracker.Data;
using CodingTracker.model;
using CodingTracker.Views;
using Range = CodingTracker.Views.Range;

namespace CodingTracker.Presenters;

public class CodingSessionPresenter(CodingSessionsView view, CodingTrackerRepository repository)
{
    private DateTime? CurrentSessionStart { get; set; }

    public void Run()
    {
        repository.CreateTable();

        while (true)
        {
            string selectedOption = view.ShowMenu(IsSessionInProgress);

            switch (selectedOption)
            {
                case "Start new session": HandleStartNewSession(); break;
                case "Stop running session": HandleStopSession(); break;
                case "Add session": HandleAddCodingSessionSelect(); break;
                case "Show all sessions": HandleShowAllSessionsSelect(); break;
                case "Update session": HandleUpdateSessionSelect(); break;
                case "Delete session": HandleDeleteSession(); break;
                case "Exit": return;
                default: HandleInvalidChoice(); break;
            }
        }
    }

    private void HandleAddCodingSessionSelect()
    {
        try
        {
            view.Clear();

            var dateRange = view.GetDateRange();
            repository.Insert(new CodingSession(
                dateRange.From, dateRange.To
            ));

            view.ShowSuccess("Session added successfully.");
        }
        catch (Exception error)
        {
            view.ShowError(error.Message);
        }

        view.PressKeyToContinue();
    }

    private void HandleShowAllSessionsSelect()
    {
        try
        {
            view.Clear();

            var codingSessions = repository.GetAll();

            if (SessionsCountGate(codingSessions))
            {
                view.ShowCodingSessions(codingSessions);
            }
        }
        catch (Exception error)
        {
            view.ShowError(error.Message);
        }

        view.PressKeyToContinue();
    }

    private void HandleUpdateSessionSelect()
    {
        try
        {
            view.Clear();

            var codingSessions = repository.GetAll();

            if (SessionsCountGate(codingSessions))
            {
                view.ShowCodingSessions(codingSessions);

                var id = view.GetExistingId("Enter id of session to update: ",
                    codingSessions.Select(c => c.Id).ToList());
                var dateRange = view.GetDateRange();

                var isSuccess = repository.Update(new CodingSession(id, dateRange.From, dateRange.To));
                if (isSuccess)
                {
                    view.ShowSuccess("Session updated successfully.");
                }
                else
                {
                    view.ShowError($"Failed to update session with '{id}' id.");
                }
            }
        }
        catch (Exception error)
        {
            view.ShowError(error.Message);
        }

        view.PressKeyToContinue();
    }

    private void HandleDeleteSession()
    {
        try
        {
            view.Clear();

            List<CodingSession> codingSessions = repository.GetAll();

            if (SessionsCountGate(codingSessions))
            {
                view.ShowCodingSessions(codingSessions);

                int idToDelete =
                    view.GetExistingId("Enter session Id to delete: ", codingSessions.Select(c => c.Id).ToList());
                bool isSuccess = repository.Delete(idToDelete);

                if (isSuccess)
                {
                    view.ShowSuccess("Session deleted successfully.");
                }
                else
                {
                    view.ShowError($"There is no record with '{idToDelete}' id");
                }
            }
        }
        catch (Exception error)
        {
            view.ShowError(error.Message);
        }

        view.PressKeyToContinue();
    }

    private void HandleStartNewSession()
    {
        if (CurrentSessionStart == null)
        {
            view.Clear();
            CurrentSessionStart = DateTime.Now;
            view.ShowInfo("Started new session...");
        }
        else
        {
            view.ShowError("Session already started. Stop current session in order to start new.");
        }
        view.PressKeyToContinue();
    }

    private void HandleStopSession()
    {
        if (CurrentSessionStart != null)
        {
            try
            {
                view.Clear();

                repository.Insert(new CodingSession(
                    CurrentSessionStart.Value, DateTime.Now
                ));

                view.ShowSuccess($"Session stopped and added successfully.");

                CurrentSessionStart = null;
            }
            catch (Exception error)
            {
                view.ShowError(error.Message);
            }
            view.PressKeyToContinue();
        }
        else
        {
            view.ShowError("No session started. Start new session first.");
        }
    }

    private bool SessionsCountGate(List<CodingSession> codingSessions)
    {
        if (codingSessions.Count == 0)
        {
            view.ShowInfo("There's isn't any sessions yet :(");
            view.ShowInfo("Add sessions first in order to continue.");

            return false;
        }

        return true;
    }

    private void HandleInvalidChoice()
    {
        view.ShowError("Invalid choice.");
        view.PressKeyToContinue();
    }

    private bool IsSessionInProgress
    {
        get
        {
            return CurrentSessionStart is not null;
        }
    }
}
