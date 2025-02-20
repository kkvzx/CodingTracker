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
 *  1. Zająć się tableVisualizationEngine
 */
CodingTrackerRepository repository = new();
CodingSessionsView codingSessionsView = new();
CodingSessionPresenter presenter = new(codingSessionsView, repository);

presenter.Run();
