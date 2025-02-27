# Coding Tracker

### Requirements
- [x] Should have menu  created with "Spectre Console"
- [x] Should perform CRUD operations
- [x] Each class is supposed to be in separate file
- [x] Configuration file with `connectionStrings` and database path
- [x] `CodingSession` class with `Id`, `StartTime`, `EndTime`, `Duration`
- [x] `Duration` should be calculated based on `StartTime` and `EndTime`
- [x] `Dapper ORM` should be used for the data access.

### Challenges
- Correct architecture and file structure.
I had to research basic application architecture to be able to create maintainable and easy to review code.\ I came across **MVC** pattern that is used in web development.
From **MVC** another pattern for Console/WinForm application emerged which is **MVP**
- I learned on my own skin how important is to change working directory of the project in context of DB creation.
- Single responsibility, I figured out that data layer shouldn't handle errors but they should be bubbled up to presenter layer.
- I found out how useful tool is **Spectre Console** during development of console application.

### Extra
- [x] Add the possibility of tracking the coding time via a stopwatch so the user can track the session as it happens.
- [ ] Create reports where the users can see their total and average coding session per period.
- [ ] Let the users filter their coding records per period (weeks, days, years) and/or order ascending or descending.
- [ ] Create the ability to set coding goals and show how far the users are from reaching their goal, along with how many hours a day they would have to code to reach their goal. You can do it via SQL queries or with C#.
