using EtherpunkBlazorWasm.Server.Data;
using EtherpunkBlazorWasm.Server.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace EtherpunkBlazorWasm.Server.Auth;

public class UserDatabase : IUserDatabase
{
    private readonly IWebHostEnvironment env;
    public UserDatabase(IWebHostEnvironment env) => this.env = env;
    public static string CreateHash(string password)
    {
        var salt = "997eff51db1544c7a3c2ddeb2053f052";
        var md5 = new HMACMD5(Encoding.UTF8.GetBytes(salt + password));
        byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
        return System.Convert.ToBase64String(data);
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
            .Where(x => x.PasswordHash == CreateHash(password))
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
