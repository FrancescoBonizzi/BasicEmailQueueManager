using BasicEmailQueueManager.Infrastructure;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Playground
{
    public class ConsoleLogger : ILogger
    {
        public Task LogError(string message, Exception exception, [CallerMemberName] string caller = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{caller}: {message} - {exception.Message}");
            Console.ForegroundColor = ConsoleColor.White;
            return Task.CompletedTask;
        }

        public Task LogError(Exception exception, [CallerMemberName] string caller = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{caller}: {exception.Message}");
            Console.ForegroundColor = ConsoleColor.White;
            return Task.CompletedTask;
        }

        public Task LogError(string error, [CallerMemberName] string caller = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{caller}: {error}");
            Console.ForegroundColor = ConsoleColor.White;
            return Task.CompletedTask;
        }

        public Task LogInformation(string message, [CallerMemberName] string caller = null)
        {
            Console.WriteLine($"{caller}: {message}");
            return Task.CompletedTask;
        }
    }
}
