namespace DevelopServer.Parse
{
    public class DollarLinkToken : Token
    {
        public DollarLinkToken() : base()
        {
            Type = TokenType.DollarLink;
        }

        public string Link { get; set; }
    }
}
