namespace SystemDot.GraphQL.Parser
{
    class ParsedArgument : ParsedQueryNode
    {
        public ParsedArgument(string name, ParsedQueryNode value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }
        public ParsedQueryNode Value { get; private set; }
    }
}