using System;
using Aerith.Data;
using Aerith.Data.Interfaces;
using Aerith.Data.Services;
using Aerith.Scraper.Interfaces;
using Aerith.Scraper.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Aerith.Function.Startup))]

namespace Aerith.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDbContext<AerithContext>(options => {
                options.UseSqlServer("Server=LOCALHOST;Database=Aerith;User Id=aerithuser;Password=MyP@ssword1;");
            });

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<INrlScraper, NrlScraper>();
            builder.Services.AddMemoryCache();
        }
    }
}