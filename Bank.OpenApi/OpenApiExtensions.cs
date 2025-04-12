using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using Scalar.AspNetCore;

namespace Bank.OpenApi;

public static class OpenApiExtensions
{
    public static IServiceCollection AddOpenApiServices(this IServiceCollection services)
    {
        services.AddOpenApi();

        return services;
    }

    public static IApplicationBuilder MapOpenApiScalar(WebApplication application)
    {
        application.MapOpenApi();
        application.MapScalarApiReference();
        
        return application;
    }

}
