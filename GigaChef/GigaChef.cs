using DotNetEnv;
using Microsoft.AspNetCore.Http.HttpResults;
using NetCord;
using NetCord.Gateway;
using NetCord.Logging;
using System.IO;

// Create console output capture for writing to log files
string logPath = Path.Combine(AppContext.BaseDirectory, $"{DateTime.Now:yyyy-MM-dd HH_mm_ss}-consolelog.log");
StreamWriter streamWriter = new StreamWriter(logPath, false) { AutoFlush = true };

// Load Discord API token from .env file in order to authenticate bot
Env.Load();
string? token = Environment.GetEnvironmentVariable("TOKEN");

// Check if token is null, this will prevent runtime errors.
if (string.IsNullOrEmpty(token))
{
  Console.WriteLine("Error: Token was not located in environment variable");
  return;
}

GatewayIntents intents = GatewayIntents.Guilds |
                         GatewayIntents.GuildUsers |
                         GatewayIntents.GuildMessages |
                         GatewayIntents.GuildMessageReactions |
                         GatewayIntents.GuildMessageTyping |
                         GatewayIntents.DirectMessages |
                         GatewayIntents.DirectMessageReactions |
                         GatewayIntents.DirectMessageTyping |
                         GatewayIntents.GuildVoiceStates |
                         GatewayIntents.MessageContent;

GatewayClient client = new(new BotToken(token), new GatewayClientConfiguration
{
  Intents = intents,
  Logger = new ConsoleLogger(),
});

void WriteLog(string message)
{
    string logLinePrefix = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]:";
    Console.WriteLine($"{logLinePrefix}", message);
    streamWriter.WriteLine($"{logLinePrefix}", message);
}

// Send ready notification to console and create console output capture so console writes can be saved to a log file.
client.Ready += (args) =>
{
    WriteLog($"Created log file at {logPath}");
    WriteLog($"Bot connected as {args.User.Username}#{args.User.Discriminator}");
    return default;
};

// I think this should log messages sent into the server the bot is in?
client.MessageCreate += message =>
{
    WriteLog($"<[{message.Channel}:{message.Author.Username}]> {message.Content}");
    return default;
};

client.MessageReactionAdd += args =>
{
    WriteLog($"");
    return default;
};

await client.StartAsync();
await Task.Delay(-1);