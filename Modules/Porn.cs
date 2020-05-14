using System.Threading.Tasks;
using Discord.Commands;

namespace SmashBot.Modules
{
    public class Porn : ModuleBase<SocketCommandContext>
    {
        [Command("porn")]
        public async Task PornAsync()
        {
            await ReplyAsync("no !");
        }
    }
}