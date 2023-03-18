#nullable enable

using System.Collections.Generic;

namespace DevelopServer.Parse
{
    public abstract class Token
    {
        public TokenType Type { get; set; }

        public Token? Parent { get; set; }
        public List<Token> Children { get; set; } = new List<Token>();
    }
}
