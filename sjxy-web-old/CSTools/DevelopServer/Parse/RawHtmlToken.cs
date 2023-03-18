#nullable enable

namespace DevelopServer.Parse
{
    public class RawHtmlToken : Token
    {
        public RawHtmlToken()
        {
            Type = TokenType.RawHtml;
        }

        public string Html { get; set; } = "";
    }
}
