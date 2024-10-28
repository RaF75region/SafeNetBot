using System;
using SafeNetBot.Interfaces;

namespace SafeNetBot.Application.Services;

public class HostedSetviceTelegram : IHostedService, IDisposable
{
    private readonly ILogger<HostedSetviceTelegram> _logger;
    private readonly ITelegramBot _bot;

    public HostedSetviceTelegram(ILogger<HostedSetviceTelegram> logger, ITelegramBot bot)
    {
        _logger = logger;
        _bot = bot;
    }

    // This is called when the service starts
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Telegram bot is starting.");

        Task.Run(() => _bot.StartBot(), cancellationToken);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Bot is stopping.");
        return Task.CompletedTask;
    }

    public void Dispose()
    {

    }
}
