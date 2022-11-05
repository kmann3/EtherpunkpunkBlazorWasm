using EtherpunkBlazorWasm.Shared;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using static EtherpunkBlazorWasm.Shared.RoleModel;
using static System.Net.WebRequestMethods;

namespace EtherpunkBlazorWasm.Client.Auth;

public static class ApiHandler
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
    public static async Task<ReturnData<T>> GetApiDataAsync<T>(string uri, IJSRuntime jsr, HttpClient http)
    {
        var returnData = new ReturnData<T>();
        try
        {
            var message = new HttpRequestMessage(HttpMethod.Get, uri);
            message.Headers.Add("Authorization", "Bearer " + await Jwt.GetJWT(jsr));
            returnData.HttpResponse = await http.SendAsync(message);
            if (!returnData.HttpResponse.IsSuccessStatusCode)
            {
                returnData.ErrorData = new();

                switch (returnData.HttpResponse.StatusCode)
                {
                    case System.Net.HttpStatusCode.Unauthorized:
                        await jsr.InvokeVoidAsync("localStorage.removeItem", "user").ConfigureAwait(false);
                        returnData.ErrorData.Message = "Unauthorized. Token expired or not logged in.";
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
                returnData.Data = await returnData.HttpResponse.Content.ReadFromJsonAsync<T?>();
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
    public static async Task<ReturnDataList<T>> GetApiDataListAsync<T>(string uri, IJSRuntime jsr, HttpClient http)
    {
        var returnData = new ReturnDataList<T>();
        try
        {
            var message = new HttpRequestMessage(HttpMethod.Get, uri);
            message.Headers.Add("Authorization", "Bearer " + await Jwt.GetJWT(jsr));
            returnData.HttpResponse = await http.SendAsync(message);
            if (!returnData.HttpResponse.IsSuccessStatusCode)
            {
                returnData.ErrorData = new();

                switch (returnData.HttpResponse.StatusCode)
                {
                    case System.Net.HttpStatusCode.Unauthorized:
                        await jsr.InvokeVoidAsync("localStorage.removeItem", "user").ConfigureAwait(false);
                        returnData.ErrorData.Message = "Unauthorized. Token expired or not logged in.";
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
                returnData.Data = await returnData.HttpResponse.Content.ReadFromJsonAsync<List<T>?>();
            }
        }
        catch (Exception ex)
        {
            // This will NOT catch errors in the API itself. Only exception thrown in this method.
            returnData.ErrorData = new ReturnDataList<T>.ErrorDetail()
            {
                Exception = ex
            };
        }

        return returnData;
    }

    public static async Task<ReturnDataList<T2>> PostApiDataListAsync<T1, T2>(string uri, T1 sendData, IJSRuntime jsr, HttpClient http)
    {
        var returnData = new ReturnDataList<T2>();
        try
        {
            var message = await http.PostAsJsonAsync<T1>(uri, sendData, CancellationToken.None);
            returnData.HttpResponse = message;
            if (!message.IsSuccessStatusCode)
            {
                returnData.ErrorData = new();

                if (returnData.HttpResponse == null)
                {
                    returnData.ErrorData.Message = message.StatusCode.ToString() + ";";
                    returnData.ErrorData.Message += message.ReasonPhrase + ";";
                    returnData.ErrorData.Message += message.Content + ";";
                }
                else
                {
                    returnData.ErrorData.Message = message.StatusCode.ToString() + ";";
                    switch (returnData.HttpResponse.StatusCode)
                    {
                        case System.Net.HttpStatusCode.Unauthorized:
                            await jsr.InvokeVoidAsync("localStorage.removeItem", "user").ConfigureAwait(false);
                            returnData.ErrorData.Message = "Unauthorized. Token expired or not logged in.";
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
                            returnData.ErrorData.Message = $"Unknown error: {returnData.HttpResponse.StatusCode}";
                            break;
                    }
                }
            }
            if (message.IsSuccessStatusCode && message.Content != null)
            {
                returnData.Data = await message.Content?.ReadFromJsonAsync<List<T2>?>();
            }
        }
        catch (Exception ex)
        {
            // This will NOT catch errors in the API itself. Only exception thrown in this method.
            returnData.ErrorData = new ReturnDataList<T2>.ErrorDetail()
            {
                Exception = ex
            };
        }

        return returnData;
    }
    public static async Task<ReturnData<T2>> PostApiDataAsync<T1, T2>(string uri, T1 sendData, IJSRuntime jsr, HttpClient http)
    {
        var returnData = new ReturnData<T2>();
        try
        {
            var message = await http.PostAsJsonAsync<T1>(uri, sendData, CancellationToken.None);
            returnData.HttpResponse = message;
            if (!message.IsSuccessStatusCode)
            {
                returnData.ErrorData = new();

                if (returnData.HttpResponse == null)
                {
                    returnData.ErrorData.Message = message.StatusCode.ToString() + ";";
                    returnData.ErrorData.Message += message.ReasonPhrase + ";";
                    returnData.ErrorData.Message += message.Content + ";";
                }
                else
                {
                    returnData.ErrorData.Message = message.StatusCode.ToString() + ";";
                    switch (returnData.HttpResponse.StatusCode)
                    {
                        case System.Net.HttpStatusCode.Unauthorized:
                            await jsr.InvokeVoidAsync("localStorage.removeItem", "user").ConfigureAwait(false);
                            returnData.ErrorData.Message = "Unauthorized. Token expired or not logged in.";
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
                            returnData.ErrorData.Message = $"Unknown error: {returnData.HttpResponse.StatusCode}";
                            break;
                    }
                }
            }
            if (message.IsSuccessStatusCode && message.Content != null)
            {
                returnData.Data = await message.Content?.ReadFromJsonAsync<T2?>();
            }
        }
        catch (Exception ex)
        {
            // This will NOT catch errors in the API itself. Only exception thrown in this method.
            returnData.ErrorData = new ReturnData<T2>.ErrorDetail()
            {
                Exception = ex
            };
        }

        return returnData;
    }
    public static async Task<ReturnDataList<T2>> PutApiDataListAsync<T1, T2>(string uri, T1 sendData, IJSRuntime jsr, HttpClient http)
    {
        var returnData = new ReturnDataList<T2>();
        try
        {
            var message = await http.PutAsJsonAsync<T1>(uri, sendData, CancellationToken.None);
            returnData.HttpResponse = message;
            if (!message.IsSuccessStatusCode)
            {
                returnData.ErrorData = new();

                if (returnData.HttpResponse == null)
                {
                    returnData.ErrorData.Message = message.StatusCode.ToString() + ";";
                    returnData.ErrorData.Message += message.ReasonPhrase + ";";
                    returnData.ErrorData.Message += message.Content + ";";
                }
                else
                {
                    returnData.ErrorData.Message = message.StatusCode.ToString() + ";";
                    switch (returnData.HttpResponse.StatusCode)
                    {
                        case System.Net.HttpStatusCode.Unauthorized:
                            await jsr.InvokeVoidAsync("localStorage.removeItem", "user").ConfigureAwait(false);
                            returnData.ErrorData.Message = "Unauthorized. Token expired or not logged in.";
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
                            returnData.ErrorData.Message = $"Unknown error: {returnData.HttpResponse.StatusCode}";
                            break;
                    }
                }
            }
            if (message.IsSuccessStatusCode && message.Content != null)
            {
                returnData.Data = await message.Content?.ReadFromJsonAsync<List<T2>?>();
            }
        }
        catch (Exception ex)
        {
            // This will NOT catch errors in the API itself. Only exception thrown in this method.
            returnData.ErrorData = new ReturnDataList<T2>.ErrorDetail()
            {
                Exception = ex
            };
        }

        return returnData;
    }
    public static async Task<ReturnData<T2>> PutApiDataAsync<T1, T2>(string uri, T1 sendData, IJSRuntime jsr, HttpClient http)
    {
        var returnData = new ReturnData<T2>();
        try
        {
            var message = await http.PutAsJsonAsync<T1>(uri, sendData, CancellationToken.None);
            returnData.HttpResponse = message;
            if (!message.IsSuccessStatusCode)
            {
                returnData.ErrorData = new();

                if (returnData.HttpResponse == null)
                {
                    returnData.ErrorData.Message = message.StatusCode.ToString() + ";";
                    returnData.ErrorData.Message += message.ReasonPhrase + ";";
                    returnData.ErrorData.Message += message.Content + ";";
                }
                else
                {
                    returnData.ErrorData.Message = message.StatusCode.ToString() + ";";
                    switch (returnData.HttpResponse.StatusCode)
                    {
                        case System.Net.HttpStatusCode.Unauthorized:
                            await jsr.InvokeVoidAsync("localStorage.removeItem", "user").ConfigureAwait(false);
                            returnData.ErrorData.Message = "Unauthorized. Token expired or not logged in.";
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
                            returnData.ErrorData.Message = $"Unknown error: {returnData.HttpResponse.StatusCode}";
                            break;
                    }
                }
            }else
            {
                returnData.ErrorData = null;
            }
            if (message.IsSuccessStatusCode && message.Content != null)
            {
                returnData.Data = await message.Content?.ReadFromJsonAsync<T2?>();
            }
        }
        catch (Exception ex)
        {
            // This will NOT catch errors in the API itself. Only exception thrown in this method.
            returnData.ErrorData = new ReturnData<T2>.ErrorDetail()
            {
                Exception = ex
            };
        }

        return returnData;
    }
}
