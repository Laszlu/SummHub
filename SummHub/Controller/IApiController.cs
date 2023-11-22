/*********************************************************************************************************************/
// von Laszlo

using Model;

namespace Controller
{
    public interface IApiController
    {
        public string BaseEndpoint { get; set; }

        public List<NewsArticle> CallApi(Category category);
    }
}

/*********************************************************************************************************************/