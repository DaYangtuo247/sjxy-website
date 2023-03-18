#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

namespace DevelopServer.Parse
{
    public class ExpressionParser : ParserBase
    {
        private List<ExpressionToken> Tokens { get; } = new List<ExpressionToken>();
        private List<ExpressionToken> _stack = new List<ExpressionToken>();

        public ExpressionParser(string source) : base(source)
        {
        }

        private void Lex()
        {
            while (CurrentPosition != Source.Length)
            {
                var next = ReadWithoutAdvance(1);
                var next2 = ReadWithoutAdvance(2);
                if (next == ".")
                {
                    CurrentPosition += 1;
                    Tokens.Add(new MemberAccessOperatorExpressionToken());
                }
                else if (next == "\"")
                {
                    CurrentPosition += 1;
                    var s = ReadUntil("\"", false);
                    CurrentPosition += 1;
                    Tokens.Add(new ConstantExpressionToken(s, ConstantExpressionToken.ConstantKind.String));
                }
                else if (next == "+")
                {
                    CurrentPosition += 1;
                    Tokens.Add(new ArithmeticOperatorExpressionToken(next));
                }
                else if (next2 == "<=" || next2 == ">=")
                {
                    CurrentPosition += 2;
                    Tokens.Add(new RelationalOperatorExpressionToken(next2));
                }
                else if (next == "<" || next == ">")
                {
                    CurrentPosition += 1;
                    Tokens.Add(new RelationalOperatorExpressionToken(next));
                }
                else if (next2 == "==" || next2 == "!=")
                {
                    CurrentPosition += 2;
                    Tokens.Add(new RelationalOperatorExpressionToken(next2));
                }
                else if (next is not null && char.IsDigit(next[0]))
                {
                    var str = ReadWord();
                    var token = new ConstantExpressionToken(str, ConstantExpressionToken.ConstantKind.Integer);
                    Tokens.Add(token);
                }
                else if (next is not null && char.IsLetter(next[0]))
                {
                    var identifier = ReadWord();
                    Tokens.Add(new IdentifierExpressionToken(identifier));
                }
                else if (next == "(" || next == ")")
                {
                    // TODO: Currently we ignore any parentheses.
                    CurrentPosition += 1;
                }
                else
                {
                    throw new Exception("Unexpected token.");
                }
            }
        }

        // Var := Identifier | Var MemberAccessOperator Identifier
        // Expression := Expression ArithmeticOperator Expression | Expression RelationalOperator Expression | Var
        private bool Reduce()
        {
            if (_stack.Count == 0) return false;
            if (_stack.Count == 1) return false;
            if (_stack.Count == 2) return false;

            var left = _stack[_stack.Count - 3];
            var middle = _stack[_stack.Count - 2];
            var right = _stack[_stack.Count - 1];

            if (middle is MemberAccessOperatorExpressionToken op)
            {
                if ((left is ILeftValueToken leftValueToken) && right is IdentifierExpressionToken rightIdentifier)
                {
                    _stack.RemoveRange(_stack.Count - 3, 3);

                    op.Parent = leftValueToken;
                    op.Child = rightIdentifier;

                    _stack.Add(op);
                    return true;
                }
                throw new Exception("Invalid expression");
            }
            else if (middle is ArithmeticOperatorExpressionToken op2)
            {
                op2.Left = left;
                op2.Right = right;

                _stack.RemoveRange(_stack.Count - 3, 3);
                _stack.Add(op2);
                return true;
            }
            else if (middle is RelationalOperatorExpressionToken op3)
            {
                op3.Left = left;
                op3.Right = right;

                _stack.RemoveRange(_stack.Count - 3, 3);
                _stack.Add(op3);
                return true;
            }

            return false;
        }

        protected override void DoParse()
        {
            Lex();

            int currentIndex = 0;

            while (currentIndex < Tokens.Count)
            {
                while (Reduce())
                {
                }

                _stack.Add(Tokens[currentIndex++]);
            }

            while (Reduce())
            {

            }

            if (_stack.Count != 1)
            {
                throw new Exception("Invalid expression");
            }
        }

        public object Evaluate(IDictionary<string, object> context)
        {
            Parse();

            return EvaluateNode(context, _stack[0]);
        }

        private object EvaluateNode(IDictionary<string, object> context, ExpressionToken token)
        {
            if (token is ConstantExpressionToken constantExpressionToken)
            {
                return constantExpressionToken.ConstantValue;
            }
            else if (token is IdentifierExpressionToken identifierExpressionToken)
            {
                return context[identifierExpressionToken.Value];
            }
            else if (token is MemberAccessOperatorExpressionToken m)
            {
                var path = new List<string>();
                ILeftValueToken t = m;
                while (t is MemberAccessOperatorExpressionToken op)
                {
                    path.Insert(0, op.Child!.Value);
                    t = op.Parent!;
                }
                path.Insert(0, ((IdentifierExpressionToken)t).Value);

                IDictionary<string, object> current = context;
                foreach (var p in path.SkipLast(1))
                {
                    current = (IDictionary<string, object>)current[p];
                }
                return current[path.Last()];
            }
            else if (token is ArithmeticOperatorExpressionToken op)
            {
                var leftValue = EvaluateNode(context, op.Left!);
                var rightValue = EvaluateNode(context, op.Right!);

                if (leftValue is not Int32 || rightValue is not Int32)
                {
                    throw new Exception("Invalid operands");
                }

                switch (op.Value)
                {
                    case "+": return (int)leftValue + (int)rightValue;
                    case "-": return (int)leftValue - (int)rightValue;
                    default: throw new Exception("Invalid operator");
                }
            }
            else if (token is RelationalOperatorExpressionToken op2)
            {
                var leftValue = EvaluateNode(context, op2.Left!);
                var rightValue = EvaluateNode(context, op2.Right!);

                int compareResult = 0;

                if (leftValue is string && rightValue is string)
                {
                    compareResult = string.Compare((string)leftValue, (string)rightValue, StringComparison.Ordinal);
                }
                else if (leftValue is int && rightValue is int)
                {
                    compareResult = (int)leftValue - (int)rightValue;
                }

                switch (op2.Value)
                {
                    case "<": return compareResult < 0;
                    case ">": return compareResult > 0;
                    case "<=": return compareResult <= 0;
                    case ">=": return compareResult >= 0;
                    case "==": return compareResult == 0;
                    case "!=": return compareResult != 0;
                    default: throw new Exception("Invalid operator");
                }
            }
            else
            {
                throw new Exception("Invalid token");
            }
        }
    }
}
