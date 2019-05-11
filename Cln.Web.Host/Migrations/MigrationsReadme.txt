https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/

Add-Migration InitialCreate (create a new migration)


Add-Migration {TitleOfTheUpdate} -Context TodoDbContext (update migrations based on updates to entity.)


Update-Database -Context TodoDbContext (update the database with the migration)