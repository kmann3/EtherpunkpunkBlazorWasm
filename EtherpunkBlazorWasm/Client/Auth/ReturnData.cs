namespace EtherpunkBlazorWasm.Client.Auth;

public class ReturnData<T>
{
    public ErrorDetail? ErrorData { get; set; }
    public List<T> Data { get; set; } = new List<T>();

    public HttpResponseMessage? HttpResponse { get; set; }
    public System.Net.HttpStatusCode HttpResponseCode { get; set; }

    public class ErrorDetail
    {
        public string Message { get; set; } = string.Empty;
        public Exception? Exception { get; set; } = null;

    }
}
