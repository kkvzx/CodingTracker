using System;
using System.Collections.Generic;
using System.Linq;
using CodingTracker.Data;
using CodingTracker.model;
using CodingTracker.Views;

namespace CodingTracker.Presenters;

public class CodingSessionPresenter(CodingSessionsView view, CodingTrackerRepository repository)
{
    private DateTime? CurrentSessionStart { get; set; }

    public void Run()
    {
        repository.CreateTable();

        while (true)
        {
            int sessionCount = repository.GetRecordsCount();
            string selectedOption = view.ShowMenu(IsSessionInProgress, sessionCount);

            switch (selectedOption)
            {
                case "Start new session": HandleStartNewSession(); break;
                case "Stop running session": HandleStopSession(); break;
                case "Add session": HandleAddCodingSessionSelect(); break;
                case "Show all sessions": HandleShowAllSessionsSelect(); break;
                case "Update session": HandleUpdateSessionSelect(); break;
                case "Delete session": HandleDeleteSession(); break;
                case "Show report": HandleShowReport(); break;
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
            string? userInput = null;
            while (userInput is null || userInput != "Exit")
            {
                view.Clear();

                var codingSessions = repository.GetAll(MapUserInputToSortOption.Parse(userInput));

                view.ShowCodingSessions(codingSessions);
                userInput = view.ShowSortOptions();
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

                var currentSession = new CodingSession(
                    CurrentSessionStart.Value, DateTime.Now
                );
                repository.Insert(currentSession);
                view.ShowSuccess(
                    $"Session stopped and added successfully. Session lasted {Math.Round(currentSession.Duration, 2)} minutes");

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

    private void HandleShowReport()
    {
        var dateFormat = "yyyy-MM-dd HH:mm:ss";
        var reportRange = view.GetDateRange("Enter date range to generate report from");
        var durationSumInPeriod = repository.GetDurationSumInPeriodInMinutes(reportRange.From.ToString(dateFormat),
            reportRange.To.ToString(dateFormat));
        var durationTotalSum = repository.GetTotalDurationInMinutes();

        view.ShowReport(reportRange, durationSumInPeriod, durationTotalSum);
    }

    private void HandleInvalidChoice()
    {
        view.ShowError("Invalid choice.");
        view.PressKeyToContinue();
    }

    private bool IsSessionInProgress => CurrentSessionStart != null;
}
