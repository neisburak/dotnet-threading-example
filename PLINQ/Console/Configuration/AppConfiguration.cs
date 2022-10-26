using Common.Extensions;
using Common.Services.Abstract;
using Console.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Console.Configuration;

public interface IAppConfiguration
{
    IPostService PostService { get; }
    T GetService<T>() where T : class;
}

public class AppConfiguration : IAppConfiguration
{
    private readonly ServiceProvider _provider;
    private readonly IConfiguration _configuration;

    public AppConfiguration()
    {
        var configurationBuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (!string.IsNullOrEmpty(environment)) configurationBuilder.AddJsonFile($"appsettings.{environment}.json");
        _configuration = configurationBuilder.Build();

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging(config => config.AddConsole());
        serviceCollection.AddDbContext<NorthwindContext>(options =>
        {
            options.UseSqlServer(GetConnectionString("SqlServer"));
        });
        serviceCollection.AddCommonServices();
        _provider = serviceCollection.BuildServiceProvider();
    }

    public IPostService PostService => _provider.GetRequiredService<IPostService>();

    public T GetService<T>() where T : class => _provider.GetRequiredService<T>();

    public string GetConnectionString(string name) => _configuration.GetConnectionString(name);
}