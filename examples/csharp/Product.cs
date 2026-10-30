// Oscaro Product API – C# example (.NET 6+, top-level statements)
// Usage: dotnet new console -n Piloterr && copy this file to Program.cs
//        PILOTERR_API_KEY=your-key dotnet run
using System.Text.Json;

var apiKey = Environment.GetEnvironmentVariable("PILOTERR_API_KEY")
    ?? throw new InvalidOperationException("Set PILOTERR_API_KEY");

using var http = new HttpClient { Timeout = TimeSpan.FromSeconds(60) };
http.DefaultRequestHeaders.Add("x-api-key", apiKey);

var query =
    "query=" + Uri.EscapeDataString("https://www.oscaro.com/huile-moteur-motul-102208-15213891-1862-p");

var response = await http.GetAsync("https://api.piloterr.com/v2/oscaro/product?" + query);
var body = await response.Content.ReadAsStringAsync();
if (!response.IsSuccessStatusCode)
{
    Console.Error.WriteLine($"Piloterr error {(int)response.StatusCode}: {body}");
    return;
}

using var doc = JsonDocument.Parse(body);
Console.WriteLine(JsonSerializer.Serialize(doc, new JsonSerializerOptions { WriteIndented = true }));
