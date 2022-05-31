using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.IO;
using Telegram.Bot.Types.InputFiles;
using VideoLibrary;

namespace Telegram_bot
{
    public static class TelegramBot
    {
        public static string Token = "5389418617:AAFsl2Sw09XwY1fhf-1r87fPtVbBOauEmPg";
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
           
            try
            {
                ChatId chatId = update.Message.Chat.Id;
                string userMessage = update.Message.Text.ToString();
                if (update.Message is Message message && update.Message.Text!=null)
                {
                    Show(message);
                    if (update.Message.Text.ToString() == "/start")
                    {
                        Start(botClient, update, cancellationToken);
                    }
                    else if (userMessage.Contains("you"))
                    {
                        await botClient.DeleteMessageAsync(chatId, update.Message.MessageId);

                        string link = update.Message.Text.ToString();
                       Message wait = await botClient.SendTextMessageAsync(chatId, "Downloading...");
                        string path =await YouTubeDownloader.DownloadVideo(link);
                        await SendVideo(botClient, update, cancellationToken,path);

                        await botClient.DeleteMessageAsync(chatId,wait.MessageId);


                    }
                    else
                    {
                        await botClient.SendTextMessageAsync(chatId, "Link Xato");
                    }

                }
            }
            catch (Exception)
            {

                
            }

            
            
        }

        public static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        public static async void Start(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            
            ChatId chatId = update.Message.Chat.Id;
            
                await botClient.SendTextMessageAsync(chatId: chatId, "Send YouTube link");
            


        }
        public static async Task SendVideo(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken,string path)
        {
            ChatId chatId = update.Message.Chat.Id;
            
            YouTube youTube = YouTube.Default;
            


                    FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                    InputOnlineFile inputOnlineFile = new InputOnlineFile(fileStream);
                    Message message1 = await botClient.SendVideoAsync(
                                                chatId: chatId,
                                                inputOnlineFile,
                                                caption: "Eng tezkor va bepul bot!",
                                                supportsStreaming: true,
                                                cancellationToken: cancellationToken);
                    System.IO.File.Delete(path);

                
            
        }

        public static void Show(Message message)
        {
            Console.WriteLine("----------------------");
            Console.WriteLine($"User: { message.From.FirstName+" "+ message.From.LastName} ({message.From.Username})");
            Console.WriteLine($"Message: {message.Text}");
        }
    }
}
