using SafeNetBot.Application.Services;
using SafeNetBot.Infastructure;
using SafeNetBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<HostedSetviceTelegram>();
builder.Services.AddSingleton<ITelegramBot, TelegramBot>();
builder.Services.AddSingleton<Update>();
builder.Services.AddSingleton(
    provider =>
    {
        var configuration = provider.GetRequiredService<IConfiguration>();
        return new TelegramBotClient(configuration.GetSection("Telegram:Token").Value!);
    }
);

builder.Services.AddHttpClient("tgwebhook").RemoveAllLoggers().AddTypedClient(httpClient => new TelegramBotClient(builder.Configuration.GetSection("Telegram:Token").Value!, httpClient));

var app = builder.Build();

app.MapGet("/bot/setWebhook", async (TelegramBotClient bot) => { await bot.SetWebhookAsync(builder.Configuration.GetSection("Telegram:BotWebhookUrl").Value!); return $"Webhook set to {builder.Configuration.GetSection("Telegram:BotWebhookUrl").Value!}"; });
app.MapPost("/bot", OnUpdate);


async void OnUpdate(TelegramBotClient bot, Update update)
{
    if (update.Message is null) return;			// we want only updates about new Message
    if (update.Message.Text is null) return;	// we want only updates about new Text Message
    var msg = update.Message;
    Console.WriteLine($"Received message '{msg.Text}' in {msg.Chat}");
    await bot.SendTextMessageAsync(msg.Chat, $"{msg.From} said: {msg.Text}");
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();