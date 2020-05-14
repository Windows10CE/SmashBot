using System.Threading.Tasks;
using Discord.Commands;

namespace SmashBot.Modules
{
    public class Help : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        public async Task HelpAsync()
        {
            await ReplyAsync("Not yet");
        }
    }
}