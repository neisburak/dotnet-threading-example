using Console.Configuration;

namespace Console;

public static class TaskExtensions
{
    public static async Task ContinueWith(this IAppConfiguration configuration, bool perform = true)
    {
        if (!perform) return;

        configuration.WriteLine("App started.");

        var task = configuration.PostService.GetAsync().ContinueWith(async (task) =>
        {
            var posts = await task;

            if (posts is null) return;

            foreach (var post in posts) configuration.WriteLine(post);
        });

        configuration.WriteLine("Doing other stuff.");

        await task;
    }

    public static async Task WhenAll(this IAppConfiguration configuration, bool perform = true)
    {
        if (!perform) return;
    }
}