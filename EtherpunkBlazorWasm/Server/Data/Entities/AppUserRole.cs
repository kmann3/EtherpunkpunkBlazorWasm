using System.ComponentModel.DataAnnotations;

namespace EtherpunkBlazorWasm.Server.Data.Entities;

public class AppUserRole
{
	[Required]
	public Guid RoleId { get; set; }
	public AppRole Role { get; set; }

	[Required]
	public Guid UserId { get; set; }
	public AppUser User { get; set; }

}
