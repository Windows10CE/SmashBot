using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace SmashBot.Services
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly Discord.Commands.CommandService _commands;
        private readonly IServiceProvider _provider;

        public CommandHandler(DiscordSocketClient client, Discord.Commands.CommandService commands, IServiceProvider provider)
        {
            _client = client;
            _commands = commands;
            _provider = provider;
            
            _client.MessageReceived += ClientOnMessageReceived;
        }

        private async Task ClientOnMessageReceived(SocketMessage arg)
        {
            var msg = arg as SocketUserMessage;
            if (msg?.Author.IsBot ?? true) return;
            
            var context = new SocketCommandContext(_client, msg);

            int argPos = 0;
            if (msg.HasCharPrefix('&', ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos, _provider);

                if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                    await context.Channel.SendMessageAsync(result.ErrorReason);
                else if (result.Error == CommandError.UnknownCommand)
                    await context.Channel.SendMessageAsync("That command doesn't exist, please use ``&help`` to see all commands.");
            }
        }
    }
}