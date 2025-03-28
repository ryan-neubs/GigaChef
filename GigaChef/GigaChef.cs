using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetEnv;
using Discord.Net;
using Newtonsoft.Json;
using Discord.Commands;

namespace GigaChef
{
    public class Bot
    {
        private static DiscordSocketClient _client;
        private static CommandHandler _commandHandler;
        private static LoggingService _loggingService;

        public static async Task Main(string[] args)
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.Guilds
            });

            _loggingService = new LoggingService(_client);
            _commandHandler = new CommandHandler(_client, _loggingService);

            Env.Load("C:/Users/kmneu/source/repos/GigaChef/GigaChef/.env");
            var token = Environment.GetEnvironmentVariable("TOKEN");

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();


            await Task.Delay(-1);
        }
    }
}
