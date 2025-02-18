using CodingTracker.Data;
using CodingTracker.Presenters;
using CodingTracker.Views;

/*
 * - Configuration file,
 * - model,
 * - database/table creation,
 * - CRUD controller (where operations will happen)
 * - Table visualization engine,
 * - Validation of data
 *
 *
 * MVP - Model View Presenter
 * Model - Odpowiada za przechowywanie danych i zasad biznesowych (Data
 * View - Interfejs przsez który użytkownik wchodzi w interakcje
 * Presenter - Służy jako most między Model i View, manipulując danymi pochodzącymi z Modelu, przekazując je do View
 */

/*  TODO
 *  1. Naprawiłęm wyświetlanie. Teraz trzeba może obsłużyć prawidłowy typ danych i dorobić pozostałe operacje?
 *
 * ref https://thecsharpacademy.com/project/13/coding-tracker
 */
CodingTrackerRepository repository = new();
CodingSessionsView codingSessionsView = new();
CodingSessionPresenter presenter = new(codingSessionsView, repository);

presenter.Run();
