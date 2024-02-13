using LanguageDetection;
using SummHub.Controller;
using SummHub.Model;

namespace N3W5_UnitTests;

[TestClass]
public class ControllerManagerTests
{
    private static ControllerManager manager;
    
    [ClassInitialize]
    public static void Initialize(TestContext context)
    {
        manager = new(null, new LanguageDetector(), null);
    }
    
    [DataRow("This is a sentence in english.", "en")]
    [DataRow("Yo vivo en una casa grande", "es")]
    [DataRow("Ma grand-mère fait de la moto dans le poulailler", "fr")]
    [TestMethod]
    public void DetectLanguage_MultipleInputs_ReturnEqual(string sentence, string expectedLanguage)
    {
        NewsArticle testArticle = new NewsArticle
        {
            Title = sentence
        };

        var result = manager.DetectLanguage(testArticle);
        
        Assert.AreEqual(expectedLanguage, result);
    }
}



