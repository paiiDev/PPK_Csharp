using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OpptionPattern;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.Configure<ApiSettings>(context.Configuration.GetSection("ApiSettings"));
    });

var host = builder.Build();
var mySettings = host.Services.GetRequiredService<IOptions<ApiSettings>>().Value;

Console.WriteLine("--- Loading Settings from appsettings.json ---");
Console.WriteLine($"Base URL: {mySettings.BaseUrl}");
Console.WriteLine($"API Key: {mySettings.ApiKey}");