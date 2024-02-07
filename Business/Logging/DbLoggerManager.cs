using Business.Abstracts;

namespace Business.Logging;

public class DbLoggerManager : ILoggerService
{
    public void Log(string message)
    {
        Console.WriteLine("[DbLogger] - " + message);
    }
}
