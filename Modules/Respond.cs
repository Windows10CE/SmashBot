using System.Threading.Tasks;
using Discord.Commands;

namespace SmashBot.Modules
{
    public class Respond : ModuleBase<SocketCommandContext>
    {
        [Command("respond")]
        [RequireOwner]
        public async Task RespondAsync([Remainder] string text)
        {
            await Context.Message.DeleteAsync();
            await ReplyAsync(text);
        }
    }
}