using System.Net;

namespace Core.Shared;

public class Response<TResult>
{
    public TResult? Data { get; set; }
    public string? Message { get; set; }
    public HttpStatusCode StatusCode { get; set; }
}
