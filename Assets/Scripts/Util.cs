using System;

public class Util
{
    public static int GetCurrentTime()
    {
        return (int)DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
    }

    public static int GetCurrentTimeInDays()
    {
        return (int)DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalDays;
    }

    public static long GetCurrentTimeInMills()
    {
        return (long)DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
    }

    public static double GetCurrentTimeInDouble()
    {
        return DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
    }
}
