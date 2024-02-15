/*********************************************************************************************************************/
// von Laszlo & David

namespace SummHub.Model.ApiData;

public class MsTranslatorResponse
{
    public DetectedLanguage DetectedLanguage { get; set; }
    public Translation[] Translations { get; set; }
}


public class DetectedLanguage
{
    public string Language { get; set; }
    public float Score { get; set; }
}

public class Translation
{
    public string Text { get; set; }
    public string To { get; set; }  
}
/*********************************************************************************************************************/