using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace SmashBot.Services
{
    public class StartupService
    {
        private readonly DiscordSocketClient _client;
        private readonly IConfigurationRoot _config;
        private readonly Discord.Commands.CommandService _commands;
        private readonly IServiceProvider _provider;
        
        public StartupService(DiscordSocketClient client, IConfigurationRoot config, Discord.Commands.CommandService commands, IServiceProvider provider)
        {
            _client = client;
            _config = config;
            _commands = commands;
            _provider = provider;
        }
        
        public async Task StartupAsync()
        {
            string botToken = _config["Token"];
            if (string.IsNullOrWhiteSpace(botToken)) throw new ArgumentNullException("Token is missing");

            await _client.LoginAsync(Discord.TokenType.Bot, botToken);
            await _client.StartAsync();

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
        }
    }
}