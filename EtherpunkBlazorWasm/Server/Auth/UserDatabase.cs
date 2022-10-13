using EtherpunkBlazorWasm.Server.Data;
using EtherpunkBlazorWasm.Server.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using BC = BCrypt.Net.BCrypt;
using EtherpunkBlazorWasm.Shared;

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
    public async Task<AppUser?> AuthenticateUser(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            return null;

        var dbContext = new EpunkDbContext();
        var appUser = await dbContext.AppUsers
                .Include(x => x.AppUserRoles)
                    .ThenInclude(x => x.Role)
            .Where(x => x.Email.ToLower() == email.ToLower())
            .Where(x => x.PasswordHash == CreateHash(password, "b", 12))
            .SingleOrDefaultAsync();

        return appUser;
    }
    public async Task<AppUser?> AddUser(string email, string password)
    {
        try
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

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
}
