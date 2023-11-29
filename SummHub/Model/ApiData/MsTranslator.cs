/*********************************************************************************************************************/

namespace Model.ApiData;

public class MsTranslator
{
    public DetectedLanguage DetectedLanguage { get; set; }
    public Translation Translation { get; set; }
}


public class DetectedLanguage
{
    public string Language { get; set; }
    public int Score { get; set; }
}

public class Translation
{
    public string Text { get; set; }
    public string to { get; set; }  
}

/*[{
    "detectedLanguage": {
        "language": "es",
        "score": 0.99
    },
    "translations": [{
        "text": "Israel-Hamas War Live: Gaza Hostage Release, Deaths, News and More - CNN",
        "to": "en"
    }]
}]*/