using CodingTracker.Data;
using CodingTracker.Presenters;
using CodingTracker.Views;

CodingTrackerRepository repository = new();
CodingSessionsView codingSessionsView = new();
CodingSessionPresenter presenter = new(codingSessionsView, repository);

presenter.Run();
<
