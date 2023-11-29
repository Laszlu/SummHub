/*********************************************************************************************************************/
// von David

namespace Model;

public class NewsApiConstants
{
    public const string BaseUrlNewsApi = "https://newsapi.org/v2/";
    public const string ApiKeyNewsApi = "apiKey=1980d7327a9948d4a97e0fafb45b3405";
    public const string TopStoriesNewsApi = "top-headlines";
    public const string SportsCategoryNewsApi = "category=sports";
    //public const string PoliticsCategoryNewsApi = "";
    public const string ScienceCategoryNewsApi = "category=science";
    public const string BusinessCategoryNewsApi = "category=business";
    public const string EntertainmentCategoryNewsApi = "category=entertainment";
    public const string GeneralCategoryNewsApi = "category=general";

}

public class TranslatorConstants
{
    public const string UriMsTranslatorApi =
        "https://microsoft-translator-text.p.rapidapi.com/translate?to%5B0%5D=%3CREQUIRED%3E&api-version=3.0&profanityAction=NoAction&textType=plain";
    
    public const string ApiKeyMsTranslator = "af25d28a40msh05323456eb8d745p1f43fejsnc4353b6fc76b";
    public const string ApiHostMsTranslator = "microsoft-translator-text.p.rapidapi.com";
    public const string MediaTypeMsTranslator = "application/json";
}



/*********************************************************************************************************************/