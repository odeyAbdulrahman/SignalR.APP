using Newtonsoft.Json;
using System.Text;

namespace SignalR.API.Extensions;
public class HttpContextAccessorUtility
{
    private static IHttpContextAccessor? ContextAccessor;

    public static IHttpContextAccessor? CurrentContextAccessor()
    {
        return ContextAccessor ?? null;
    }
    public static HttpContext? Current()
    {
        return ContextAccessor?.HttpContext;
    }

    public static HttpContext? SetHttpContext(string message, string? method, string Path, string? contentType)
    {
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Method = method ?? "POST";
        httpContext.Request.Path = Path;
        httpContext.Request.ContentType = contentType ?? "application/json";
        httpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)));
        return httpContext;
    }

    internal static void Configure(IHttpContextAccessor? contextAccessor)
    {
        ContextAccessor = contextAccessor;
    }
}
