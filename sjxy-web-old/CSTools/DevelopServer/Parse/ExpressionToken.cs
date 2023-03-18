#nullable enable


namespace DevelopServer.Parse
{
    public abstract class ExpressionToken
    {
        public ExpressionTokenType Type { get; }
        public string Value { get; }

        public ExpressionToken(ExpressionTokenType type) : this(type, "")
        {
        }

        public ExpressionToken(ExpressionTokenType type, string value)
        {
            Type = type;
            Value = value;
        }
    }

    public interface ILeftValueToken
    {
    }

    public class ConstantExpressionToken : ExpressionToken
    {
        public enum ConstantKind
        {
            String,
            Integer
        }

        public ConstantKind Kind { get; }

        public ConstantExpressionToken(string value, ConstantKind kind) : base(ExpressionTokenType.Constant, value)
        {
            Kind = kind;
        }

        public object ConstantValue => Kind == ConstantKind.Integer ? int.Parse(Value) : Value;
    }

    public class IdentifierExpressionToken : ExpressionToken, ILeftValueToken
    {
        public IdentifierExpressionToken(string value) : base(ExpressionTokenType.Identifier, value)
        {
        }
    }

    public class MemberAccessOperatorExpressionToken : ExpressionToken, ILeftValueToken
    {
        public ILeftValueToken? Parent { get; set; }
        public IdentifierExpressionToken? Child { get; set; }

        public MemberAccessOperatorExpressionToken() : base(ExpressionTokenType.MemberAccessOperator)
        {
        }
    }

    public class RelationalOperatorExpressionToken : ExpressionToken
    {
        public ExpressionToken? Left { get; set; }
        public ExpressionToken? Right { get; set; }

        public RelationalOperatorExpressionToken(string value) : base(ExpressionTokenType.RelationalOperator, value)
        {
        }
    }

    public class ArithmeticOperatorExpressionToken : ExpressionToken
    {
        public ExpressionToken? Left { get; set; }
        public ExpressionToken? Right { get; set; }

        public ArithmeticOperatorExpressionToken(string value) : base(ExpressionTokenType.ArithmeticOperator, value)
        {
        }
    }
}
