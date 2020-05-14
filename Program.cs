using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmashBot.Services;

namespace SmashBot
{
    internal class Program
    {
        public static async Task Main()
        {
            await Console.Out.WriteLineAsync("Starting SmashBot...");
            
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("settings.json")
                .Build();
            
            ServiceProvider provider = new ServiceCollection()
                .AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
                {
                    LogLevel = LogSeverity.Debug,
                    MessageCacheSize = 50
                }))
                .AddSingleton(new Discord.Commands.CommandService(new CommandServiceConfig
                {
                    DefaultRunMode = RunMode.Async
                }))
                .AddSingleton(config)
                .AddSingleton<StartupService>()
                .AddSingleton<CommandHandler>()
                .AddSingleton<LogService>()
                .BuildServiceProvider();

            provider.GetRequiredService<LogService>();
            await provider.GetRequiredService<StartupService>().StartupAsync();
            provider.GetRequiredService<CommandHandler>();

            await Task.Delay(-1);
        }
    }
}