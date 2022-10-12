This is meant to be a JWT / Identity-like template.
Uses .Net 7. You'll need Visual Studio 2022. It should be easy enough to translate this to .Net 6 though.

How to install:
Drop to a command prompt. Go to the root directory of solution. `dotnet new install .`

How to uninstall:
Drop to a command prompt. Go to the root directory of solution. `dotnet new uninstall .`


- Add optional https://github.com/natemcmaster/LettuceEncrypt  
- Optional variable / pages for Sqlite versus SQL Server versus Postgres
- Add Server-side logout / forced logout

Notes for myself:
    ValidAudience = "EpunkAud";
    ValidIssuer = "EpunkIss";
    SecretKey = "EpunkSecretKey";

    EpunkCopyright
    Database / EpunkSqliteDbName
    appDbContextName / EpunkDbContext

    dotnet dev-certs https --trust