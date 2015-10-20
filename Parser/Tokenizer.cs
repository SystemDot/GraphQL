namespace SystemDot.GraphQL.Parser
{
    using System;
    using System.Globalization;

    public class Tokenizer
    {
        readonly string source;
        int pos;
        int lineStart;
        protected int Line;
        protected Token Lookahead;

        protected Tokenizer(string source)
        {
            this.source = source;

            pos = 0;
            Line = 1;
            lineStart = 0;
            Lookahead = Next();
        }

        protected int Column()
        {
            return pos - lineStart;
        }

        TokenType getKeyword(string name)
        {
            switch (name)
            {
                case "null":
                    return TokenType.Null;
                case "true":
                    return TokenType.True;
                case "false":
                    return TokenType.False;
                case "as":
                    return TokenType.As;
            }

            return TokenType.Identifier;
        }

        protected bool End()
        {
            return Lookahead.Type == TokenType.End;
        }

        protected Token Lex()
        {
            var prev = Lookahead;
            Lookahead = Next();
            return prev;
        }

        Token Next()
        {
            SkipWhitespace();
            Token token = Scan();
            token.Line = Line;
            token.Column = pos - lineStart;

            return token;
        }

        Token Scan()
        {
            if (pos >= source.Length)
            {
                return new Token(TokenType.End);
            }

            var ch = source[pos];
            switch (ch)
            {
                case '(':
                    ++pos;
                    return new Token(TokenType.Lparen);
                case ')':
                    ++pos;
                    return new Token(TokenType.Rparen);
                case '{':
                    ++pos;
                    return new Token(TokenType.Lbrace);
                case '}':
                    ++pos;
                    return new Token(TokenType.Rbrace);
                case '<':
                    ++pos;
                    return new Token(TokenType.Lt);
                case '>':
                    ++pos;
                    return new Token(TokenType.Gt);
                case '&':
                    ++pos;
                    return new Token(TokenType.Amp);
                case ',':
                    ++pos;
                    return new Token(TokenType.Comma);
                case ':':
                    ++pos;
                    return new Token(TokenType.Colon);
            }

            if (ch == '_' || ch == '$' || 'a' <= ch && ch <= 'z' || 'A' <= ch && ch <= 'Z')
            {
                return ScanWord();
            }

            if (ch == '-' || '0' <= ch && ch <= '9')
            {
                return ScanNumber();
            }

            if (ch == '"')
            {
                return ScanString();
            }

            throw OnIllegalCharacterFound();
        }

        Token ScanWord()
        {
            var start = pos;
            pos++;

            while (pos < source.Length)
            {
                var ch = source[pos];
                if (ch == '_' || ch == '$' || 'a' <= ch && ch <= 'z' || 'A' <= ch && ch <= 'Z' || '0' <= ch && ch <= '9')
                {
                    pos++;
                }
                else
                {
                    break;
                }
            }

            var value = source.Slice(start, pos);
            return new Token(getKeyword(value), value);
        }

        Token ScanNumber()
        {
            var start = pos;

            if (source[pos] == '-')
            {
                pos++;
            }

            SkipInteger();

            if (source[pos] == '.')
            {
                pos++;
                SkipInteger();
            }

            var ch = source[pos];
            if (ch == 'e' || ch == 'E')
            {
                pos++;

                ch = source[pos];
                if (ch == '+' || ch == '-')
                {
                    pos++;
                }

                SkipInteger();
            }

            var value = float.Parse(source.Slice(start, pos));
            return new Token(TokenType.Number, value.ToString(CultureInfo.InvariantCulture));
        }

        Token ScanString()
        {
            pos++;
            string value = string.Empty;

            while (pos < source.Length)
            {
                var ch = source[pos];

                if (ch == '\'')
                {
                    pos++;
                    return new Token(TokenType.String, value);
                }

                if (ch == '\r' || ch == '\n')
                {
                    break;
                }

                value += ch;
                pos++;
            }

            throw OnIllegalCharacterFound();
        }

        void SkipInteger()
        {
            var start = pos;

            while (pos < source.Length)
            {
                var ch = source[pos];
                if ('0' <= ch && ch <= '9')
                {
                    pos++;
                }
                else
                {
                    break;
                }
            }

            if (pos - start == 0)
            {
                throw OnIllegalCharacterFound();
            }
        }

        void SkipWhitespace()
        {
            while (pos < source.Length)
            {
                char ch = source[pos];
                if (ch == ' ' || ch == '\t')
                {
                    pos++;
                }
                else if (ch == '\r')
                {
                    pos++;
                    if (source[pos] == '\n')
                    {
                        pos++;
                    }
                    Line++;
                    lineStart = pos;
                }
                else if (ch == '\n')
                {
                    pos++;
                    Line++;
                    lineStart = pos;
                }
                else
                {
                    break;
                }
            }
        }

        Exception OnIllegalCharacterFound()
        {
            if (pos < source.Length)
                return new UnexpectedCharacterException(source, pos, Line, Column());

            return new UnexpectedEndOfInputException(Line, Column());
        }

    }
}
