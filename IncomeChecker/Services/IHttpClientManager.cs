
namespace IncomeChecker.Services
{
    public interface IHttpClientManager
    {
        Task<HttpResponseMessage> GetRequestAsync(string requestUrl);
    }
}