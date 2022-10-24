using System.ComponentModel.DataAnnotations;

namespace EtherpunkBlazorWasm.Server.Data.Entities;

public class AppRole
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string RoleName { get; set; } = String.Empty;
    public ICollection<AppUserRole> Users { get; set; } = new List<AppUserRole>();
}
