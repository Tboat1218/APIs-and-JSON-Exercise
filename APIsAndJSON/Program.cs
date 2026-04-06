using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

class Program
{
    static async Task Main(string[] args)
    {
        var client = new HttpClient();

        for (int i = 0; i < 5; i++)
        {
            // Ron Swanson API
            var ronResponse = await client.GetStringAsync("https://ron-swanson-quotes.herokuapp.com/v2/quotes");
            var ronQuote = JArray.Parse(ronResponse).ToString()
                .Replace("[", "")
                .Replace("]", "")
                .Replace("\"", "")
                .Trim();

            Console.WriteLine($"Ron: {ronQuote}");

            // Kanye API
            var kanyeResponse = await client.GetStringAsync("https://api.kanye.rest");
            var kanyeJson = JObject.Parse(kanyeResponse);
            var kanyeQuote = kanyeJson["quote"].ToString();

            Console.WriteLine($"Kanye: {kanyeQuote}");
            Console.WriteLine();
        }
    }
}