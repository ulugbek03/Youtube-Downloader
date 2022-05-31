using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using VideoLibrary;

namespace Telegram_bot
{
    public static class YouTubeDownloader
    {

        public static async Task <string> DownloadVideo(string link)
        {
            try
            {
                
                    string path = @"G:";
                    YouTube youTube = YouTube.Default;
                    YouTubeVideo video = youTube.GetVideo(link);

                    byte[] file = video.GetBytes();
                    string fileName = Path.Combine(path, $"{video.Title}{video.FileExtension}");
                    System.IO.File.WriteAllBytes(fileName, file);

                    string fi = Path.Combine(@"G:", $"{video.Title}{video.FileExtension}");

                    Console.WriteLine(fi);

                    return fi;

                
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
            return null;

        }
    } 
}
