using System.Collections.Generic;

namespace DevelopServer
{
    public class CommonNavGenerator : BaseModuleGenerator
    {
        private static Dictionary<string, object> CreateSubMenu(string title)
        {
            return new()
            {
                { "url", "about:blank" },
                { "showTitle", title }
            };
        }

        private static List<object> CreateSubMenuList()
        {
            return new()
            {
                CreateSubMenu("三个字"),
                CreateSubMenu("四个字啊"),
                CreateSubMenu("三个字")
            };
        }

        private static Dictionary<string, object> CreateNavMenu(string title, int pos, bool hasSubMenu, bool isFocus = false)
        {
            return new()
            {
                { "url", "about:blank" },
                { "pos", pos },
                { "showTitle", title },
                { "hasSubMenu", hasSubMenu },
                { "isFocus", isFocus },
                { "list_subMenu", CreateSubMenuList() }
            };
        }

        private static Dictionary<string, object> CreateModel()
        {
            List<object> navMenus = new()
            {
                CreateNavMenu("首页", 1, false),
                CreateNavMenu("学院概况", 2, true),
                CreateNavMenu("师资队伍", 3, true, true),
                CreateNavMenu("本科生教育", 4, true),
                CreateNavMenu("研究生教育", 5, true),
                CreateNavMenu("规章制度", 6, true),
                CreateNavMenu("校友风采", 7, true),
                CreateNavMenu("学校首页", 8, true),
            };

            return new()
            {
                { "list_menu", navMenus },
            };
        }

        public CommonNavGenerator() : base(CreateModel())
        {
        }
    }
}
