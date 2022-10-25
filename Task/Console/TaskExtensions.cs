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

        configuration.WriteLine($"Main thread id: {Thread.CurrentThread.ManagedThreadId}");

        var tasks = Enumerable.Range(1, 3).Select(s => configuration.PostService.GetAsync(s));

        var posts = await Task.WhenAll(tasks);

        foreach (var post in posts) configuration.WriteLine(post);
    }

    public static async Task WhenAny(this IAppConfiguration configuration, bool perform = true)
    {
        if (!perform) return;

        var tasks = Enumerable.Range(1, 3).Select(s => configuration.PostService.GetAsync(s));

        var post = await Task.WhenAny(tasks);

        configuration.WriteLine(post.Result);
    }

    public static void WaitAll(this IAppConfiguration configuration, bool perform = true)
    {
        if (!perform) return;

        var tasks = Enumerable.Range(1, 3).Select(s =>
        {
            return configuration.PostService.GetAsync(s)
                .ContinueWith(async (task) => configuration.WriteLine(await task));
        });

        configuration.WriteLine("Before Task.WaitAll()");

        Task.WaitAll(tasks.ToArray()); // It blocks the main thread.

        configuration.WriteLine("After Task.WaitAll()");
    }

    public static void WaitAny(this IAppConfiguration configuration, bool perform = true)
    {
        if (!perform) return;

        var tasks = Enumerable.Range(1, 3).Select(s => configuration.PostService.GetAsync(s));

        configuration.WriteLine("Before Task.WaitAny()");

        var index = Task.WaitAny(tasks.ToArray()); // It blocks the main thread.

        configuration.WriteLine($"After Task.WaitAny() with index: {index}");
    }

    public static async Task Delay(this IAppConfiguration configuration, bool perform = true)
    {
        if (!perform) return;

        var task = configuration.PostService.GetAsync();

        await Task.Delay(10000); // It doesn't block the main thread.

        var posts = await task;
        if (posts is null) return;
        foreach (var post in posts) configuration.WriteLine(post);
    }

    public static async Task Run(this IAppConfiguration configuration, bool perform = true)
    {
        if (!perform) return;

        var task = configuration.PostService.GetAsync();

        await Task.Delay(10000); // It doesn't block the main thread.

        var posts = await task;
        if (posts is null) return;
        foreach (var post in posts) configuration.WriteLine(post);
    }
}