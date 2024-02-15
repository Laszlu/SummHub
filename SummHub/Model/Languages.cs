/*********************************************************************************************************************/
// von Laszlo

namespace SummHub.Model;

public class Languages
{
    public Dictionary<string, Tuple<string, string>> LanguageDictionary = new()
    {
        { "English", Tuple.Create("English", "en") },
        { "Spanish", Tuple.Create("Español", "es") },
        { "Simplified Chinese", Tuple.Create("繁體中文 (繁體)", "zh-Hans")},
        { "Japanese", Tuple.Create("日本語", "ja") },
        { "French", Tuple.Create("Français", "fr") },
        { "Arabic", Tuple.Create("العربية", "ar") },
        { "German", Tuple.Create("Deutsch", "de") },
        { "Russian", Tuple.Create("Русский", "ru") },
        { "Hindi", Tuple.Create("हनद", "hi") },
        { "Turkish", Tuple.Create("Türkçe", "tr") },
        { "Klingon", Tuple.Create("Klingon (pIqaD)", "tlh-Piqd") }
    };
}
/*********************************************************************************************************************/