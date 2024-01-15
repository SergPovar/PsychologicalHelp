using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PsychologicalHelp.Services;

public class SubscriptionService
{ 
    public ChatId SubscribeChatId { get; }
    private ITelegramBotClient _botClient;
    private List<ChatMemberStatus> _allowedStatuses = new ()  {
        ChatMemberStatus.Member, ChatMemberStatus.Administrator, ChatMemberStatus.Creator
    };

    public SubscriptionService(ITelegramBotClient botClient, ChatId subscribeChatId)
    {
        _botClient = botClient;
    }

    public async Task<bool>IsSubscribed(long userId)
    {
        
        try
        {
          var member =  await _botClient.GetChatMemberAsync(SubscribeChatId, userId);
          if (_allowedStatuses.Contains(member.Status))
          {
              return true;
          }
        }
        catch ( ApiRequestException e)
        {
            Console.WriteLine(e);
            return false;
        }

        return false;
    }
}