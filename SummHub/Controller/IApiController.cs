/*********************************************************************************************************************/
// von Laszlo

using Model;

namespace Controller
{
    interface IApiController
    {
        public string BaseEndpoint { get; set; }

        public List<NewsArticle> CallApi(Category category);
    }
}

/*********************************************************************************************************************/