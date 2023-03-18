using System.Collections.Generic;

namespace DevelopServer
{
    class CurrentPosGenerator : BaseModuleGenerator
    {
        private static Dictionary<string, object> CreatePosMenu(int pos)
        {
            return new()
            {
                { "pos", pos },
                { "linkUrl", "about:blank" },
                { "showName", "位置" },
                { "name", "位置" },
            };
        }

        private static Dictionary<string, object> CreateModel()
        {
            return new()
            {
                {
                    "list_menu",
                    new List<object>() {
                        CreatePosMenu(1),
                        CreatePosMenu(2),
                        CreatePosMenu(3),
                    }
                },
            };
        }

        public CurrentPosGenerator() : base(CreateModel())
        {
        }
    }
}
