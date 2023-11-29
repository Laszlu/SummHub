using System.Net.Http.Headers;
using static Model.TranslatorConstants;

namespace Controller;

public class MsTranslatorApiController
{
    public HttpClient HttpClient { get; set; } = new();

    public async Task<string?> Translate(string textToTranslate)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(UriMsTranslatorApi),
            Headers =
            {
                { ApiKeyPropMsTranslator, ApiKeyMsTranslator },
                { ApiHostPropMsTranslator, ApiHostMsTranslator },
            },
            Content = new StringContent($"[{{\"Text\":\"{textToTranslate}\"}}]")
            {
                Headers =
                {
                    ContentType = new MediaTypeHeaderValue(MediaTypeMsTranslator)
                }
            }
        };

        using var response = await HttpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        return body;
    }
}