namespace SystemDot.GraphQL.Parser
{
    using SystemDot.Core;

    public class TokenType : Equatable<TokenType>
    {
        public readonly string Name;
        public readonly TokenClass Class;

        TokenType(TokenClass @class, string name)
        {
            Name = name;
            Class = @class;
        }

        // Special
        public static TokenType End { get { return new TokenType(TokenClass.End, "end"); }}
        public static TokenType Identifier { get { return new TokenType(TokenClass.Identifier, "identifier"); } }
        public static TokenType Number { get { return new TokenType(TokenClass.NumberLiteral, "number"); } }
        public static TokenType String { get { return new TokenType(TokenClass.StringLiteral, "string"); } }

        // Punctuators
        public static TokenType Lt { get { return new TokenType(TokenClass.Punctuator, "<"); } }
        public static TokenType Gt { get { return new TokenType(TokenClass.Punctuator, ">"); } }
        public static TokenType Lbrace { get { return new TokenType(TokenClass.Punctuator, "{"); } }
        public static TokenType Rbrace { get { return new TokenType(TokenClass.Punctuator, "}"); } }
        public static TokenType Lparen { get { return new TokenType(TokenClass.Punctuator, "("); } }
        public static TokenType Rparen { get { return new TokenType(TokenClass.Punctuator, ")"); } }
        public static TokenType Colon { get { return new TokenType(TokenClass.Punctuator, ""); } }
        public static TokenType Comma { get { return new TokenType(TokenClass.Punctuator, ","); } }
        public static TokenType Amp { get { return new TokenType(TokenClass.Punctuator, "&"); } }

        // Keywords
        public static TokenType Null { get { return new TokenType(TokenClass.Keyword, "null"); } }
        public static TokenType True { get { return new TokenType(TokenClass.Keyword, "true"); } }
        public static TokenType False { get { return new TokenType(TokenClass.Keyword, "false"); } }
        public static TokenType As { get { return new TokenType(TokenClass.Keyword, "as"); } }

        public override bool Equals(TokenType other)
        {
            return other.Class == Class && other.Name == Name;
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + Class.GetHashCode();
            hash = (hash * 7) + Name.GetHashCode();
            return hash;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Class, Name);
        }
    }
}