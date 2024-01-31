namespace SummHub.Controller;

public interface ITranslatorApiController
{
   public Task<string?> Translate(string? textToTranslate, string language);
}