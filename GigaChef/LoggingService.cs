using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaChef
{
    public class LoggingService
    {

        public LoggingService(DiscordSocketClient client)
        {
            client.Log += LogAsync;
        }
        private Task LogAsync(LogMessage message)
        {
            Console.WriteLine($"[{message.Severity}] {message}");
            return Task.CompletedTask;
        }

        public void LogInfo(string source, string message)
        {
            LogAsync(new LogMessage(LogSeverity.Info, source, message));
        }

        public void LogError(string source, string message, Exception ex)
        {
            LogAsync(new LogMessage(LogSeverity.Error, source, message, ex));
        }
    }
}
