using Model;

namespace Controller
{
    public class NewsApiController : IApiController
    {
        private string _BaseEndpoint;

        public string BaseEndpoint
        {
            get => _BaseEndpoint;
            set => _BaseEndpoint = value;
        }

        public List<NewsArticle> CallApi(Category category)
        {
            throw new NotImplementedException();
        }
    }
}