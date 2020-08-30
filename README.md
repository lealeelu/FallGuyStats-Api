# FallGuyStats-Api
The Backend API for the Fall Guy Stats App written in ASP.NET C# with EFCore.
It watches the log file as you play for a DTO output in your player.log file, captures that DTO and adds it to the SQLite db.
Has endpoints for episodes and rounds.
Will be able to provide useful stats per round type while you play.

## Database
Run the migrations to add the sqlite db and generate data objects
`Update-database`

Db setup reference that actually worked for me.
I installed an easy sqlite db to use that is persistent.
https://docs.microsoft.com/en-us/ef/core/get-started/?tabs=visual-studio
