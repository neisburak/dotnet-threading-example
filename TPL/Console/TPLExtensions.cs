using System.Diagnostics;
using Console.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Console;

public static class TPLExtensions
{
    private static void WithOutParallelForEach(this IAppConfiguration configuration)
    {
        configuration.WriteLine("Getting images without parallel...");

        var stopwatch = Stopwatch.StartNew();

        var items = Directory.GetFiles($"{Directory.GetCurrentDirectory()}/Images");

        items.ToList().ForEach((item) =>
        {
            configuration.WriteLine($"Thumbnail creating with thread id: {Thread.CurrentThread.ManagedThreadId}");

            using var image = Image.Load(item);
            image.Mutate(x => x.Resize(50, 50));
            var thumbnails = $"{Directory.GetCurrentDirectory()}/Thumbnails";
            if (!Directory.Exists(thumbnails)) Directory.CreateDirectory(thumbnails);
            image.Save(Path.Combine(thumbnails, Path.GetFileName(item)));
        });

        stopwatch.Stop();

        configuration.WriteLine($"Thumbnails were created in {stopwatch.ElapsedMilliseconds} ms.");
    }

    private static void WithParallelForEach(this IAppConfiguration configuration)
    {
        configuration.WriteLine("Getting images...");

        var stopwatch = Stopwatch.StartNew();

        var items = Directory.GetFiles($"{Directory.GetCurrentDirectory()}/Images");

        Parallel.ForEach(items, (item) =>
        {
            configuration.WriteLine($"Thumbnail creating with thread id: {Thread.CurrentThread.ManagedThreadId}");

            using var image = Image.Load(item);
            image.Mutate(x => x.Resize(50, 50));
            var thumbnails = $"{Directory.GetCurrentDirectory()}/Thumbnails";
            if (!Directory.Exists(thumbnails)) Directory.CreateDirectory(thumbnails);
            image.Save(Path.Combine(thumbnails, Path.GetFileName(item)));
        });

        stopwatch.Stop();

        configuration.WriteLine($"Thumbnails were created in {stopwatch.ElapsedMilliseconds} ms.");
    }

    public static void ForEach(this IAppConfiguration configuration, bool perform = true)
    {
        if (!perform) return;

        configuration.WithParallelForEach();

        configuration.WithOutParallelForEach();
    }
}