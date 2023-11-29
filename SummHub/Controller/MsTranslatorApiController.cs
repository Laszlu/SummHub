/**********************************************************************************************************************/
// von : https://rapidapi.com/microsoft-azure-org-microsoft-cognitive-services/api/microsoft-translator-text
// Bearbeitet von Laszlo

using System.Net.Http.Headers;
using static Model.TranslatorConstants;

namespace Controller;

public class MsTranslatorApiController
{
    private HttpClient HttpClient { get; set; } = new();

    public async Task<string?> Translate(string? textToTranslate)
    {
        string body;
        
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
            
            using var response = await HttpClient.SendAsync(request);
            try
            {
                response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();
                return body;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                body = string.Empty;
            }
            return body;
        }
        
        body = string.Empty;
        return body;
    }
}