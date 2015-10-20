namespace SystemDot.GraphQL.Parser
{
    using System;

    class ParseException : Exception
    {
        protected ParseException(int line, int column, string message) : base(GetMessage(line, column, message))
        {
        }

        static string GetMessage(int line, int column, string message)
        {
            return string.Format("{0} ({1}:{2})", message, line, column);
        }
    }
}