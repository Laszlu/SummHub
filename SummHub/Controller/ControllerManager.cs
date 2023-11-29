/*********************************************************************************************************************/
// von Laszlo & David
using Model;
using LanguageDetection;
namespace Controller;

public class ControllerManager
{
    public ArticlesService ArticlesService { get; set; }
    private LanguageDetector Detector { get; set; }
    public MsTranslatorApiController Translator { get; set; }
    public NewsApiController NewsApi { get; set; }

    //TODO: new prop for every other api controller
    
    /*****************************************************************************************************************/
    /// Main Pipeline for loading Content
    public async Task<bool> LoadContent(IApiController apiController, Category category)
    {
        var hasLoaded = false;
        
        var allArticles = await GetNews(apiController, category);

        if (allArticles.Count > 0)
        {
            foreach (var article in allArticles)
            {
                var language = DetectLanguage(article);
                
                // Console.WriteLine(language);
                // Console.WriteLine(article);
                
                if (language != "en")
                {
                    var translatedArticle = await TranslateNews(article, language);

                    article.Title = translatedArticle.Title;
                    article.Description = translatedArticle.Description;
                }
                
                //Thread.Sleep(200);
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
    /*****************************************************************************************************************/
    
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

    private string DetectLanguage(NewsArticle article)
    {
        return Detector.Detect(article.Title);
    }

    private async Task<NewsArticle> TranslateNews(NewsArticle article, string language)
    {
        var translatedTitle = await Translator.Translate(article.Title);
        var translatedDescription = await Translator.Translate(article.Description);
        
        if (translatedTitle != null && translatedDescription != null)
        {
            article.Title = translatedTitle;
            article.Description = translatedDescription;
            return article;
        }

        article.Title = String.Empty;
        article.Description = String.Empty;
        return article;
    }

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
        Translator = new();
        NewsApi = new();

        Detector.AddAllLanguages();
    }
}

/*********************************************************************************************************************/