namespace SystemDot.GraphQL.Parser
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json.Linq;

    public class GraphQlParser : Tokenizer
    {
        public static ParsedQuery ParseQuery(string query)
        {
            return new GraphQlParser(query).ParseQuery();
        }

        GraphQlParser(string source)
            : base(source)
        {
        }

        bool Match(TokenType type)
        {
            return Lookahead.Type == type;
        }

        Token Eat(TokenType type)
        {
            return Match(type) ? Lex() : null;
        }

        Token Expect(TokenType type)
        {
            if (Match(type))
            {
                return Lex();
            }

            throw OnUnexpectedError(Lookahead);
        }

        ParsedQuery ParseQuery()
        {
            return new ParsedQuery(ParseFieldList());
        }

        string ParseIdentifier()
        {
            return Expect(TokenType.Identifier).Value;
        }

        ParsedQueryNode[] ParseFieldList()
        {
            Expect(TokenType.Lbrace);

            var fields = new List<ParsedQueryNode>();
            var first = true;

            while (!Match(TokenType.Rbrace) && !End())
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    Expect(TokenType.Comma);
                }

                fields.Add(Match(TokenType.Amp) ? ParseReference() : ParseField());
            }

            Expect(TokenType.Rbrace);
            return fields.ToArray();
        }

        ParsedQueryNode ParseReference()
        {
            Expect(TokenType.Amp);

            if (Match(TokenType.Number) || Match(TokenType.Identifier))
            {
                return new Reference(Lex().Value);
            }

            throw OnUnexpectedError(Lookahead);
        }

        ParsedField ParseField()
        {
            var name = ParseIdentifier();
            var args = Match(TokenType.Lparen) ? ParseArgumentList() : new ParsedArgument[0];
            var alias = Eat(TokenType.As) != null ? ParseIdentifier() : null;
            var fields = Match(TokenType.Lbrace) ? ParseFieldList() : new ParsedField[0];

            return new ParsedField(name, alias, args, fields);
        }


        ParsedArgument[] ParseArgumentList()
        {
            var args = new List<ParsedArgument>();
            var first = true;

            Expect(TokenType.Lparen);

            while (!Match(TokenType.Rparen) && !End())
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    Expect(TokenType.Comma);
                }

                args.Add(ParseArgument());
            }

            Expect(TokenType.Rparen);
            return args.ToArray();
        }

        ParsedArgument ParseArgument()
        {
            var name = ParseIdentifier();
            Expect(TokenType.Colon);
            var value = ParseValue();

            return new ParsedArgument(name, value);
        }

        ParsedQueryNode ParseValue()
        {
            if (Lookahead.Type == TokenType.Amp) return ParseReference();
            if (Lookahead.Type == TokenType.Lt) return ParseVariable();
            if (Lookahead.Type == TokenType.Number || Lookahead.Type == TokenType.String) return new ParsedLiteral(Lex().Value);
            if (Lookahead.Type == TokenType.Null || Lookahead.Type == TokenType.True || Lookahead.Type == TokenType.False) return new ParsedLiteral(JObject.Parse(Lex().Value).ToString());
            throw OnUnexpectedError(Lookahead);
        }

        ParsedQueryNode ParseVariable()
        {
            Expect(TokenType.Lt);
            var name = Expect(TokenType.Identifier).Value;
            Expect(TokenType.Gt);

            return new ParsedVariable(name);
        }

        Exception OnUnexpectedError(Token token)
        {
            return new UnexpectedTokenException(Line, Column(), token);
        }
    }
}
