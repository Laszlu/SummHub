/*********************************************************************************************************************/
// von Laszlo & David

using LanguageDetection;
using SummHub.Model;

namespace SummHub.Controller;

public class ControllerManager
{
    private readonly ArticlesService _articlesService;
    private readonly LanguageDetector _detector;
    private readonly ErrorController _errorController;
    
    public Languages Languages { get; set; }

    public string CurrentLanguage { get; set; } = "en";
    public string PreviousLanguage { get; set; } = "en";

    public Category CurrentCategory { get; set; } = Category.TopStories;
    
    /*****************************************************************************************************************/
    /// Main Pipeline for loading all Content
    public async Task<bool> LoadContent(IApiController apiController, Category category, 
        ITranslatorApiController translator, string language)
    {
        bool hasLoaded;
        
        var allArticles = await GetNews(apiController, category);

        if (allArticles.Count > 0)
        {
            foreach (var article in allArticles)
            {
                var detectedLanguage = DetectLanguage(article);
                
                // Console.WriteLine(language);
                // Console.WriteLine(article);
                
                if (detectedLanguage != language)
                {
                    var translatedArticle = await TranslateNews(article, translator, language);

                    article.Title = translatedArticle.Title;
                    article.Description = translatedArticle.Description;
                }
            }
            //-----------------//
            //Console.WriteLine(allArticles.First().ImageUrl); 
            //-----------------//
            _articlesService.SaveArticles(allArticles, category);
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
            _errorController.Exception = new CustomException(ex, CustomExceptionType.DevError);
            Console.WriteLine(ex.Message);
        }

        return articles;
    }

    private string DetectLanguage(NewsArticle article)
    {
        return _detector.Detect(article.Title);
    }

    private async Task<NewsArticle> TranslateNews(NewsArticle article, ITranslatorApiController translator, string language)
    {
        var translatedTitle = await translator.Translate(article.Title, language);
        var translatedDescription = await translator.Translate(article.Description, language);
        
        if (translatedTitle != string.Empty && translatedDescription != string.Empty)
        {
            article.Title = translatedTitle;
            article.Description = translatedDescription;
            return article;
        }
        
        _errorController.Exception = new CustomException("Translation failed", CustomExceptionType.UserAlert);
        return article;
    }

    // Function for testing api call
    /*public async Task<string> testfunc()
    {
        Console.WriteLine("manager works");
        var result = await NewsApi.testfunc();
        return result;
    }*/

    public ControllerManager(ArticlesService articlesService, LanguageDetector detector, ErrorController errorController)
    {
        _articlesService = articlesService;
        _errorController = errorController;
        _detector = detector;
        _detector.AddAllLanguages();
        
        Languages = new();
    }
}

/*********************************************************************************************************************/