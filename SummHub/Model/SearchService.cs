/*********************************************************************************************************************/
// von Laszlo & David

using System.ComponentModel.DataAnnotations;
using static SummHub.Model.SearchServiceConstants;

namespace SummHub.Model;

public class SearchInputModel
{
    [MaxLength(20)]
    [RegularExpression(@"[\w\-\s]{0,20}$", ErrorMessage = InvalidSearch)]
    public string Query { get; set; }
}

public class SearchService
{
    public List<NewsArticle> FilterArticles(List<NewsArticle> articles, string? searchTerm)
    {
        if (!String.IsNullOrEmpty(searchTerm))
        {
            return articles.FindAll(a => a.Title.ToLower().Contains(searchTerm.ToLower()));
        }
        return articles;
    }
}
/*********************************************************************************************************************/