/*********************************************************************************************************************/
// von Laszlo und David

using Newtonsoft.Json;
using SummHub.Model;
using SummHub.Model.ApiData;
using static SummHub.Model.NewsApiConstants;

namespace SummHub.Controller;

public class NewsApiController : IApiController
{
    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;

    public async Task<List<NewsArticle>> GetData(Category category)
    {
        var articles = new List<NewsArticle>();

        string jsonFromApi =  await CallApi(category);

        NewsApiResponse? apiResponse = JsonConvert.DeserializeObject<NewsApiResponse>(jsonFromApi);

        if (apiResponse.Status == Statuses.Ok)
        {
            foreach (var newsApiArticle in apiResponse.Articles)
            {
                articles.Add(new NewsArticle()
                {
                    Title = newsApiArticle.Title,
                    Description = newsApiArticle.Description,
                    Author = newsApiArticle.Author,
                    Url = newsApiArticle.Url,
                    ImageUrl = newsApiArticle.UrlToImage,
                    Published = newsApiArticle.PublishedAt
                });
            }
        }

        return articles;
    }

    public async Task<string> CallApi(Category category)
    {
        var response =  _client.SendAsync(new HttpRequestMessage(HttpMethod.Get, BuildQuery(category)));

        var json = await response;
        
        return json.Content.ReadAsStringAsync().Result;
    }

    public string? BuildQuery(Category category)
    {
        var connectionStrings = _configuration.GetSection("ConnectionStrings");
        var key = connectionStrings["GoogleNews"];
        
        var categoryString = GetCategory(category);

        if (categoryString != null)
        {
            var queryString = $"{BaseUrlNewsApi}{TopStoriesNewsApi}?{categoryString}&{ApiKeyNewsApi}{key}";
            return queryString;
        }
        
        return null;
    }
    
    public string? GetCategory(Category category)
    {
        return category switch
        {
            Category.TopStories => GeneralCategoryNewsApi,
            Category.Sports => SportsCategoryNewsApi,
            Category.Science => ScienceCategoryNewsApi,
            Category.Business => BusinessCategoryNewsApi,
            Category.Entertainment => EntertainmentCategoryNewsApi,
            _ => null
        };
    }
    
    // Function for testing api call
    /*public async Task<string> testfunc()
    {
        Console.WriteLine("apicontroller works");

        var result = await CallApi(Category.TopStories);

        return result;
    }*/

    public NewsApiController(HttpClient injectedClient, IConfiguration injectedConfiguration)
    {
        _client = injectedClient;
        _configuration = injectedConfiguration;
    }
}

/*********************************************************************************************************************/