using Newtonsoft.Json;
using TodoMVC.Enums;

namespace TodoMVC.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;
        public HttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<T> SendAsync<T>(string url, EnumHttpMethod method, object? data = null)
        {
           var request = new HttpRequestMessage(new HttpMethod(method.ToString()), url);
           if(data != null)
            {
                var jsonStr = JsonConvert.SerializeObject(data);
                request.Content = new StringContent(jsonStr, System.Text.Encoding.UTF8, "application/json");
            }
           var response = await _httpClient.SendAsync(request);

            if(response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseContent)!;
            }
            return default!;
        }
    }
}
