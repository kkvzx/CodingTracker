using CodingTracker.model;

namespace CodingTracker.Views;

public interface ICodingSessionsView
{
    void ShowCodingSessions(List<CodingSession> codingSessions);
    void ShowMessage(string message);
    void PressKeyToContinue();
    string GetDateTime(string prompt);
}
