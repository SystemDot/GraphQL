namespace SystemDot.GraphQL.Parser
{
    using System;

    class UnexpectedCharacterException : ParseException
    {
        public UnexpectedCharacterException(string source, int pos, int line, int column)
            : base(line, column, "Unexcpected " + source[pos])
        {
        }
    }
}