namespace Common.Extensions;

public static class DateTimeExtensions
{
    public static string GetTime(this DateTime dateTime)
    {
        return dateTime.ToString("hh:mm:ss.fff tt");
    }
}