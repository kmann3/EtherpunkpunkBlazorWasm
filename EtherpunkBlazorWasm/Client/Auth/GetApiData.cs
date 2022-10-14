using EtherpunkBlazorWasm.Shared;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace EtherpunkBlazorWasm.Client.Auth;

public static class ApiHandler<T>
{
    public static async Task<ReturnData<T>> GetApiDataAsync(string uri, IJSRuntime jsr, HttpClient http)
    {
        ReturnData<T> returnData = new ReturnData<T>();
        try
        {
            var requestMsg = new HttpRequestMessage(HttpMethod.Get, uri);
            requestMsg.Headers.Add("Authorization", "Bearer " + await Jwt.GetJWT(jsr));
            returnData.HttpResponse = await http.SendAsync(requestMsg);
            if (!returnData.HttpResponse.IsSuccessStatusCode)
            {
                returnData.ErrorData = new();

                switch (returnData.HttpResponse.StatusCode)
                {
                    case System.Net.HttpStatusCode.Unauthorized:
                        await jsr.InvokeVoidAsync("localStorage.removeItem", "user").ConfigureAwait(false);
                        returnData.ErrorData.Message = "Unauthroized, token expired?";
                        break;
                    case System.Net.HttpStatusCode.Forbidden:
                        returnData.ErrorData.Message = "Not allowed to see this!";
                        break;
                    case System.Net.HttpStatusCode.NoContent:
                        break;
                    case System.Net.HttpStatusCode.InternalServerError:
                        returnData.ErrorData.Message = $"Internal Server Error: {returnData.HttpResponse.Content}";
                        break;
                    default:
                        break;
                }
            }
            if (returnData.HttpResponse.IsSuccessStatusCode)
            {
                returnData.Data = await returnData.HttpResponse.Content.ReadFromJsonAsync<List<T>>();
            }
        }
        catch (Exception ex)
        {
            returnData.ErrorData = new ReturnData<T>.ErrorDetail()
            {
                Exception = ex
            };
        }

        return returnData;
    }

    
}
