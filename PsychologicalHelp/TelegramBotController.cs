using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PsychologicalHelp;

public class TelegramBotController
{
    private  readonly ITelegramBotClient _botClient;

    public TelegramBotController(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public void StartBot()
    {
       using var cts = new CancellationTokenSource();
        var options = new ReceiverOptions
        {
            AllowedUpdates = new[]
            {
                UpdateType.Message,
                UpdateType.CallbackQuery
            }
        };
        CreateCommandsKeyboard().WaitAsync(cts.Token);
        _botClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync, options, cancellationToken: cts.Token);
       
    }

    private async Task CreateCommandsKeyboard()
    {
       await _botClient.DeleteMyCommandsAsync();
       var commands = new[]
       {
           new BotCommand { Command = GlobalData.START, Description = "Начать работу" },
       };
        await _botClient.SetMyCommandsAsync(commands);
    }
    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message == null && update.CallbackQuery == null)
        {
            return;
        }

        var message = update.Message;
        var callbackQuery = update.CallbackQuery;
        if (message != null &&  message.Type != MessageType.Text)
        {
            return;
        }

        var userId = message?.From.Id ?? callbackQuery.From.Id;
        var messageId = message?.MessageId ?? callbackQuery.Message.MessageId;
        var messageText = message?.Text ?? callbackQuery?.Data;
    }

    private async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        Console.WriteLine(exception);
    }
}