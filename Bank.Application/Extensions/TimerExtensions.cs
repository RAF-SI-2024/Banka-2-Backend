namespace Bank.Application.Extensions;

public static class TimerExtensions
{
    public static void Cancel(this Timer timer)
    {
        timer.Change(Timeout.Infinite, 0);
        timer.Dispose();
    }
}
