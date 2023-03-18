#nullable enable

namespace DevelopServer.Parse
{
    public class ListDirectiveToken : DirectiveToken
    {
        public ListDirectiveToken()
        {
            DirectiveType = DirectiveType.List;
        }

        public string ListName { get; set; } = "";
        public string AsName { get; set; } = "";
    }
}
