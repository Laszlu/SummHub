/*********************************************************************************************************************/
// von Laszlo und David

using Microsoft.AspNetCore.Components;
using Model;
using Model.ApiData;
using Newtonsoft.Json;
using static Model.NewsApiConstants;

namespace Controller;

public class NewsApiController : IApiController
{
    //TODO: try to fix injecting httpclient from program.cs
    public HttpClient HttpClient { get; set; } = new();

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
        var response =  HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, BuildQuery(category)));

        var json = await response;
        
        return json.Content.ReadAsStringAsync().Result;
    }

    public string? BuildQuery(Category category)
    {
        var queryString = "";

        var categoryString = GetCategory(category);

        if (categoryString != null)
        {
            queryString = $"{BaseUrlNewsApi}{TopStoriesNewsApi}?{categoryString}&{ApiKeyNewsApi}";
            return queryString;
        }
        
        return null;
    }
    
    public string? GetCategory(Category category)
    {
        switch (category)
        {
            case Category.TopStories:
                return GeneralCategoryNewsApi;
            case Category.Sports:
                return SportsCategoryNewsApi;
            case Category.Science:
                return ScienceCategoryNewsApi;
            case Category.Business:
                return BusinessCategoryNewsApi;
            case Category.Entertainment:
                return EntertainmentCategoryNewsApi;
            default:
                return null;
        }
    }
    
    // Function for testing api call
    /*public async Task<string> testfunc()
    {
        Console.WriteLine("apicontroller works");

        var result = await CallApi(Category.TopStories);

        return result;
    }*/
}

/*********************************************************************************************************************/