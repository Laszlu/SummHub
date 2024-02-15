/*********************************************************************************************************************/
// von David

namespace SummHub.Model
{

    public class ArticlesService
    {
        private List<NewsArticle> TopStories { get; set; } = new ();

        private List<NewsArticle> Sports { get; set; } = new ();

        private List<NewsArticle> Politics { get; set; } = new ();

        private List<NewsArticle> Science { get; set; } = new ();

        private List<NewsArticle> Business { get; set; } = new ();

        private List<NewsArticle> Entertainment { get; set; } = new ();


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
            }
        }

        public List<NewsArticle>? GetArticleList(Category category)
        {
            return category switch
            {
                Category.TopStories => TopStories,
                Category.Sports => Sports,
                Category.Politics => Politics,
                Category.Science => Science,
                Category.Business => Business,
                Category.Entertainment => Entertainment,
                _ => null
            };
        }
    }
}
/*********************************************************************************************************************/