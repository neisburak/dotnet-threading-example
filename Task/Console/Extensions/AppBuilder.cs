using Common.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Console.Extensions;

public static class AppBuilder
{
    public static ServiceProvider Build()
    {
        var builder = new ServiceCollection();
        builder.AddLogging(config => config.AddConsole());
        builder.AddCommonServices();
        return builder.BuildServiceProvider();
    }

    public static T Get<T>(this ServiceProvider provider) where T : class => provider.GetRequiredService<T>();
}