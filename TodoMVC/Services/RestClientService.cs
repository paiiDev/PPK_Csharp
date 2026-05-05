using Newtonsoft.Json;
using RestSharp;

namespace TodoMVC.Services
{
    public class RestClientService : IHttpClientService
    {
        private readonly RestClient _restClient;
        public RestClientService(string domainUrl)
        {
            _restClient = new RestClient(domainUrl);
        }

        public async Task<T> SendAsync<T>(string url, Enums.EnumHttpMethod method, object? data = null)
        {
            RestRequest request = new RestRequest(url, (Method)method);
            if (data != null)
            {
                var jsonStr = JsonConvert.SerializeObject(data);
                request.AddJsonBody(jsonStr);
            }
            var response = await _restClient.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                var responseString = response.Content!;
                return JsonConvert.DeserializeObject<T>(responseString)!;
            }
            else
            {
                // 🚨 တကယ့် Error အစစ်ကို ဒီနေရာမှာ ထုတ်ကြည့်ပါမည်
                Console.WriteLine($"🚨 API Error Status: {response.StatusCode}");
                Console.WriteLine($"🚨 API Error Message: {response.ErrorMessage}");
                Console.WriteLine($"🚨 API Error Exception: {response.ErrorException?.Message}");
            }
            return default!;
        }
    }
}
