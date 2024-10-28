using SafeNetBot.Application.Commands;
using SafeNetBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace SafeNetBot.Infastructure;

public class TelegramBot : ITelegramBot
{
    private readonly TelegramBotClient _bot;

    public TelegramBot(TelegramBotClient bot)
    {
        _bot = bot;
    }

    public Task StartBot()
    {
        using var cts = new CancellationTokenSource();
        _bot.OnMessage += OnMessage;
        // _bot.O

        Console.ReadLine();
        cts.Cancel();
        return Task.CompletedTask;
    }

    async Task OnMessage(Message msg, UpdateType type)
    {
       await MessageCommand.SendMessageAsync(_bot, msg, type);
    }
}