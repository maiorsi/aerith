using System;
using System.Text;
using Aerith.Api.Interfaces;
using Aerith.Api.Services;
using Aerith.Api.Settings;
using Aerith.Common.Models.Identity;
using Aerith.Data;
using Aerith.Data.Interfaces;
using Aerith.Data.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Aerith.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            // DB Context
            services.AddDbContext<AerithContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("AerithDB"), b => b.MigrationsAssembly("aerith-api"));
            });

            // Settings
            services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));
            services.Configure<AerithSettings>(Configuration.GetSection("Aerith"));

            // DB Repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            
            services.AddSingleton<IJwtService, JwtService>();

            // Memory Cache
            services.AddMemoryCache();

            // API Versioning
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Aerith API", Version = "v1" });
            });

            // Lower case URLs
            services.AddRouting(options => options.LowercaseUrls = true);

            // CORS
            services.AddCors(options =>
            {
                options.AddPolicy(name: "cors",
                    builder =>
                    {
                        //* Move this into Settings
                        builder.WithOrigins("http://localhost:8080")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            // Identity
            services.AddIdentityCore<ApplicationUser>(o =>
            {
                //* Move these into Settings
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            })
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<AerithContext>()
            .AddDefaultTokenProviders();

            var jwtSettings = Configuration.GetSection("Jwt");

            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings[nameof(JwtSettings.HmacSecretKey)]));

            // Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        //* Move these into Settings
                        ClockSkew = TimeSpan.FromMinutes(double.Parse(jwtSettings[nameof(JwtSettings.ClockSkewMinutes)])),
                        IssuerSigningKey = issuerSigningKey,
                        RequireSignedTokens = bool.Parse(jwtSettings[nameof(JwtSettings.RequireSignedTokens)]),
                        RequireExpirationTime = bool.Parse(jwtSettings[nameof(JwtSettings.RequireExpirationTime)]),
                        ValidateLifetime = bool.Parse(jwtSettings[nameof(JwtSettings.ValidateLifetime)]),
                        ValidateAudience = bool.Parse(jwtSettings[nameof(JwtSettings.ValidateAudience)]),
                        ValidAudience = jwtSettings[nameof(JwtSettings.Audience)],
                        ValidateIssuer = bool.Parse(jwtSettings[nameof(JwtSettings.ValidateIssuer)]),
                        ValidIssuer = jwtSettings[nameof(JwtSettings.Issuer)]
                    };
                });
            
            // Automapper
            services.AddAutoMapper(typeof(Startup));

            IdentityModelEventSource.ShowPII = true;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aerith API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseCors("cors");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
