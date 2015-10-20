namespace SystemDot.GraphQL.Parser
{
    public class ParsedQuery : ParsedQueryNode
    {
        public ParsedQueryNode[] Fields { get; private set; }

        public ParsedQuery(ParsedQueryNode[] fields)
        {
            Fields = fields;
        }
    }
}