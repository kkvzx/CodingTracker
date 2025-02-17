using CodingTracker.Data;
using CodingTracker.model;
using CodingTracker.Views;

namespace CodingTracker.Presenters;

public class CodingSessionPresenter(ICodingSessionsView view, CodingTrackerRepository repository)
{
    private readonly CodingTrackerRepository _repository = repository;
    private readonly ICodingSessionsView _view = view;

    public void Run()
    {
        _repository.CreateTable();

        while (true)
        {
            Console.WriteLine("\n1. Add session \n2. Show all sessions\n3. Update session\n4. Delete session\n5. Exit");
            string? choice = _view.GetDateTime("Choose an option: ");
            switch (choice)
            {
                case "1": HandleAddCodingSession(); break;
                case "2": HandleShowAllSessionsSelect(); break;
                case "3": HandleUpdateSession(); break;
                case "4": HandleDeleteSession(); break;
                case "5": return;
                default: HandleInvalidChoice(); break;
            }
        }
    }

    private void HandleAddCodingSession()
    {
        _view.ShowMessage("Add entry");
        // string startDate = _view.GetDateTime("Enter start date time: ");
        // string endDate = _view.GetDateTime("Enter end date time: ");
        _repository.Insert(new CodingSession(
            10, "12-05-1999", "23-06-2025"
        ));
        _view.ShowMessage("User added successfully.");
    }

    private void HandleShowAllSessionsSelect() => _view.ShowCodingSessions(_repository.GetAll());

    private void HandleUpdateSession() => _view.ShowMessage("Update Session");

    // int id = int.Parse(_view.GetUserInput("Enter user ID to update: "));
    // var user = _users.Find(u => u.Id == id);
    // if (user != null)
    // {
    //     user.Name = _view.GetUserInput("Enter new name: ");
    //     _view.ShowMessage("User updated successfully.");
    // }
    // else
    // {
    //     _view.ShowMessage("User not found.");
    // }
    private void HandleDeleteSession() => _view.ShowMessage("Delete Session");
    // int id = int.Parse(_view.GetUserInput("Enter user ID to delete: "));
    // var user = _users.Find(u => u.Id == id);
    // if (user != null)
    // {
    //     _users.Remove(user);
    //     _view.ShowMessage("User deleted successfully.");
    // }
    // else
    // {
    //     _view.ShowMessage("User not found.");
    // }

    private void HandleInvalidChoice()
    {
        _view.ShowMessage("Invalid choice.");
        _view.PressKeyToContinue();
    }
}
