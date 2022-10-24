using System.ComponentModel.DataAnnotations;

namespace EtherpunkBlazorWasm.Server.Data.Entities;

public class AppUser
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public string Email { get; set; } = String.Empty;
    [Required]
    public string PasswordHash { get; set; }
    [Required]
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public ICollection<AppUserRole> AppUserRoles { get; set; } = new List<AppUserRole>();
}
