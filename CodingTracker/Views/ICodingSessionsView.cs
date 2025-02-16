using CodingTracker.model;

namespace CodingTracker.Views;

public interface ICodingSessionsView
{
    void ShowCodingSessions(List<CodingSession> codingSessions);
    void ShowMessage(string message);
    string GetUserInput(string prompt);
}
