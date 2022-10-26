using System.Globalization;
using System.Text;
using Console.Configuration;
using Console.Data;

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

        evenNumbers.ForAll((item) => sb.Append($" {item} "));

        configuration.WriteLine(sb.ToString());
    }

    public static void ForAll(this IAppConfiguration configuration, NorthwindContext context, bool perform = true)
    {
        if (!perform) return;

        var list = from prouct in context.Products
                   where prouct.UnitPrice > 50
                   select prouct;

        list.AsParallel().Where(w => w.ProductName.StartsWith("C", true, CultureInfo.CurrentCulture)).ForAll(f => configuration.WriteLine(f.ProductName));
    }

    public static void WithDegreeOfParallelism(this IAppConfiguration configuration, NorthwindContext context, bool perform = true)
    {
        if (!perform) return;

        var list = from prouct in context.Products
                   where prouct.UnitPrice > 50
                   select prouct;

        list.AsParallel().WithDegreeOfParallelism(2).Where(w => w.ProductName.StartsWith("C", true, CultureInfo.CurrentCulture)).ForAll(f => configuration.WriteLine(f.ProductName));
    }

    public static void WithExecuteMode(this IAppConfiguration configuration, NorthwindContext context, bool perform = true)
    {
        if (!perform) return;

        var list = from prouct in context.Products
                   where prouct.UnitPrice > 50
                   select prouct;

        list.AsParallel().WithExecutionMode(ParallelExecutionMode.ForceParallelism).Where(w => w.ProductName.StartsWith("C", true, CultureInfo.CurrentCulture)).ForAll(f => configuration.WriteLine(f.ProductName));
    }

    public static void AsOrdered(this IAppConfiguration configuration, NorthwindContext context, bool perform = true)
    {
        if (!perform) return;

        var list = from prouct in context.Products
                   where prouct.UnitPrice > 50
                   select prouct;

        list.AsParallel().AsOrdered().Where(w => w.ProductName.StartsWith("C", true, CultureInfo.CurrentCulture)).ForAll(f => configuration.WriteLine(f.ProductName));
    }

    public static void ExceptionHandling(this IAppConfiguration configuration, NorthwindContext context, bool perform = true)
    {
        if (!perform) return;

        try
        {
            var list = context.Products.Take(10).ToList();

            list.AsParallel().AsOrdered().Where(w => w.ProductName[10] == 'c').ForAll(f => configuration.WriteLine(f.ProductName));
        }
        catch (AggregateException ex)
        {
            ex.InnerExceptions.ToList().ForEach(f => configuration.WriteLine(f.Message));
        }
    }

    public static void CanellationToken(this IAppConfiguration configuration, NorthwindContext context, CancellationToken cancellationToken, bool perform = true)
    {
        if (!perform) return;

        try
        {
            var list = context.Products.Take(10).ToList();

            list.AsParallel().WithCancellation(cancellationToken).Where(w => w.UnitPrice > 10).ForAll(f => configuration.WriteLine(f.ProductName));
        }
        catch (OperationCanceledException ex)
        {
            configuration.WriteLine(ex.Message);
        }
    }
}