using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SmashBot.Services
{
    public class LogService
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;

        private const string _logDirectory = "Logs";
        private string _logFile => Path.Combine(_logDirectory, $"{DateTime.UtcNow.ToString("yyyy-MM-dd")}.log.txt");

        public LogService(DiscordSocketClient client, CommandService commands)
        {
            _client = client;
            _commands = commands;

            _client.Log += LogAsync;
            _commands.Log += LogAsync;
        }

        private async Task LogAsync(LogMessage msg)
        {
            if (!Directory.Exists(_logDirectory))
                Directory.CreateDirectory(_logDirectory);
            if (!File.Exists(_logFile))
                File.Create(_logFile);

            string logText = $"[{DateTime.UtcNow.ToString("hh:mm:ss")}] {msg.Source}: {msg.Exception?.ToString() ?? msg.Message}";
            await File.AppendAllTextAsync(_logFile, logText);

            await Console.Out.WriteLineAsync(logText);
        }
    }
}