namespace SystemDot.GraphQL.Parser
{
    public class Token
    {
        public string Value { get; private set; }

        public TokenType Type { get; private set; }

        public int Line { set; get; }

        public int Column { get; set; }

        public Token(TokenType type)
        {
            Type = type;
        }

        public Token(TokenType type, string value)
        {
            Type = type; 
            Value = value;
        }
    }
}