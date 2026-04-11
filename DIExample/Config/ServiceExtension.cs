using DIExample.Interfaces;
using DIExample.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace DIExample.Config
{
    public static class ServiceExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddRefitClient<ITodoApi>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri("https://localhost:7062");
                });

            services.AddScoped<ITodoRefitService, TodoRefitService>();
        }
    }
}
