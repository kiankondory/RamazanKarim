using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

class Program
{
    static string token = "8730575940:AAH3wdgopG1-_jeIT_MXF9tqxFjsGLUPWHI";

    static async Task Main(string[] args)
    {
        var bot = new TelegramBotClient(token);

        var cts = new CancellationTokenSource();
        try
        {
            ReceiverOptions receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }
            };

            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken: cts.Token
            );

            Console.WriteLine("Bot started...");
            Console.ReadLine();
        }
        finally
        {
            cts.Dispose();
        }
    }

    static async Task HandleUpdateAsync(
        ITelegramBotClient bot,
        Update update,
        CancellationToken cancellationToken)
    {

        if (update.Message?.Text == "/start")
        {
            var keyboard = new InlineKeyboardMarkup(new[]
            {
                new [] { InlineKeyboardButton.WithCallbackData("🇰🇿 Kazakhstan","kz") },
                new [] { InlineKeyboardButton.WithCallbackData("🇹🇯 Tajikistan","tj") },
                new [] { InlineKeyboardButton.WithCallbackData("🇮🇷 Iran","ir") },
                new [] { InlineKeyboardButton.WithCallbackData("🇷🇺 Russia","ru") }
            });

            await bot.SendMessage(
                chatId: update.Message.Chat.Id,
                text: "🌍 لطفا کشور خود را انتخاب کنید",
                replyMarkup: keyboard
            );
        }
    }

    static Task HandleErrorAsync(
        ITelegramBotClient bot,
        Exception exception,
        CancellationToken cancellationToken)
    {
        Console.WriteLine(exception.Message);
        return Task.CompletedTask;
    }
}