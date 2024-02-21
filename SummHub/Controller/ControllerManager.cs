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
    /// <summary>Main Pipeline for loading all Content</summary>
    /// <param name="apiController">API controller to load data from</param>
    /// <param name="category">category to load</param>
    /// <param name="translator">API to translate articles</param>
    /// <param name="language">language to translate to</param>
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
    
    /// <summary>Get articles from specific API</summary>
    /// <param name="apiController"> API controller to load data from</param>
    /// <param name="category">category to load</param>
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

    /// <summary>Detect original language from article</summary>
    /// <param name="article">article for language check</param>
    public string DetectLanguage(NewsArticle article)
    {
        return _detector.Detect(article.Title);
    }

    /// <summary>Send article to translator and receive translation</summary>
    /// <param name="article">article to be translated</param>
    /// <param name="translator">translator used for translation</param>
    /// <param name="language">language for translation</param>
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