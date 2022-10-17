using EtherpunkBlazorWasm.Shared;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace EtherpunkBlazorWasm.Client.Auth;

public static class ApiHandler<T>
{
    /// <summary>
    /// Gets data from an API.
    /// Check HttpResponse.IsSuccessStatusCode to see if you have data to deserialize or if there was a problem.
    /// Important HttpResponse.StatusCode's:
    ///     HttpStatusCode.Unauthorized         : User has not been authenticated. Could mean token expired.
    ///     System.Net.HttpStatusCode.Forbidden : Access is denied for this user.
    ///     HttpStatusCode.InternalServerError  : Error has occured. See: HttpResponse.Content for why.
    /// </summary>
    /// <param name="uri">The URI of the API. e.g. /api/auth/getAllRoles</param>
    /// <param name="jsr">No idea what this is.</param>
    /// <param name="http">Client control?</param>
    /// <returns></returns>
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
                        returnData.ErrorData.Message = "Unauthroized. Token expired or not logged in.";
                        break;
                    case System.Net.HttpStatusCode.Forbidden:
                        returnData.ErrorData.Message = "Not allowed to see this!";
                        break;
                    case System.Net.HttpStatusCode.NoContent:
                        break;
                    case System.Net.HttpStatusCode.InternalServerError:
                        returnData.ErrorData.Message = "Internal Server Error. Probably an exception was thrown in the API.";
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
            // This will NOT catch errors in the API itself. Only exception thrown in this method.
            returnData.ErrorData = new ReturnData<T>.ErrorDetail()
            {
                Exception = ex
            };
        }

        return returnData;
    }

    
}
