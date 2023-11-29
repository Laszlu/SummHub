/**********************************************************************************************************************/
// von : https://rapidapi.com/microsoft-azure-org-microsoft-cognitive-services/api/microsoft-translator-text
// Bearbeitet von Laszlo

using System.Net.Http.Headers;
using static Model.TranslatorConstants;

namespace Controller;

public class MsTranslatorApiController
{
    public HttpClient HttpClient { get; set; } = new();

    public async Task<string?> Translate(string? textToTranslate)
    {
        if (!string.IsNullOrEmpty(textToTranslate))
        {
            textToTranslate = textToTranslate.Replace(@"""", "*");
            Console.WriteLine(textToTranslate);
        
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{UriMsTranslatorApi}"),
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
        
            //--------------------
            //Console.WriteLine(request);

            using var response = await HttpClient.SendAsync(request);
            //needs try catch
            //Console.WriteLine(response);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            return body;
        }
        
        return String.Empty;
    }
}