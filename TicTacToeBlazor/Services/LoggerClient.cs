using System.Diagnostics;

namespace TicTacToeBlazor.Services
{
    public class LoggerClient : ILoggerClient
    {
        public void Error(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
