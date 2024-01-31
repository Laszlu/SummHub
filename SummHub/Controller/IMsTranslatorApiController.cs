namespace SummHub.Controller;

public interface IMsTranslatorApiController
{
   public Task<string?> Translate(string? textToTranslate, string language);
}