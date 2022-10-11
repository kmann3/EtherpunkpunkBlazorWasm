using System.ComponentModel.DataAnnotations;

namespace EtherpunkBlazorWasm.Server.Data.Entities;

public class AppRole
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Role { get; set; } = string.Empty;
    public ICollection<AppUserRole> Users { get; set; } = new List<AppUserRole>();
}
