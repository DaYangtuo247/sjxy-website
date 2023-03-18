#nullable enable

namespace DevelopServer.Parse
{
    public abstract class DirectiveToken : Token
    {
        public DirectiveToken()
        {
            Type = TokenType.Directive;
        }

        public DirectiveType DirectiveType { get; set; }
    }
}
