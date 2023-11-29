// von: https://github.com/News-API-gh/News-API-csharp/tree/master

namespace SummHub.Model.ApiData;

public enum Statuses
{
    Ok,
    Error
}

public enum ErrorCodes
{
    ApiKeyExhausted,
    ApiKeyMissing,
    ApiKeyInvalid,
    ApiKeyDisabled,
    ParametersMissing,
    ParametersIncompatible,
    ParameterInvalid,
    RateLimited,
    RequestTimeout,
    SourcesTooMany,
    SourceDoesNotExist,
    SourceUnavailableSortedBy,
    SourceTemporarilyUnavailable,
    UnexpectedError,
    UnknownError
}

public class Source
{
    public string Id { get; set; }
    public string Name { get; set; }
}

public class NewsApiArticle
{
    public Source Source { get; set; }
    public string Author { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
    public string UrlToImage { get; set; }
    public DateTime? PublishedAt { get; set; }
}

public class NewsApiResponse
{
    public Statuses Status { get; set; }
    public ErrorCodes? Code { get; set; }
    public string Message { get; set; }
    public List<NewsApiArticle> Articles { get; set; }
    public int TotalResults { get; set; }
}