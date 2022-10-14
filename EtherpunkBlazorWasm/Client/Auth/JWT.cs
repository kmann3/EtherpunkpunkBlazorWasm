using Microsoft.JSInterop;

namespace EtherpunkBlazorWasm.Client.Auth;

public static class Jwt
{
	public static async Task<string> GetJWT(IJSRuntime jsr)
	{
		string userData = await jsr.InvokeAsync<string>("localStorage.getItem", "user").ConfigureAwait(false);
		if (!string.IsNullOrEmpty(userData))
		{
			var dataArray = userData.Split(';', 2);
			if (dataArray.Length == 2)
				return dataArray[1];
		}

		return string.Empty;
	}
}
