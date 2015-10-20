namespace SystemDot.GraphQL.Parser
{
    class UnexpectedTokenException : ParseException
    {
        public UnexpectedTokenException(int line, int column, Token token)
            : base(line, column, GetMessage(token))
        {
        }

        static string GetMessage(Token token)
        {
            switch (token.Type.Class)
            {
                case TokenClass.End:
                    return "Unexpected end of input";
                case TokenClass.NumberLiteral:
                    return "Unexpected number";
                case TokenClass.StringLiteral:
                    return "Unexpected string";
                case TokenClass.Identifier:
                    return "Unexpected identifier";
                case TokenClass.Keyword:
                    return string.Format("Unexpected token {0}", token.Value);
                case TokenClass.Punctuator:
                    return string.Format("Unexpected token {0}", token.Type.Name);
            }
            return "";
        }
    }
}