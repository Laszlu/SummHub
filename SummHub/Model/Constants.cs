/**********************************************************************************************************************/
// von David & Laszlo

namespace SummHub.Model;

public class NewsApiConstants
{
    public const string BaseUrlNewsApi = "https://newsapi.org/v2/";
    public const string ApiKeyNewsApi = "apiKey=";
    public const string TopStoriesNewsApi = "top-headlines";
    public const string SportsCategoryNewsApi = "category=sports";
    public const string ScienceCategoryNewsApi = "category=science";
    public const string BusinessCategoryNewsApi = "category=business";
    public const string EntertainmentCategoryNewsApi = "category=entertainment";
    public const string GeneralCategoryNewsApi = "category=general";
    public const string PageLimit = "pageSize=5";

}

/**********************************************************************************************************************/

public class TranslatorConstants
{
    public const string UriMsTranslatorApiPart1 = 
        "https://microsoft-translator-text.p.rapidapi.com/translate?to%5B0%5D=";
    public const string UriMsTranslatorApiPart2 = 
        "&api-version=3.0&profanityAction=NoAction&textType=plain";
    public const string ApiKeyPropMsTranslator = "X-RapidAPI-Key";
    public const string ApiHostMsTranslator = "microsoft-translator-text.p.rapidapi.com";
    public const string ApiHostPropMsTranslator = "X-RapidAPI-Host";
    public const string MediaTypeMsTranslator = "application/json";
}

/**********************************************************************************************************************/

public class SearchServiceConstants
{
    public const string InvalidSearch = "Search contains invalid symbols";
}

/**********************************************************************************************************************/