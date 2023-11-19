/*********************************************************************************************************************/
// von Laszlo & David

using Model;
using static Model.Category;
using LanguageDetection;

namespace Controller
{
    public class ControllerManager
    {
        // props??
        private ArticlesService articlesService;

        private LanguageDetector detector;

        private void LoadContent(IApiController apiController, Category category)
        {
            var allArticles = GetNews(apiController, category);

            if (allArticles.Count > 0)
            {
                foreach (var article in allArticles)
                {
                    var language = DetectLanguage(article);

                    // Uncomment when translation is implemented
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

                articlesService.SaveArticles(allArticles, category);
            }
        }

        private List<NewsArticle> GetNews(IApiController apiController, Category category)
        {
            List<NewsArticle> articles = new();

            try
            {
                articles = apiController.CallApi(category);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return articles;
        }

        private string DetectLanguage(NewsArticle article)
        {
            return detector.Detect(article.Title);
        }

        // private NewsArticle TranslateNews(NewsArticle article, string language)
        // {
        //     //Translation API not implemented yet
        // }

        public ControllerManager()
        {
            articlesService = new();
            detector = new();

            detector.AddAllLanguages();
        }
    }
}

/*********************************************************************************************************************/