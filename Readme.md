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

- WORKING ON NOW IN UserMaintenance BRANCH: Optional User Management + Ability to CRUD roles.
- Figure out something for the Wasm.Client to pass in to check for auth so not repeating GetJWT() methods over and over. Is there any reason NOT to just toss the token in on every page or embed it into App.Razor somehow? Or somewhere generic? Perhaps AuthLayout component inside Wasm.Client/Shared/MainLayout.razor?
- Figure out Generic Get/Post for Wasm.Client so all that's needed is: Task<(bool isSuccessStatusCode, System.Net.HttpStatusCode? errorCode, string? errorDetail, List<T>? data)> DoThing(string uri)
	isSuccessStatusCode = makes it easier to know if we need to ignore errorCode+errorDetail
	errorCode = used to cypher out if it's unauthorized or some other error (e.g. bad uri?)
	errorCode = Can be used to send exceptions for display?
	data = Data requested
- 2FA: TOTP
- 2FA: Email
- Confirm email / Resend / Update
- Add optional https://github.com/natemcmaster/LettuceEncrypt  - this is less useful for playful projects where you don't have a domain name though.
- Optional variable / pages for Sqlite versus SQL Server versus Postgres - should consider noSQL stuffs too?
- Add Server-side logout / forced logout
- User maintained sessions / ability to revoke sessions (this would be nice if you forgot to logout at a public machine like a library)
- Is there anything that can be don about SCS00005 - Weak Number Generator? That's the only security issue the scanner has found thus far. Need to investigate further.

Small things:
- Go through warnings and messages. Tweak things like extra lines, sort usings.