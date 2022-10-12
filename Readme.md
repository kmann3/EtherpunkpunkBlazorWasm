This is meant to be a JWT / Identity-like template.
Uses .Net 7. You'll need Visual Studio 2022. It should be easy enough to translate this to .Net 6 though.

How to install:
Drop to a command prompt. Go to the root directory of solution. `dotnet new install .`

How to uninstall:
Drop to a command prompt. Go to the root directory of solution. `dotnet new uninstall .`

To be implemented:

- Add optional https://github.com/natemcmaster/LettuceEncrypt  
- Optional variable / pages for Sqlite versus SQL Server versus Postgres
- Add Server-side logout / forced logout
- Confirm email / Resend / Update
- TOTP and/or Email 2FA (implemented via Interface so the authors can implement it on their own)
- User maintained sessions / ability to revoke sessions (this would be nice if you forgot to logout at a public machine like a library)

Notes for myself:
    ValidAudience = "EpunkAud";
    ValidIssuer = "EpunkIss";
    SecretKey = "EpunkSecretKey";

    EpunkCopyright
    Database / EpunkSqliteDbName
    appDbContextName / EpunkDbContext

    dotnet dev-certs https --trust