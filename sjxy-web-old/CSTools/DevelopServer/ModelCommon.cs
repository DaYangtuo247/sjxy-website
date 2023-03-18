using System.Collections.Generic;

namespace DevelopServer;

public static class ModelCommon
{
    public static Dictionary<string, object> CreateNews(string lineId, string showTitle, string showDate, bool hasPic = false, bool isNew = false)
    {
        return new()
        {
            { "lineID", lineId },
            { "lineShowStyle", "" },
            { "isNew", isNew },
            { "picUrl", hasPic ? "/apic" : null },
            { "url", "about:blank" },
            { "showTitle", showTitle },
            { "showDate", showDate }
        };
    }
}