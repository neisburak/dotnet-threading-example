using System.Text;
using Console.Configuration;

namespace Console;

public static class PLINQExtensions
{
    public static void AsParallel(this IAppConfiguration configuration, bool perform = true)
    {
        if (!perform) return;

        var array = Enumerable.Range(1, 100).ToList();
        var evenNumbers = array.AsParallel().Where(w => w % 2 == 0);

        configuration.WriteLine(string.Join(", ", evenNumbers));
    }

    public static void ForAll(this IAppConfiguration configuration, bool perform = true)
    {
        if (!perform) return;

        var array = Enumerable.Range(1, 100).ToList();
        var evenNumbers = array.AsParallel().Where(w => w % 2 == 0);

        var sb = new StringBuilder("");

        evenNumbers.ForAll((item)=>sb.Append($" {item} "));

        configuration.WriteLine(sb.ToString());
    }
}