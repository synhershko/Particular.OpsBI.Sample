using System;

public static class SampleRandomization
{
    internal static readonly Random Random = new Random();

    public static readonly int FailedMessagesRate = 120;
    public static readonly int FailedMessagesRateDuringPeaks = 120;

	public static bool IsPeak()
	{
	    var now = DateTime.UtcNow;
	    if (now.Minute <= 5)
	        return true;
	    return false;
	}

    public static Exception RandomException()
    {
        var r = Random.Next();

        if (r % 3 == 0)
            return new TimeoutException("Timeout reached when trying to access external billing service");

        if (r%2 == 0)
            return new InsufficientMemoryException();

        return new ArgumentException();
    }
}
