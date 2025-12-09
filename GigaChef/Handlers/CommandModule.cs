using NetCord.Services.ApplicationCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaChef.Handlers
{
    public class CommandModule : ApplicationCommandModule<ApplicationCommandContext>
    {
        [SlashCommand("pong", "Test command, replies with Pong!")]
        public static string Pong() => "Ping!";
    }
}
