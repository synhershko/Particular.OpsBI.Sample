using System;

public static class SampleRandomization
{
    internal static readonly Random Random = new Random();

    public static readonly int FailedMessagesRate = 30;
    public static readonly int FailedMessagesRateDuringPeaks = 5;

	public static bool IsPeak()
	{
	    var now = DateTime.UtcNow;
	    if (now.Minute <= 5)
	        return true;
	    return false;
	}

    public static int SleepBetweenMessages()
    {
        // On peak we sleep less and send more
        return Random.Next(IsPeak() ? 100 : 10000);
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

    public static bool ShouldThisOrderFail(int orderNumber)
    {
        return orderNumber%(IsPeak() ? FailedMessagesRateDuringPeaks : FailedMessagesRate) == 0;
    }
}
