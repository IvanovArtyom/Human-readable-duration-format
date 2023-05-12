using System;
using System.Text;

public class HumanTimeFormat
{
    public static void Main()
    {
        // Some tests
        var a = FormatDuration(62);
        // ...should return "1 minute and 2 seconds"
        var b = FormatDuration(3662);
        // ...should return "1 hour, 1 minute and 2 seconds"
    }

    public static string FormatDuration(int seconds)
    {
        if (seconds == 0)
            return "now";

        var timeSpan = TimeSpan.FromSeconds(seconds);
        int year = timeSpan.Days / 365;
        timeSpan = timeSpan.Subtract(TimeSpan.FromDays(year * 365));
        int count = 0;

        if (year > 0) ++count;
        if (timeSpan.Days > 0) ++count;
        if (timeSpan.Hours > 0) ++count;
        if (timeSpan.Minutes > 0) ++count;
        if (timeSpan.Seconds > 0) ++count;

        var result = new StringBuilder();
        result.Append(FormatConclusion(year, "year", ref count));
        result.Append(FormatConclusion(timeSpan.Days, "day", ref count));
        result.Append(FormatConclusion(timeSpan.Hours, "hour", ref count));
        result.Append(FormatConclusion(timeSpan.Minutes, "minute", ref count));
        result.Append(FormatConclusion(timeSpan.Seconds, "second", ref count));

        return result.ToString();
    }

    public static string FormatConclusion(int value, string timeInterval, ref int count)
    {
        if (value == 0)
            return "";

        if (value != 1)
            timeInterval += "s";

        return count-- switch
        {
            > 2 => $"{value} {timeInterval}, ",
            2 => $"{value} {timeInterval} and ",
            _ => $"{value} {timeInterval}"
        };
    }
}