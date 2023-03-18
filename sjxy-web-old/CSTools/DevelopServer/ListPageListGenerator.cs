using System.Collections.Generic;

namespace DevelopServer
{
    class ListPageListGenerator : BaseModuleGenerator
    {
        private static Dictionary<string, object> CreateModel()
        {
            string title = new string('你', 24);

            List<object> listNews = new List<object>();

            for (int i = 0; i < 10; i++)
            {
                listNews.Add(ModelCommon.CreateNews(i.ToString(), title, "2019/01/01", hasPic: true));
            }

            return new()
            {
                { "list_news", listNews },
                { "pageBar", "" },
            };
        }

        public ListPageListGenerator() : base(CreateModel())
        {
        }
    }
}
