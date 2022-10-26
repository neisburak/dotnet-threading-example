namespace Console.Configuration;

public static class AppBuilder
{
    public static IAppConfiguration Build() => new AppConfiguration();

    public static T Get<T>(this IAppConfiguration configuration) where T : class => configuration.GetService<T>();
    public static void Write(this IAppConfiguration configuration, string value) => System.Console.Write(value);
    public static void WriteLine<T>(this IAppConfiguration configuration, T value) => System.Console.WriteLine(value);
}