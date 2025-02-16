using CodingTracker.Data;
using CodingTracker.model;
using CodingTracker.Views;

namespace CodingTracker.Presenters;

public class CodingSessionPresenter(ICodingSessionsView view)
{
    private readonly List<CodingSession> _codingSessions = new();
    private readonly ICodingSessionsView _view = view;

    public void Run()
    {
        CodingTrackerRepository repository = new();
        repository.CreateTable();

        while (true)
        {
            Console.WriteLine("\n1. Add session \n2. Show all sessions\n3. Update session\n4. Delete session\n5. Exit");
            string? choice = _view.GetUserInput("Choose an option: ");
            switch (choice)
            {
                case "1": AddCodingSession(); break;
                case "2": _view.ShowCodingSessions(_codingSessions); break;
                case "3": UpdateUser(); break;
                case "4": DeleteUser(); break;
                case "5": return;
                default: _view.ShowMessage("Invalid choice."); break;
            }
        }
    }

    private void AddCodingSession()
    {
        _view.ShowMessage("Add entry");
        string name = _view.GetUserInput("Enter user name: ");
        // _codingSessions.Add(new CodingSession( { Id = _nextId++, Name = name });
        _view.ShowMessage("User added successfully.");
    }

    private void UpdateUser() => _view.ShowMessage("Update user");

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
    private void DeleteUser() => _view.ShowMessage("Delete user");
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
}
