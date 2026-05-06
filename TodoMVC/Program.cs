using Serilog;
using System.Linq;
using TodoMVC.Services;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    var domainUrl = builder.Configuration.GetSection("ApiUrl").Value!;
    builder.Services.AddSingleton<IHttpClientService, RestClientService>(_ => new RestClientService(domainUrl));

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
} catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
} finally
{
    Log.CloseAndFlush();
}


