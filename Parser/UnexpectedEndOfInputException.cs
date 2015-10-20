namespace SystemDot.GraphQL.Parser
{
    class UnexpectedEndOfInputException : ParseException
    {
        public UnexpectedEndOfInputException(int line, int column) : base(line, column, "Unexpected end of input")
        {
        }
    }
}