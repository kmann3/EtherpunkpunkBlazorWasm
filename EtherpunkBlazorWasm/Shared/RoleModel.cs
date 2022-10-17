using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherpunkBlazorWasm.Shared;
public class RoleModel
{
	public Guid Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public List<User> UserList { get; set; } = new();

	public class User
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = string.Empty;
	}
}
