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
            switch (category)
            {
                case Category.TopStories : TopStories = articleList;
                    break;
                case Category.Sports: Sports = articleList;
                    break;
                case Category.Politics: Politics = articleList;
                    break;
                case Category.Science: Science = articleList;
                    break;
                case Category.Business: Business = articleList;
                    break;
                case Category.Entertainment: Entertainment = articleList;
                    break;
                default: break;
            }
        }
    }
}