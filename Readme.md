This is meant to be a JWT / Identity template.
Uses .Net 7. You'll need Visual Studio 2022. It should be easy enough to translate this to .Net 6 though.

- Add optional https://github.com/natemcmaster/LettuceEncrypt  
- Optional variable / pages for Sqlite versus SQL Server versus Postgres


Notes for myself:
    ValidAudience = "EpunkAud";
    ValidIssuer = "EpunkIss";
    SecretKey = "EpunkSecretKey";

    EpunkCopyright
    Database / EpunkSqliteDbName
    appDbContextName / EpunkDbContext

    dotnet dev-certs https --trust