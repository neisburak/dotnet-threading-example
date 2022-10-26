using System.Drawing;
using Console.Configuration;

namespace Console;

public static class TPLExtensions
{
    public static void ForEach(this IAppConfiguration configuration, bool perform = true)
    {
        if (!perform) return;

        var items = Directory.GetFiles($"{Directory.GetCurrentDirectory()}/Images");

        Parallel.ForEach(items, (item) =>
        {
            var image = new Bitmap(item);
            var thumbnail = image.GetThumbnailImage(50, 50, () => false, IntPtr.Zero);
            thumbnail.Save(Path.Combine($"{Directory.GetCurrentDirectory()}/Thumbnails", Path.GetFileName(item)));
        });
    }
}