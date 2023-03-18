using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DevelopServer
{
    class LeftNavGenerator : IModuleGenerator
    {
        private class NavMenu
        {
            public NavMenu()
            {
                this.url = "about:blank";
                this.showTitle = "我我我我";
            }

            public string url;
            public string showTitle;
        }

        private readonly Regex menuListRegex = new Regex(@"<#list list_menu as menu>([\S\s]*?)<\/#list>");

        private static List<NavMenu> GenerateNavMenuList()
        {
            var result = new List<NavMenu>();
            for (int i = 0; i < 5; i++)
                result.Add(new NavMenu());
            return result;
        }

        private List<NavMenu> menuList;

        public LeftNavGenerator()
        {
            menuList = GenerateNavMenuList();
        }

        public string Generate(string source)
        {
            return menuListRegex.Replace(source, (match) =>
            {
                var c = match.Groups[1].Value;
                var resultBuilder = new StringBuilder();
                foreach (var menu in menuList)
                {
                    resultBuilder.Append(Common.EvaluateAllProperty("menu", menu, c));
                }
                return resultBuilder.ToString();
            });
        }
    }
}
