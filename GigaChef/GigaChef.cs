using DotNetEnv;
using GigaChef.Services;
using NetCord;
using NetCord.Gateway;
using NetCord.Logging;
using NetCord.Rest;
using GigaChef;

internal class Program
{
    private static async Task Main()
    {
        // Load Discord API token from .env file in order to authenticate bot
        Env.Load();
        string? token = Environment.GetEnvironmentVariable("TOKEN");
        if (string.IsNullOrEmpty(token)) // Check if token is null, this will prevent runtime errors.
        {
            Console.WriteLine("Error: Token was not located in environment variable");
            return;
        }
        var bot = new DiscordBot(token);
        await bot.StartAsync();
    }
}