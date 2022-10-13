using EtherpunkBlazorWasm.Server.Data;
using EtherpunkBlazorWasm.Server.Data.Entities;
using EtherpunkBlazorWasm.Shared;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace EtherpunkBlazorWasm.Server.Auth;

public class UserDatabase : IUserDatabase
{
    private readonly IWebHostEnvironment env;
    public UserDatabase(IWebHostEnvironment env) => this.env = env;

    /// <summary>
    /// Creates a hash of a given password and returns a string.
    /// </summary>
    /// <param name="password"></param>
    /// <param name="version">Version used for the hash. Default is 'b'. More information can be found at: https://github.com/BcryptNet/bcrypt.net#blowfish-based-scheme---versioningbcrypt-revisions </param>
    /// <param name="workFactor">How much work to put into it. Default is 12. It is strongly advised not to go lower than 10. More information can be found here: https://github.com/BcryptNet/bcrypt.net   -- Note: Scroll down to benchmarks</param>
    /// <remarks>This method is made public specifically so you can use it to seed your database.</remarks>
    /// <returns>Hash as string</returns>
    public static string CreateHash(string password, string version = "b", int workFactor = 12)
    {
        var passwordHash = BC.HashPassword(password, $"$2{version}${workFactor}${AppSettings.PasswordSalt}");
        return passwordHash;
    }

    /// <summary>
    /// Validates a user and their password.
    /// </summary>
    /// <param name="email">Email address to lookup</param>
    /// <param name="password">Password to be hashed and compared against the database.</param>
    /// <returns>Returns either the user and their roles or null if match not found.</returns>
    public async Task<AppUser?> AuthenticateUser(string email, string password)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(email);
        ArgumentNullException.ThrowIfNullOrEmpty(password);

        var dbContext = new EpunkDbContext();
        var appUser = await dbContext.AppUsers
                .Include(x => x.AppUserRoles)
                    .ThenInclude(x => x.Role)
            .Where(x => x.Email.ToLower() == email.ToLower())
            .Where(x => x.PasswordHash == CreateHash(password, "b", 12))
            .SingleOrDefaultAsync();

        return appUser;
    }
    /// <summary>
    /// Adds a user to the database.
    /// </summary>
    /// <param name="email">Email address to use. This is effectively their username</param>
    /// <param name="password">Password to use. This is hashed. The plaintext is NOT stored.</param>
    /// <returns>Returns the user that is created. This should contain their Id as well.</returns>
    public async Task<AppUser?> AddUser(string email, string password)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(email);
        ArgumentNullException.ThrowIfNullOrEmpty(password);

        try
        {
            var dbContext = new EpunkDbContext();
            AppUser newUser = new AppUser()
            {
                CreatedOn = DateTime.UtcNow,
                Email = email.ToLower(),
                PasswordHash = CreateHash(password),
                Id = Guid.NewGuid()
            };
            dbContext.AppUsers.Add(newUser);
            await dbContext.SaveChangesAsync();
            return newUser;
        }
        catch
        {
            return null;
        }
    }

    public Task<AppRole?> AddRole(string roleName)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(roleName);
        try
        {

        }
        catch
        {

        }
        throw new NotImplementedException();
    }

    public Task LogUserToken(string token)
    {
        throw new NotImplementedException();
    }
}
