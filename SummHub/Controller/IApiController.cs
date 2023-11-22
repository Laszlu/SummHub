/*********************************************************************************************************************/
// von Laszlo

using Model;

namespace Controller;

public interface IApiController
{
    public List<NewsArticle> ConvertJsonToList(Category category);
}

/*********************************************************************************************************************/