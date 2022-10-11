using EtherpunkBlazorWasm.Server.Data.Entities;

namespace EtherpunkBlazorWasm.Server.Auth;

public interface IUserDatabase
{
    Task<AppUser?> AuthenticateUser(string email, string password);
    Task<AppUser?> AddUser(string email, string password);
}
