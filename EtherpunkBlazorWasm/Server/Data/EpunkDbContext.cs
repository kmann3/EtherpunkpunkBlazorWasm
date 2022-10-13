using Microsoft.EntityFrameworkCore;
using EtherpunkBlazorWasm.Server.Data.Entities;
using EtherpunkBlazorWasm.Server.Auth;

namespace EtherpunkBlazorWasm.Server.Data;

public class EpunkDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<AppUserRole>().HasKey(nameof(AppUserRole.RoleId), nameof(AppUserRole.UserId));

        // Uncommon the following if you want to add an admin user to view WeatherForecase admin area.
        //AppUser adminUser = new AppUser()
        //{
        //    CreatedOn = DateTime.UtcNow,
        //    Email = "admin@admin.com",
        //    Id = Guid.NewGuid(),
        //    PasswordHash = UserDatabase.CreateHash("password")
        //};

        //AppRole adminRole = new AppRole()
        //{
        //    Id = Guid.NewGuid(),
        //    Role = "Admin"
        //};

        //AppUserRole adminUserRoleLink = new AppUserRole
        //{
        //    RoleId = adminRole.Id,
        //    UserId = adminUser.Id,
        //};

        //builder.Entity<AppUser>().HasData(adminUser);
        //builder.Entity<AppRole>().HasData(adminRole);
        //builder.Entity<AppUserRole>().HasData(adminUserRoleLink);
    }

	protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"DataSource=EpunkSqliteDbName.sqlite3");
    }

	public DbSet<AppUserRole> AppUserRoles { get; set; }
	public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<AppRole> AppRoles { get; set; }
}
