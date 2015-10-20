namespace SystemDot.GraphQL.Parser
{
    public class ParsedField : ParsedQueryNode
    {
        public string Value { get; private set; }
        public string Alias { get; private set; }
        public ParsedQueryNode[] Parameters { get; private set; }
        public ParsedQueryNode[] Fields { get; private set; }

        public ParsedField(string value, string @alias, ParsedQueryNode[] parameters, ParsedQueryNode[] fields)
        {
            Value = value;
            Alias = alias;
            Parameters = parameters;
            Fields = fields;
        }
    }
}