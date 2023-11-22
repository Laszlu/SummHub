using Model;

namespace Controller;

public class ApiCaller
{
    public string BaseUrl { get; set; }
    public string ApiKey { get; set; }
    public string? TopStories { get; set; }
    public string? SportsCategory { get; set; }
    public string? ScienceCategory { get; set; }
    public string? BusinessCategory { get; set; }
    public string? EntertainmentCategory { get; set; }

    public string CallApi(Category category)
    {
        var apiData = "";

        return apiData;
    }

    private string BuildQuery()
    {
        var queryString = "";

        return queryString;
    }
}