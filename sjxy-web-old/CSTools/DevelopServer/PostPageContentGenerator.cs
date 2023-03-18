using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DevelopServer
{
    class PostPageContentGenerator : IModuleGenerator
    {
        private readonly Regex extraRegex = new Regex(@"<#if\s*hasExtend>([\S\s]*)<\/#if>");
        private readonly Regex sourceLinkRegex = new Regex(@"<#if\s*hasSourceLink>([\S\s]*?)<#else>[\S\s]*?<\/#if>");

        private readonly Dictionary<string, string> propertyMap = new Dictionary<string, string>
        {
            { "showTitle", Common.title },
            { "showDate", "2020年10月30日 14:44" },
            { "showAuthor", "你我他" },
            { "sourceLink", "about:blank" },
            { "showSource", ""},
            { "showClickTimes", "100"},
            { "content", Common.content}
        };

        public string Generate(string source)
        {
            source = extraRegex.Replace(source, "");
            source = sourceLinkRegex.Replace(source, match => match.Groups[1].Value);
            foreach (var (key, value) in propertyMap)
                source = Common.EvaluateVariable(source, key, value);
            return source;
        }
    }
}
