using Microsoft.Extensions.Configuration;
using SummHub.Controller;
using SummHub.Model;

namespace N3W5_UnitTests;

[TestClass]
public class NewsApiControllerTests
{
    private static NewsApiController controller;

    [ClassInitialize]
    public static void Initialize(TestContext context)
    {
        var testConfig = new Dictionary<string, string>
        {
            { "ConnectionStrings:MSTranslator", "TestConntectionString" },
            { "ConnectionStrings:GoogleNews", "TestConntectionString" }
        };

        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(testConfig).Build();
        
        controller = new(null, config, null);
    }
    
    [DataRow(Category.Entertainment, "https://newsapi.org/v2/top-headlines?pageSize=5&category=entertainment&apiKey=TestConntectionString")]
    [DataRow(Category.Science, "https://newsapi.org/v2/top-headlines?pageSize=5&category=science&apiKey=TestConntectionString")]
    [DataRow(Category.TopStories, "https://newsapi.org/v2/top-headlines?pageSize=5&category=general&apiKey=TestConntectionString")]
    [TestMethod]
    public void BuildQuery_InputCategory_ReturnEqual(Category category, string expectedResult)
    {
        var actualResult = controller.BuildQuery(category);
        
        Assert.AreEqual(expectedResult, actualResult);
    }
}