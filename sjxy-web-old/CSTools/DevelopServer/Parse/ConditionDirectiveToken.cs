#nullable enable

using System.Collections.Generic;

namespace DevelopServer.Parse
{
    public class ConditionDirectiveToken : DirectiveToken
    {
        public ConditionDirectiveToken()
        {
            DirectiveType = DirectiveType.Condition;
        }

        public string Condition { get; set; } = "";

        public ElseToken? ElseToken { get; set; } = null;
    }
}
