// See https://aka.ms/new-console-template for more information

using System.Text.Json.Serialization;
using Newtonsoft.Json;
using PsychologicalHelp;
using Telegram.Bot;
using JsonSerializer = System.Text.Json.JsonSerializer;

internal class Program
{
    public static async Task Main(string[] args)
    {
        var secretsJson = File.ReadAllText("AppSettings/secrets.json");
        var secrets = JsonConvert.DeserializeObject<Secrets>(secretsJson);
        var settingsJson = File.ReadAllText("AppSettings/app-settings.json");
        var settings = JsonConvert.DeserializeObject<AppSettings>(settingsJson);
        var botClient = new TelegramBotClient(secrets.ApiKeys.TelegramKey);
        var telegramBotController = new TelegramBotController(botClient);
        telegramBotController.StartBot();
        await Task.Delay(Timeout.Infinite);
    }
}