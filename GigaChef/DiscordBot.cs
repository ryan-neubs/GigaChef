using DotNetEnv;
using GigaChef.Services;
using NetCord;
using NetCord.Gateway;
using NetCord.Logging;
using NetCord.Rest;

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

    public DiscordBot(string token)
	{
		_client = new GatewayClient(new BotToken(token), new GatewayClientConfiguration
		{
			Intents = _intents
		});

        RegisterEvents();
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
        await _client.StartAsync();
        await Task.Delay(-1);
    }
}
