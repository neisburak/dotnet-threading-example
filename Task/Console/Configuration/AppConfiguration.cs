using Common.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Console.Configuration;

public interface IAppConfiguration
{
    T GetService<T>() where T : class;
}

public class AppConfiguration : IAppConfiguration
{
    private readonly ServiceProvider _provider;

    public AppConfiguration()
    {
        var builder = new ServiceCollection();
        builder.AddLogging(config => config.AddConsole());
        builder.AddCommonServices();
        _provider = builder.BuildServiceProvider();
    }

    public T GetService<T>() where T : class => _provider.GetRequiredService<T>();
}