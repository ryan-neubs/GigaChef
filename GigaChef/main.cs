using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetEnv;

namespace GigaChef
{
    public class Program
    {
        private static DiscordSocketClient _client;

        public static async Task Main(string[] args)
        {
            _client = new DiscordSocketClient();

            _client.Log += Log;

            Env.Load("C:/Users/kmneu/source/repos/GigaChef/GigaChef/.env");
            var token = Environment.GetEnvironmentVariable("TOKEN");

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
