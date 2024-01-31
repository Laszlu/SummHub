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
                
                if (detectedLanguage != language)
                {
                    var translatedArticle = await TranslateNews(article, translator, language);

                    article.Title = translatedArticle.Title;
                    article.Description = translatedArticle.Description;
                }
            }
            _articlesService.SaveArticles(allArticles, category);
            hasLoaded = true;
        }
        else
        {
            _errorController.Exception = new CustomException("API returned no articles", CustomExceptionType.UserAlert);
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
        
        if (translatedTitle != string.Empty)
        {
            article.Title = translatedTitle;
            article.Description = translatedDescription;
            return article;
        }
        else
        {
            _errorController.Exception = new CustomException("Translation failed", CustomExceptionType.UserAlert);
            return article;
        }
    }

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