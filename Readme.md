This is meant to be a JWT / Identity-like template.
Uses .Net 7. You'll need Visual Studio 2022. It should be easy enough to translate this to .Net 6 though.

How to install:
Drop to a command prompt. Go to the root directory of solution. `dotnet new install .`

How to uninstall:
Drop to a command prompt. Go to the root directory of solution. `dotnet new uninstall .`


Licenses:
- Bcrypt
	URL: https://github.com/BcryptNet/bcrypt.net
	License: MIT

To be implemented:

- Optional User Management + Ability to CRUD roles.
- 2FA: TOTP
- 2FA: Email
- Confirm email / Resend / Update
- Add optional https://github.com/natemcmaster/LettuceEncrypt  
- Optional variable / pages for Sqlite versus SQL Server versus Postgres
- Add Server-side logout / forced logout
- User maintained sessions / ability to revoke sessions (this would be nice if you forgot to logout at a public machine like a library)
- Is there anything that can be don about SCS00005 - Weak Number Generator? That's the only security issue the scanner has found thus far. Need to investigate further.