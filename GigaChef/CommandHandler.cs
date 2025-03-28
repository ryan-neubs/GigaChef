using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace GigaChef
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly LoggingService _logger;
        public CommandHandler(DiscordSocketClient client, LoggingService logger)
        {
            _client = client;
            _logger = logger;
            _client.Ready += RegisterCommandsAsync;
            _client.SlashCommandExecuted += HandleSlashCommand;
        }

        private async Task RegisterCommandsAsync()
        {
            var guild = _client.GetGuild(1102454152908517436);

            List<SlashCommandBuilder> commands = new List<SlashCommandBuilder>();
            commands.Add(
                new SlashCommandBuilder()
                    .WithName("cook")
                    .WithDescription("Responds with 'YES CHEF'")
                );
            commands.Add(
                new SlashCommandBuilder()
                    .WithName("ping")
                    .WithDescription("Responds with 'pong' to test if bot is repsonding.")
                );
                
            foreach (var command in commands)
            {
                try
                {
                    await guild.CreateApplicationCommandAsync(command.Build());
                    _logger.LogInfo("Cmd Handler: ", $"Slash Command /{command.Name} registered");
                }
                catch (Exception ex)
                {
                    _logger.LogInfo("Cmd Handler: ", $"Error registering /{command.Name} command: {ex.Message}");
                }
            }
        }

        private async Task HandleSlashCommand(SocketSlashCommand command)
        {
            if (command.Data.Name == "cook")
            {
                await command.RespondAsync("YES CHEF!"); 
            }

            if (command.Data.Name == "ping")
            {
                await command.RespondAsync("pong");
            }
            _logger.LogInfo("Cmd Handler: ", $"{command.User} executed command /{command.Data.Name}");
        }

    }
}
