using Common.Services.Abstract;
using Common.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services)
    {
        services.AddScoped<IPostService, PostManager>();

        return services;
    }
}