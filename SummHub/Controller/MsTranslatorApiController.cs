/**********************************************************************************************************************/
// von : https://rapidapi.com/microsoft-azure-org-microsoft-cognitive-services/api/microsoft-translator-text
// Bearbeitet von Laszlo

using System.Net.Http.Headers;
using Newtonsoft.Json;
using SummHub.Model.ApiData;
using static SummHub.Model.TranslatorConstants;

namespace SummHub.Controller;

public class MsTranslatorApiController : IMsTranslatorApiController
{
    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;
    private readonly ErrorController _errorController;

    public async Task<string?> Translate(string? textToTranslate, string language)
    {
        if (!string.IsNullOrEmpty(textToTranslate))
        {
            textToTranslate = textToTranslate.Replace(@"""", "*");
            //Console.WriteLine(textToTranslate);
        
            var connectionStrings = _configuration.GetSection("ConnectionStrings");
            var key = connectionStrings["MSTranslator"];
            
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{UriMsTranslatorApiPart1}{language}{UriMsTranslatorApiPart2}"),
                Headers =
                {
                    { ApiKeyPropMsTranslator, key },
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
            
            using var response = await _client.SendAsync(request);
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
                _errorController.Exception = e;
            }

            _errorController.Exception = new Exception("Translation failed");
            return string.Empty;
        }
        
        return string.Empty;
        
        //TODO: Implement custom errors 
    }

    public MsTranslatorApiController(HttpClient injectedClient, IConfiguration injectedConfiguration, ErrorController errorController)
    {
        _client = injectedClient;
        _configuration = injectedConfiguration;
        _errorController = errorController;
    }
}