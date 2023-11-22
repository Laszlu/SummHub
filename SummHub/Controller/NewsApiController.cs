using Model;
using static Model.NewsApiConstants;

namespace Controller
{
    public class NewsApiController : IApiController
    {
        private ApiCaller Caller { get; set; } = new()
        {
            BaseUrl = BaseUrlNewsApi,
            ApiKey = ApiKeyNewsApi,
            TopStories = TopStoriesCategoryNewsApi,
            SportsCategory = SportsCategoryNewsApi,
            ScienceCategory = ScienceCategoryNewsApi,
            BusinessCategory = BusinessCategoryNewsApi,
            EntertainmentCategory = EntertainmentCategoryNewsApi
        };

        public List<NewsArticle> ConvertJsonToList(Category category)
        {
            var articles = new List<NewsArticle>();
            var jsonString = Caller.CallApi(category);

            return articles;
        }
    }
}