using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using UserSecretsExample;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddUserSecrets<Program>();
    })
    .ConfigureServices((context, services) =>
    {
        services.Configure<ApiSettings>(context.Configuration.GetSection("ApiSettings"));
        services.AddOptions<ApiSettings>().Bind(context.Configuration.GetSection("ApiSettings"))
        .ValidateDataAnnotations();
    });

var host = builder.Build();
var mySettings = host.Services.GetRequiredService<IOptions<ApiSettings>>().Value;

Console.WriteLine("--- Loading Settings ---");
Console.WriteLine($"Base URL: {mySettings.BaseUrl}");
Console.WriteLine($"API Key: {mySettings.ApiKey}");
Console.ReadKey();