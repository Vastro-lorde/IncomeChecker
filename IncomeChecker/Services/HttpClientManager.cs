using System.Net.Http.Headers;

namespace IncomeChecker.Services
{
    public class HttpClientManager : IHttpClientManager
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client = new HttpClient(); // Making the client a property so this same client is used throught out.

        public HttpClientManager(IConfiguration configuration)
        {
            _configuration = configuration;
            _client.BaseAddress = new Uri(_configuration.GetSection("BaseUrl").Value); // Assignin the base-url from AppSettings.
        }

        public async Task<HttpResponseMessage> GetRequestAsync(string requestUrl)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("mono-sec-key", _configuration.GetSection("MonoSecKey").Value.ToString()); // Add the mono-sec-key in the headers.
            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            var response = await _client.SendAsync(request);
            return response;
        }
    }
}
