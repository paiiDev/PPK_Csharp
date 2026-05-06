using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TodoMVC.Models;
using TodoMVC.Services;

namespace TodoMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientService _httpClientService;

        public HomeController(ILogger<HomeController> logger, IHttpClientService httpClientService)
        {
            _logger = logger;
            _httpClientService = httpClientService;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("HomeController Index action called. Fetching todos from API.");
            try
            {
                _logger.LogDebug("Sending GET request to API endpoint: api/todos");
                var response = await _httpClientService.SendAsync<BaseResponse<List<Todo>>>("api/todos", Enums.EnumHttpMethod.Get);
                if (response != null && response.data != null)
                {
                    return View(response.data);
                }
                else
                {
                    _logger.LogError("Failed to fetch todos from API. Message: {Message}", response?.message);
                    return View(new List<Todo>());
                }
            } catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching todos from API.");
                return View(new List<Todo>());
            }
           
        }

        public IActionResult Privacy()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
