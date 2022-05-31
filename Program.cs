using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram_bot
{
    internal  class Program
    {
       public static ITelegramBotClient Bot = new TelegramBotClient(TelegramBot.Token);
        static async Task Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var cancellationToken =  cts.Token;
            var receiverOptions = new ReceiverOptions
            {

                AllowedUpdates = { }
            };
            Bot.StartReceiving(
                TelegramBot.HandleUpdateAsync,
                TelegramBot.HandlePollingErrorAsync,
                receiverOptions,
                cancellationToken
                
                );
            Console.WriteLine("Bot is started...");
            Console.ReadLine();
        }
        

    }
}
