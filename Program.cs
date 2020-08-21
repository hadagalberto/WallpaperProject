using RestSharp;
using System;
using System.IO;
using System.Net;
using System.Text.Json;

namespace WallPaper_Project
{
    class Program
    {
        static void Main(string[] args)
        {

            var config = JsonSerializer.Deserialize<Config>(System.IO.File.ReadAllText("config.json"));

            var client = new RestClient("https://wall.alphacoders.com/api2.0/get.php");

            var request = new RestRequest("/");
            request.AddParameter("auth", "b9eca2be962d4524ce3e8944877edf4a");
            request.AddParameter("method", "search");
            request.AddParameter("width", config.width);
            request.AddParameter("height", config.height);
            request.AddParameter("term", config.tema);
            if(!config.only_resolution)
                request.AddParameter("operator", "min");

            var response = client.Get(request);
            var obj = JsonSerializer.Deserialize<ApiResponse>(response.Content);
            if (!obj.success)
                Environment.Exit(0);

            var total = Convert.ToInt32(obj.total_match);
            var pages = total / 30;
            if (total % 30 != 0)
                pages++;

            int page = new Random().Next(pages);

            if(page > 1)
            {
                request.AddParameter("page", page);
                response = client.Get(request);
                obj = JsonSerializer.Deserialize<ApiResponse>(response.Content);
                if (!obj.success)
                    Environment.Exit(0);
            }

            using(var webClient = new WebClient())
            {
                var wall = obj.wallpapers[new Random().Next(obj.wallpapers.Count)];
                webClient.DownloadFile(wall.url_image, $"image.{wall.file_type}");
                var path = Path.GetFullPath($"image.{wall.file_type}");
                Wallpaper.Style style = 
                    wall.height == "1080" &&
                    wall.width == "2560" ? 
                    Wallpaper.Style.Centered : Wallpaper.Style.Stretched;
                Wallpaper.Set(path, style);
            }
            //Console.WriteLine(response.Content);
        }
    }
}
