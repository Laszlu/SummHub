using System.Net.Http.Headers;

namespace Controller;

public class TranslatorApiController
{
    public HttpClient HttpClient { get; set; }
    
    
var client = new HttpClient();
var request = new HttpRequestMessage
{
    Method = HttpMethod.Post,
    RequestUri = new Uri("https://microsoft-translator-text.p.rapidapi.com/translate?to%5B0%5D=%3CREQUIRED%3E&api-version=3.0&profanityAction=NoAction&textType=plain"),
    Headers =
    {
        { "X-RapidAPI-Key", "af25d28a40msh05323456eb8d745p1f43fejsnc4353b6fc76b" },
        { "X-RapidAPI-Host", "microsoft-translator-text.p.rapidapi.com" },
    },
    Content = new StringContent("[\n    {\n        \"Text\": \"I would really like to drive your car around the block a few times.\"\n    }\n]")
    {
        Headers =
        {
            ContentType = new MediaTypeHeaderValue("application/json")
        }
    }
};
using (var response = await client.SendAsync(request))
{
    response.EnsureSuccessStatusCode();
    var body = await response.Content.ReadAsStringAsync();
    Console.WriteLine(body);
}
}