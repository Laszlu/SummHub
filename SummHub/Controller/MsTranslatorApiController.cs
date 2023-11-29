/**********************************************************************************************************************/
// von : https://rapidapi.com/microsoft-azure-org-microsoft-cognitive-services/api/microsoft-translator-text
// Bearbeitet von Laszlo

using System.Globalization;
using System.Net.Http.Headers;
using Model.ApiData;
using Newtonsoft.Json;
using static Model.TranslatorConstants;

namespace Controller;

public class MsTranslatorApiController
{
    private HttpClient HttpClient { get; set; } = new();

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
            
            using var response = await HttpClient.SendAsync(request);
            try
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var translatorResponse = 
                    JsonConvert.DeserializeObject<MsTranslatorResponse[]>(body);
                return translatorResponse[0].Translations[0].Text;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
            return string.Empty;
        }
        
        return string.Empty;
        
        //TODO: Implement custom errors 
    }
}