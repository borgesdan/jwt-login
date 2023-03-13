using System.Net;

namespace Marketplace.Application.Services
{
    public class StatusService
    {
        public static IResultData OkStatus()
            => new ResultData<string>(true, "OK!", HttpStatusCode.OK, $"{DateTime.UtcNow.ToLongDateString()}, {DateTime.UtcNow.ToLongTimeString()}");
    }
}