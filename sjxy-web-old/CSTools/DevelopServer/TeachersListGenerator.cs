using System.Collections.Generic;
using DevelopServer.Parse;

namespace DevelopServer
{
    class TeachersListGenerator : IModuleGenerator
    {
        private Dictionary<string, object> model;

        public TeachersListGenerator()
        {
            model = CreateModel();
        }

        private Dictionary<string, object> CreateModel()
        {
            List<object> listNews = new()
            {
                ModelCommon.CreateNews("1", "杨宇千（教授）", "2019-01-01", hasPic: true),
                ModelCommon.CreateNews("2", "杨宇千（教授）", "2019-01-01", hasPic: true),
                ModelCommon.CreateNews("3", "杨宇千（教授）", "2019-01-01", hasPic: true),
                ModelCommon.CreateNews("4", "杨宇千（教授）", "2019-01-01", hasPic: true),
                ModelCommon.CreateNews("5", "杨宇千（教授）", "2019-01-01", hasPic: true),
                ModelCommon.CreateNews("6", "杨宇千（教授）", "2019-01-01", hasPic: true),
                ModelCommon.CreateNews("7", "杨宇千（教授）", "2019-01-01", hasPic: true),
                ModelCommon.CreateNews("8", "杨宇千（教授）", "2019-01-01", hasPic: true),
                ModelCommon.CreateNews("9", "杨宇千（教授）", "2019-01-01", hasPic: true),
                ModelCommon.CreateNews("10", "杨宇千（教授）", "2019-01-01", hasPic: true),
                ModelCommon.CreateNews("11", "杨宇千（教授）", "2019-01-01", hasPic: true),
                ModelCommon.CreateNews("12", "杨宇千（教授）", "2019-01-01", hasPic: true),
            };

            return new()
            {
                { "list_news", listNews },
                { "pageBar", "" },
            };
        }

        public string Generate(string source)
        {
            var parser = new Parser(source);
            return parser.Evaluate(model);
        }
    }
}
