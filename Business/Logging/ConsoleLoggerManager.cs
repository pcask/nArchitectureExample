using Business.Abstracts;

namespace Business.Logging
{
    public class ConsoleLoggerManager : ILoggerService
    {
        public void Log(string message)
        {
            Console.WriteLine("[ConsoleLogger] - " + message);
        }
    }
}
