using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DevelopServer
{
    class IndexPageNewsColumnGenerator : IModuleGenerator
    {
        private class News
        {
            public News(string showTitle, string showDate)
            {
                this.isNew = false;
                this.picUrl = null;
                this.url = "about:blank";
                this.showTitle = showTitle;
                this.showDate = showDate;
            }

            public News(string picUrl, string showTitle, string showDate)
            {
                this.isNew = true;
                this.picUrl = picUrl;
                this.url = "about:blank";
                this.showTitle = showTitle;
                this.showDate = showDate;
            }

            public bool isNew;
            public string picUrl;
            public string url;
            public string showTitle;
            public string showDate;
        }

        private readonly Regex newsListRegex = new Regex(@"<#list list_news as news>([\S\s]*?)<\/#list>");
        private readonly Regex ifRegex = new Regex(@"<#if\s*news\.(.*?)>([\S\s]*?)<\/#if>");

        private List<News> newsList;

        public IndexPageNewsColumnGenerator()
        {
            newsList = new List<News>();
        }

        public IndexPageNewsColumnGenerator((int, int) config) : this(config.Item1, config.Item2)
        {

        }

        public IndexPageNewsColumnGenerator(int newsCount, int titleMaxLength)
        {
            newsList = GenerateNewsList(newsCount, titleMaxLength);
        }

        private List<News> GenerateNewsList(int newsCount, int titleMaxLength)
        {
            var title = new string('我', titleMaxLength - 1) + "...";
            var result = new List<News> {
                new News("/apic", title, "09/02"),
            };
            for (int i = 1; i < newsCount; i++)
            {
                result.Add(new News(title, "09/02"));
            }
            return result;
        }

        public string Generate(string source)
        {
            source = Common.ConvertVLink(source);

            source = newsListRegex.Replace(source, (m) =>
            {
                var c = m.Groups[1].Value;
                var resultBuilder = new StringBuilder();
                foreach (var news in newsList)
                {
                    var r = ifRegex.Replace(c, (m2) =>
                    {
                        var condition = Common.GetPropertyValue<bool>(news, m2.Groups[1].Value);
                        if (!condition)
                            return "";
                        else
                            return m2.Groups[2].Value;
                    });
                    resultBuilder.Append(Common.EvaluateAllProperty("news", news, r));
                }
                return resultBuilder.ToString();
            });

            return source;
        }
    }
}
