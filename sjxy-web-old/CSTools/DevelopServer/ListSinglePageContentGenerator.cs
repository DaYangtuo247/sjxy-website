using System.Text.RegularExpressions;

namespace DevelopServer
{
    class ListSinglePageContentGenerator : IModuleGenerator
    {
        private readonly Regex newsListRegex = new Regex(@"<#list list_news as news>([\S\s]*)<\/#list>");
        private readonly Regex pageListRegex = new Regex(@"<#list\s*news\.list_page\s*as\s*page>[\S\s]*?</#list>");

        private class News
        {
            public string showTitle = Common.title;
            public string content = Common.content;
            public string pageBar = "<div>翻页控件</div>";
            public string attach = "";
        }

        private News news = new News();

        public string Generate(string source)
        {
            return newsListRegex.Replace(source, (match) =>
            {
                var c = match.Groups[1].Value;

                c = pageListRegex.Replace(c, "");

                return Common.EvaluateAllProperty("news", news, c);
            });
        }
    }
}
