/*********************************************************************************************************************/

namespace Model
{
    public class ArticlesService
    {
        public List<NewsArticle> TopStories { get; set; }

        public List<NewsArticle> Sports { get; set; }

        public List<NewsArticle> Politics { get; set; }

        public List<NewsArticle> Science { get; set; }

        public List<NewsArticle> Business { get; set; }

        public List<NewsArticle> Entertainment { get; set; }


        public void SaveArticles(List<NewsArticle> articleList, Category category)
        {

        }
    }
}