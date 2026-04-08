using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;

class Program
{
    static async Task Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var client = new HttpClient();

        string apiKey = config["OpenWeatherMap:ApiKey"]!;
        string city = "Columbus";

        string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=imperial";

        var weatherResponse = await client.GetStringAsync(url);
        var weatherJson = JObject.Parse(weatherResponse);

        string cityName = weatherJson["name"]?.ToString();
        string description = weatherJson["weather"]?[0]?["description"]?.ToString();
        string temperature = weatherJson["main"]?["temp"]?.ToString();
        string feelsLike = weatherJson["main"]?["feels_like"]?.ToString();
        string humidity = weatherJson["main"]?["humidity"]?.ToString();
        string windSpeed = weatherJson["wind"]?["speed"]?.ToString();

        Console.WriteLine("==================================");
        Console.WriteLine("        CURRENT WEATHER");
        Console.WriteLine("==================================");
        Console.WriteLine($" City:        {cityName}");
        Console.WriteLine($" Forecast:    {description}");
        Console.WriteLine($" Temp:        {temperature}°F");
        Console.WriteLine($" Feels Like:  {feelsLike}°F");
        Console.WriteLine($" Humidity:    {humidity}%");
        Console.WriteLine($" Wind Speed:  {windSpeed} mph");
        Console.WriteLine("==================================");
    }
}
