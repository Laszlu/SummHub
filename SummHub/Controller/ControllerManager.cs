/*********************************************************************************************************************/
// von Laszlo & David

using LanguageDetection;
using SummHub.Model;

namespace SummHub.Controller;

public class ControllerManager
{
    private ArticlesService ArticlesService { get; }
    private LanguageDetector Detector { get; }
    private MsTranslatorApiController Translator { get; }
    
    /*****************************************************************************************************************/
    /// Main Pipeline for loading Content
    public async Task<bool> LoadContent(IApiController apiController, Category category)
    {
        bool hasLoaded;
        
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
                    var translatedArticle = await TranslateNews(article);

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
    
    private async Task<List<NewsArticle>> GetNews(IApiController apiController, Category category)
    {
        List<NewsArticle> articles = new();

        try
        {
            articles = await apiController.GetData(category);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return articles;
    }

    private string DetectLanguage(NewsArticle article)
    {
        return Detector.Detect(article.Title);
    }

    private async Task<NewsArticle> TranslateNews(NewsArticle article)
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

    public ControllerManager(ArticlesService articlesService, NewsApiController newsApi,
        MsTranslatorApiController translator, LanguageDetector detector)
    {
        ArticlesService = articlesService;
        Detector = detector;
        Translator = translator;

        Detector.AddAllLanguages();
    }
}

/*********************************************************************************************************************/