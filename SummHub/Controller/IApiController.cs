/*********************************************************************************************************************/
// von Laszlo

using SummHub.Model;

namespace SummHub.Controller;

public interface IApiController
{
    public Task<List<NewsArticle>> GetData(Category category);

    public Task<string> CallApi(Category category);

    public string? BuildQuery(Category category);

    public string? GetCategory(Category category);
}

/*********************************************************************************************************************/