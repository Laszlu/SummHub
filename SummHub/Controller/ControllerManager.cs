/*********************************************************************************************************************/
// von Laszlo & David

using Model;
using static Model.Category;
using LanguageDetection;

namespace Controller;

public class ControllerManager
{
    public ArticlesService ArticlesService { get; set; }

    private LanguageDetector Detector { get; set; }

    public NewsApiController NewsApi;

    //TODO: new prop for every other api controller

    public async Task<bool> LoadContent(IApiController apiController, Category category)
    {
        var hasLoaded = false;
        
        var allArticles = await GetNews(apiController, category);

        if (allArticles.Count > 0)
        {
            foreach (var article in allArticles)
            {
                var language = DetectLanguage(article);

                //TODO: Uncomment when translation is implemented
                // if (language != null)
                // {
                //     if (language != "en")
                //     {
                //         var translatedArticle = TranslateNews(article, language);

                //         article.Title = translatedArticle.Title;
                //         article.Description = translatedArticle.Description;
                //     }
                // }
                // else
                // {
                //     article.Title = "";
                //     article.Description = "";
                // }
            }

            ArticlesService.SaveArticles(allArticles, category);
            hasLoaded = true;
        }
        else
        {
            // TODO: Error Response for UI
            hasLoaded = false;
        }

        return hasLoaded;
    }

    public async Task<List<NewsArticle>> GetNews(IApiController apiController, Category category)
    {
        List<NewsArticle> articles = new();

        try
        {
            articles = await apiController.GetData(category);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return articles;
    }

    public string DetectLanguage(NewsArticle article)
    {
        return Detector.Detect(article.Title);
    }

    // private NewsArticle TranslateNews(NewsArticle article, string language)
    // {
    //     //TODO:Translation API not implemented yet
    // }

    // Function for testing api call
    /*public async Task<string> testfunc()
    {
        Console.WriteLine("manager works");
        var result = await NewsApi.testfunc();
        return result;
    }*/

    public ControllerManager()
    {
        ArticlesService = new();
        Detector = new();

        NewsApi = new();

        Detector.AddAllLanguages();
    }
}

/*********************************************************************************************************************/