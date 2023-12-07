namespace SummHub.Controller;

public interface IMsTranslatorApiController
{
    Task<string?> Translate(string? textToTranslate);
}