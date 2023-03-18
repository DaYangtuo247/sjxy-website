using System;
using System.Collections.Generic;
using System.Text;

#nullable enable

namespace DevelopServer.Parse
{
    public class Parser : ParserBase
    {
        private Token CurrentToken { get; set; }
        public Token RootToken = new RootToken();

        public Parser(string source) : base(source)
        {
            CurrentToken = RootToken;
        }

        private void ClearHtml(StringBuilder rawHtmlBuilder)
        {
            if (rawHtmlBuilder.Length > 0)
            {
                CurrentToken.Children.Add(new RawHtmlToken() { Html = rawHtmlBuilder.ToString() });
                rawHtmlBuilder.Clear();
            }
        }

        protected override void DoParse()
        {
            var rawHtml = new StringBuilder();

            while (CurrentPosition < Source.Length)
            {
                int spaceCount = ReadSpaceAndReturnSpaceCount();

                if (ReadWithoutAdvance(2) == "<#")
                {
                    ClearHtml(rawHtml);

                    CurrentPosition += 2;

                    if (ReadWithoutAdvance(4) == "list")
                    {
                        var token = new ListDirectiveToken();

                        CurrentPosition += 4;

                        token.ListName = ReadUntil(" ");

                        if (Read(2) == "as")
                        {
                            token.AsName = ReadWord();

                            if (Read(1) != ">")
                                throw new Exception("Expected '>'");
                        }
                        else
                        {
                            throw new Exception("Invalid directive");
                        }

                        CurrentToken.Children.Add(token);
                        token.Parent = CurrentToken;
                        CurrentToken = token;

                    }
                    else if (ReadWithoutAdvance(2) == "if")
                    {
                        var token = new ConditionDirectiveToken();

                        CurrentPosition += 2;

                        token.Condition = ReadUntil(">").TrimEnd();

                        if (Read(1) != ">")
                            throw new Exception("Expected '>'");

                        CurrentToken.Children.Add(token);
                        token.Parent = CurrentToken;
                        CurrentToken = token;
                    }
                    else if (ReadWithoutAdvance(4) == "else")
                    {
                        if (CurrentToken is not ConditionDirectiveToken)
                            throw new Exception("Else directive needs to be inside if directive.");

                        var t = (ConditionDirectiveToken)CurrentToken;

                        t.ElseToken = new ElseToken();
                        t.ElseToken.Parent = t;
                        CurrentToken = t.ElseToken;
                    }
                    else
                    {
                        throw new Exception("Invalid directive");
                    }
                }
                else if (ReadWithoutAdvance(3) == "</#")
                {
                    ClearHtml(rawHtml);

                    CurrentPosition += 3;

                    if (ReadWithoutAdvance(4) == "list")
                    {
                        CurrentPosition += 4;

                        if (Read(1) != ">")
                            throw new Exception("Expected '>'");

                        if (CurrentToken.Parent is null) throw new Exception("Unexpected directive close token.");
                        CurrentToken = CurrentToken.Parent;
                    }
                    else if (ReadWithoutAdvance(2) == "if")
                    {
                        CurrentPosition += 2;

                        if (Read(1) != ">")
                            throw new Exception("Expected '>'");


                        if (CurrentToken.Parent is null) throw new Exception("Unexpected directive close token.");

                        if (CurrentToken.Parent is ElseToken) CurrentToken = CurrentToken.Parent;

                        if (CurrentToken.Parent is null) throw new Exception("Unexpected directive close token.");

                        CurrentToken = CurrentToken.Parent;
                    }
                    else
                    {
                        throw new System.Exception("Invalid directive");
                    }
                }
                else if (ReadWithoutAdvance(2) == "${")
                {
                    ClearHtml(rawHtml);
                    CurrentPosition += 2;

                    if (ReadWithoutAdvance(8) == "v_link('")
                    {
                        CurrentPosition += 8;

                        var token = new DollarLinkToken();

                        token.Link = ReadUntil("')}");

                        if (Read(3) != "')}")
                            throw new Exception("Expected ')}");

                        CurrentToken.Children.Add(token);
                    }
                    else
                    {
                        var token = new DollarToken();

                        token.Expression = ReadUntil("}");

                        if (Read(1) != "}")
                            throw new Exception("Expected '}'");

                        CurrentToken.Children.Add(token);
                    }
                }
                else
                {
                    if (spaceCount > 0)
                    {
                        rawHtml.Append(' ', spaceCount);
                    }

                    if (CurrentPosition < Source.Length)
                    {
                        rawHtml.Append(Source[CurrentPosition]);
                    }
                    CurrentPosition++;
                }
            }

            ClearHtml(rawHtml);
        }

        public string Evaluate(IDictionary<string, object> context)
        {
            Parse();

            return EvaluateNode(context, RootToken);
        }

        private string EvaluateNode(IDictionary<string, object> context, Token node)
        {
            if (node is RawHtmlToken rawHtmlToken)
            {
                return rawHtmlToken.Html;
            }
            else if (node is ListDirectiveToken listDirectiveToken)
            {
                var asName = listDirectiveToken.AsName;

                ExpressionParser listNameParser = new ExpressionParser(listDirectiveToken.ListName);
                var list = listNameParser.Evaluate(context);

                if (list is not List<object>)
                {
                    throw new Exception("List directive not operate on list.");
                }
                var l = (List<object>)list;
                var sb = new StringBuilder();

                foreach (var o in l)
                {
                    foreach (var child in listDirectiveToken.Children)
                        sb.Append(EvaluateNode(new Dictionary<string, object>() { { asName, o } }, child));
                }

                return sb.ToString();
            }
            else if (node is ConditionDirectiveToken conditionDirectiveToken)
            {
                var parser = new ExpressionParser(conditionDirectiveToken.Condition);
                var value = parser.Evaluate(context);
                if (value is not bool)
                {
                    throw new Exception("Condition directive not operate on bool.");
                }

                if ((bool)value)
                {
                    var sb = new StringBuilder();

                    foreach (var child in conditionDirectiveToken.Children)
                        sb.Append(EvaluateNode(context, child));

                    return sb.ToString();
                }
                else if (conditionDirectiveToken.ElseToken is not null)
                {
                    var sb = new StringBuilder();

                    foreach (var child in conditionDirectiveToken.ElseToken.Children)
                        sb.Append(EvaluateNode(context, child));

                    return sb.ToString();
                }

                return "";
            }
            else if (node is DollarToken dollarToken)
            {
                var parser = new ExpressionParser(dollarToken.Expression);
                var value = parser.Evaluate(context);

                return value.ToString()!;
            }
            else if (node is DollarLinkToken dollarLinkToken)
            {
                return dollarLinkToken.Link;
            }
            else if (node is RootToken token)
            {
                var sb = new StringBuilder();

                foreach (var child in token.Children)
                {
                    sb.Append(EvaluateNode(context, child));
                }

                return sb.ToString();
            }

            throw new Exception("Unknown node type.");
        }
    }
}
