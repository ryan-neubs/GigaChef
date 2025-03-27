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

        public CommandHandler(DiscordSocketClient client)
        {
            _client = client;
            _client.Ready += RegisterCommandsAsync;
            _client.SlashCommandExecuted += HandleSlashCommand;
        }

        private async Task RegisterCommandsAsync()
        {
            var guild = _client.GetGuild(1102454152908517436);

            var cookCommand = new SlashCommandBuilder()
                .WithName("cook")
                .WithDescription("Responds with 'YES CHEF'");

            try
            {
                await guild.CreateApplicationCommandAsync(cookCommand.Build());
                Console.WriteLine("Slash Command /cook registered");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error registering command: {ex.Message}");
            }
        }

        private async Task HandleSlashCommand(SocketSlashCommand command)
        {
            if (command.Data.Name == "cook")
            {
                await command.RespondAsync("YES CHEF!");
            }
        }

    }
}
