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

    public static void ResizeForEach(this IAppConfiguration configuration, bool perform = true)
    {
        if (!perform) return;

        // Parallel.ForEach() must be called for heavy jobs, otherwise it will be less performant.
        configuration.WithParallelForEach();

        configuration.WithOutParallelForEach();
    }

    public static void TotalSizeForEach(this IAppConfiguration configuration, bool perform = true, bool raceCondition = false)
    {
        if (!perform) return;

        var totalSize = 0L;

        var items = Directory.GetFiles($"{Directory.GetCurrentDirectory()}/Images");

        Parallel.ForEach(items, (item) =>
        {
            configuration.WriteLine($"Getting file size with thread id: {Thread.CurrentThread.ManagedThreadId}");
            var fileInfo = new FileInfo(item);

            // It locks the totalSize for race conditions for thread-safe operation.
            if (!raceCondition) Interlocked.Add(ref totalSize, fileInfo.Length);
            else totalSize += fileInfo.Length;
        });

        configuration.WriteLine($"File sizes received. {totalSize / 1024 / 1024} mb");
    }

    public static void TotalSizeFor(this IAppConfiguration configuration, bool perform = true)
    {
        if (!perform) return;

        var totalSize = 0L;

        var items = Directory.GetFiles($"{Directory.GetCurrentDirectory()}/Images");

        Parallel.For(0, items.Length, (index) =>
        {
            configuration.WriteLine($"Getting file size with thread id: {Thread.CurrentThread.ManagedThreadId}");
            var fileInfo = new FileInfo(items[index]);

            Interlocked.Add(ref totalSize, fileInfo.Length);
        });

        configuration.WriteLine($"File sizes received. {totalSize / 1024 / 1024} mb");
    }

    public static void TotalSizeForEachWithSharedData(this IAppConfiguration configuration, bool perform = true)
    {
        if (!perform) return;

        var totalSize = 0L;

        var items = Directory.GetFiles($"{Directory.GetCurrentDirectory()}/Images");

        // Parallel.For can also be used.
        Parallel.ForEach(items, () => 0L, (item, state, sharedData) =>
        {
            configuration.WriteLine($"Getting file size with thread id: {Thread.CurrentThread.ManagedThreadId}");
            var fileInfo = new FileInfo(item);
            sharedData += fileInfo.Length;
            return sharedData;
        }, (sharedData) =>
        {
            Interlocked.Add(ref totalSize, sharedData);
        });

        configuration.WriteLine($"File sizes received. {totalSize / 1024 / 1024} mb");
    }
}