using GigaChef.Handlers;
using GigaChef.Services;
using NetCord;
using NetCord.Gateway;
using NetCord.Logging;
using NetCord.Rest;
using NetCord.Services;
using NetCord.Services.ApplicationCommands;
using Sprache;
using System.Threading.Tasks;

public class DiscordBot
{
    private readonly GatewayClient _client;

    private readonly GatewayIntents _intents = GatewayIntents.Guilds |
											  GatewayIntents.GuildUsers |
											  GatewayIntents.GuildMessages |
											  GatewayIntents.GuildMessageReactions |
											  GatewayIntents.GuildMessageTyping |
											  GatewayIntents.DirectMessages |
											  GatewayIntents.DirectMessageReactions |
											  GatewayIntents.DirectMessageTyping |
											  GatewayIntents.GuildVoiceStates |
											  GatewayIntents.MessageContent;

    private ApplicationCommandService<ApplicationCommandContext> _applicationCommandService;

    public DiscordBot(string token)
	{
		_client = new GatewayClient(new BotToken(token), new GatewayClientConfiguration
		{
			Intents = _intents
		});

        _applicationCommandService = new();

        AddCommands();
        RegisterEvents();
        RegisterInteractions();
    }

    private void AddCommands()
    {
        _applicationCommandService.AddSlashCommand(new SlashCommandBuilder("ping", "Ping!", () => "Pong!"));
        _applicationCommandService.AddModule<CommandModule>();
    }

    private void RegisterInteractions()
    {
        _client.InteractionCreate += async interaction =>
        {
            if (interaction is not ApplicationCommandInteraction applicationCommandInteraction) return;

            var result = await _applicationCommandService.ExecuteAsync(new ApplicationCommandContext(applicationCommandInteraction, _client));

            if (result is not IFailResult failResult) return;

            try
            {
                await interaction.SendResponseAsync(InteractionCallback.Message(failResult.Message));
                Logger.WriteLog(failResult.Message);
            }
            catch
            {
            }
        };
    }

    private void RegisterEvents()
    {
        // Ready event
        _client.Ready += args =>
        {
            Logger.WriteLog($"Bot connected as {args.User.Username}#{args.User.Discriminator}");
            return default;
        };

        // Message created logs
        _client.MessageCreate += message =>
        {
            Logger.WriteLog($"<[{message.ChannelId}:{message.Author.Username}]> {message.Content}");
            return default;
        };

        // Logger for message reactions
        _client.MessageReactionAdd += args =>
        {
            Logger.WriteLog($"<[{args.ChannelId} {args.User?.Username}]> reacted with {args.Emoji.Name}!");
            return default;
        };
    }

    public async Task StartAsync()
    {
        await _applicationCommandService.RegisterCommandsAsync(_client.Rest, _client.Id);

        await _client.StartAsync();
        await Task.Delay(-1);
    }
}
