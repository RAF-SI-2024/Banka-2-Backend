using System.IdentityModel.Tokens.Jwt;
using System.Text;

using Bank.Application;
using Bank.Application.Domain;
using Bank.ExchangeService.BackgroundServices;
using Bank.ExchangeService.Configurations;
using Bank.ExchangeService.Database;
using Bank.ExchangeService.HostedServices;
using Bank.ExchangeService.HttpClients;
using Bank.ExchangeService.Repositorties;
using Bank.ExchangeService.Services;

using DotNetEnv;

using FluentValidation;
using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Bank.ExchangeService.Application;

public class ExchangeApplication
{
    public static void Run(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Env.Load();

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        builder.Services.AddValidation();
        builder.Services.AddServices();
        builder.Services.AddDatabase();
        builder.Services.AddHostedServices();
        builder.Services.AddBackgroundServices();
        builder.Services.AddHttpServices();

        builder.Services.AddCors();
        builder.Services.AddAuthenticationServices();
        builder.Services.AddAuthorizationServices();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwagger();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(Configuration.Policy.FrontendApplication);

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddScoped<IStockExchangeRepository, StockExchangeRepository>();
        services.AddScoped<IStockExchangeService, StockExchangeService>();
        services.AddScoped<IListingRepository, ListingRepository>();
        services.AddScoped<IListingService, ListingService>();
        services.AddScoped<IListingHistoricalRepository, ListingHistoricalRepository>();
        services.AddScoped<IListingHistoricalService, ListingHistoricalService>();
        services.AddScoped<ICurrencyClient, CurrencyClient>();

        return services;
    }

    public static IServiceCollection AddBackgroundServices(this IServiceCollection services)
    {
        services.AddSingleton<DatabaseBackgroundService>();

        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(Configuration.Database.GetConnectionString()), ServiceLifetime.Scoped, ServiceLifetime.Singleton);

        return services;
    }

    public static IServiceCollection AddHostedServices(this IServiceCollection services)
    {
        services.AddSingleton<DatabaseHostedService>();
        services.AddHostedService<ApplicationHostedService>();

        return services;
    }

    public static IServiceCollection AddHttpServices(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddHttpClient<ICurrencyClient, CurrencyClient>(client => { client.BaseAddress = new Uri("http://localhost:5075"); });
        services.AddHttpContextAccessor();

        return services;
    }

    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Continue;
        ValidatorOptions.Global.DefaultRuleLevelCascadeMode  = CascadeMode.Stop;

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<AssemblyInfo>();

        return services;
    }

    public static IServiceCollection AddCors(this IServiceCollection services)
    {
        services.AddCors(options => options.AddPolicy(Configuration.Policy.FrontendApplication, policy => policy.WithOrigins(Configuration.Frontend.BaseUrl)
                                                                                                                .AllowAnyHeader()
                                                                                                                .AllowAnyMethod()));

        return services;
    }

    public static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtOptions => jwtOptions.TokenValidationParameters = new TokenValidationParameters
                                                                                   {
                                                                                       IssuerSigningKey =
                                                                                       new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.Jwt
                                                                                                                                                    .SecretKey)),
                                                                                       ValidateIssuerSigningKey = true,
                                                                                       ValidateLifetime         = true,
                                                                                       ValidateIssuer           = false,
                                                                                       ValidateAudience         = false,
                                                                                       ClockSkew                = TimeSpan.Zero
                                                                                   });

        return services;
    }

    public static IServiceCollection AddAuthorizationServices(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
                .AddPolicy(Configuration.Policy.Role.Admin,    policy => policy.RequireRole(nameof(Role.Admin)))
                .AddPolicy(Configuration.Policy.Role.Employee, policy => policy.RequireRole(nameof(Role.Employee)))
                .AddPolicy(Configuration.Policy.Role.Client,   policy => policy.RequireRole(nameof(Role.Client)));

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(config =>
                               {
                                   config.SwaggerDoc("v1", new OpenApiInfo { Title = "ExchangeService", Version = "v1" });

                                   config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                                                                          {
                                                                              Description = "Authorization: Bearer {token}",
                                                                              Name        = "Authorization",
                                                                              In          = ParameterLocation.Header,
                                                                              Type        = SecuritySchemeType.ApiKey,
                                                                              Scheme      = "Bearer"
                                                                          });

                                   config.AddSecurityRequirement(new OpenApiSecurityRequirement
                                                                 {
                                                                     {
                                                                         new OpenApiSecurityScheme
                                                                         {
                                                                             Reference = new OpenApiReference
                                                                                         {
                                                                                             Type =
                                                                                             ReferenceType
                                                                                             .SecurityScheme,
                                                                                             Id =
                                                                                             "Bearer"
                                                                                         }
                                                                         },
                                                                         []
                                                                     }
                                                                 });
                               });

        return services;
    }
}
