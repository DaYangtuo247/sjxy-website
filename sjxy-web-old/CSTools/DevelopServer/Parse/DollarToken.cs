namespace DevelopServer.Parse
{
    public class DollarToken : Token
    {
        public DollarToken() : base()
        {
            Type = TokenType.Dollar;
        }

        public string Expression { get; set; }
    }
}
