/*********************************************************************************************************************/
// von David
namespace Model
{
    public class ArticlesService
    {
        public List<NewsArticle> TopStories { get; set; } = new ();

        public List<NewsArticle> Sports { get; set; } = new ();

        public List<NewsArticle> Politics { get; set; } = new ();

        public List<NewsArticle> Science { get; set; } = new ();

        public List<NewsArticle> Business { get; set; } = new ();

        public List<NewsArticle> Entertainment { get; set; } = new ();


        public void SaveArticles(List<NewsArticle> articleList, Category category)
        {

        }
    }
}