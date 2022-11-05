using EtherpunkBlazorWasm.Server.Auth;
using EtherpunkBlazorWasm.Server.Data;
using EtherpunkBlazorWasm.Server.Data.Entities;
using EtherpunkBlazorWasm.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EtherpunkBlazorWasm.Server.Controllers;

[ApiController]
public class AuthController : ControllerBase
{
	private string CreateJWT(AppUser user)
	{
		var secretkey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(AppSettings.SecretKey));
		var credentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);

		List<Claim> claimList = new List<Claim>();
		claimList.Add(new Claim(ClaimTypes.Name, user.Email));
		claimList.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Email));
		claimList.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
		claimList.Add(new Claim(JwtRegisteredClaimNames.Jti, user.Email));
		foreach (var role in user.AppUserRoles.Select ( x=> x.Role))
		{
			claimList.Add(new Claim(ClaimTypes.Role, role.RoleName));
		}

		var token = new JwtSecurityToken(issuer: AppSettings.ValidIssuer, audience: AppSettings.ValidAudience, claims: claimList, expires: DateTime.Now.AddDays(AppSettings.TokenLengthInDays), signingCredentials: credentials);
		return new JwtSecurityTokenHandler().WriteToken(token);
	}

	private IUserDatabase userDb { get; set; }
	private EpunkDbContext dbContext { get; set; }

	public AuthController(IUserDatabase userDb, EpunkDbContext db)
	{
		this.userDb = userDb;
		this.dbContext = db;
	}

	[HttpPost, Route("api/auth/register")]
	public async Task<LoginResult> Register([FromBody] RegistrationModel reg)
	{
		if (string.IsNullOrWhiteSpace(reg.Email) || string.IsNullOrWhiteSpace(reg.Password))
		{
			string errorMessage = String.Empty;
			if (string.IsNullOrWhiteSpace(reg.Email))
				errorMessage += " Email address is empty;";
			if (string.IsNullOrWhiteSpace(reg.Password))
				errorMessage += " Password is empty;";
			return new LoginResult { Message = errorMessage, Success = false };
		}

		if (reg.Password != reg.ConfirmPassword)
			return new LoginResult { Message = "Password and confirm password do not match.", Success = false };

		AppUser? newUser = await userDb.AddUser(reg.Email, reg.Password);
		if (newUser != null)
			return new LoginResult { Message = "Registration successful.", JwtBearer = CreateJWT(newUser), Email = reg.Email, Success = true };
		return new LoginResult { Message = "User already exists.", Success = false };
	}

	[HttpPost, Route("api/auth/login")]
	public async Task<LoginResult> Login([FromBody] LoginModel login)
	{
		if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
		{
			string errorMessage = String.Empty;
			if (string.IsNullOrWhiteSpace(login.Email))
				errorMessage += " Email address is empty;";
			if (string.IsNullOrWhiteSpace(login.Password))
				errorMessage += " Password is empty;";
			return new LoginResult { Message = errorMessage, Success = false };
		}

		AppUser? user = await userDb.AuthenticateUser(login.Email, login.Password);
		if (user != null)
			return new LoginResult { Message = "Login successful.", JwtBearer = CreateJWT(user), Email = login.Email, Success = true };

		return new LoginResult { Message = "User/password not found.", Success = false };
	}

	/// <summary>
	/// Gets a list of all the roles and includes users in the list of each role.
	/// </summary>
	/// <returns>Returns List<RoleModel> of all roles.</returns>
	[HttpGet, Authorize(Roles = "Admin"), Route("api/auth/roleList")]
	public async Task<List<RoleModel>> GetRoleList()
	{
		var roles = dbContext.AppRoles
			.Include(x => x.Users)
			.Select(x => new RoleModel() {
				Id = x.Id,
				Name = x.RoleName,
				UserList = (List<RoleModel.User>)x.Users.Select(x => new RoleModel.User() { Id = x.UserId, Name = x.User.Email })
			});

        return await roles.ToListAsync();
	}

	/// <summary>
	/// Adds a user to the rule.
	/// </summary>
	/// <param name="userRole">User and role to create.</param>
	/// <returns>Returns an updated list of users for the role that the user was added to.</returns>
    [HttpPost, Authorize(Roles = "Admin"), Route("api/auth/addUserToRole")]
    public async Task<RoleModel?> AddUserToRole([FromBody] UserRole userRole)
    {
        dbContext.AppUserRoles.Add(new AppUserRole() { RoleId = userRole.RoleId, UserId = userRole.UserId });
        await dbContext.SaveChangesAsync();

        var newRoleUserList = dbContext.AppRoles
            .Include(x => x.Users)
            .Where(x => x.Id == userRole.RoleId)
            .Select(x => new RoleModel()
            {
                Id = x.Id,
                Name = x.RoleName,
                UserList = (List<RoleModel.User>)x.Users.Select(x => new RoleModel.User() { Id = x.UserId, Name = x.User.Email })
            });

        return await newRoleUserList.SingleOrDefaultAsync();
    }

	/// <summary>
	/// Delete a role.
	/// </summary>
	/// <param name="roleId">Id if the role to delete</param>
	/// <returns>True if role was successfully deleted. False otherwise.</returns>
    [HttpPost, Authorize(Roles = "Admin"), Route("api/auth/deleteRole")]
    public async Task<bool> DeleteRole([FromBody] Guid roleId)
    {
		try
		{
			var usersInRole = dbContext.AppUserRoles.Where(x => x.RoleId == roleId);
			dbContext.RemoveRange(usersInRole);
			dbContext.Remove(dbContext.AppRoles.Select(x => x.Id));

			await dbContext.SaveChangesAsync();

			return true;
		} catch
		{
			return false;
		}
    }

	/// <summary>
	/// Rename a given role.
	/// </summary>
	/// <param name="roleInfo">ID of role to change and new name to be given to role</param>
	/// <returns>Returns true if success, false if failure. Failure will more likely mean roleId doesn't exist.</returns>
    [HttpPost, Authorize(Roles = "Admin"), Route("api/auth/renameRole")]
    public async Task<bool> RenameRole([FromBody] RoleData roleInfo)
	{
		try
		{
			var role = dbContext.AppRoles.Where(x => x.Id == roleInfo.RoleId).Single();
			role.RoleName = roleInfo.Name;
			await dbContext.SaveChangesAsync();

			return true;
		} catch
		{
			return false;
		}
	}

    /// <summary>
    /// Creates a new role and return the Guid of the new role.
    /// </summary>
    /// <param name="roleName">Name of the role to create.</param>
    /// <returns>Returns the Guid of a new role. It should be all you need to add users quickly afterwards calling the API to get an updated list. Returns Guid.Empty if failure of any kind.</returns>
    //[HttpPost, Authorize(Roles = "Admin"), Route("api/auth/createRole")]
    [HttpPut, Route("api/auth/createRole")]
    public async Task<Guid> CreateNewRole([FromBody] string roleName)
	{
		try
		{
			AppRole role = new() { RoleName = roleName };
			dbContext.Add(role);
			await dbContext.SaveChangesAsync();
			return role.Id;
		} catch
		{
			// Odds are it's a duplicate name
			return Guid.Empty;
		}
	}
}
