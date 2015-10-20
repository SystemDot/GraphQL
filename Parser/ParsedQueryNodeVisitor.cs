namespace SystemDot.GraphQL.Parser
{
    using SystemDot.Core;

    public interface ParsedQueryNodeVisitor
    {
        ParsedQueryNode Visit(ParsedQueryNode node, ParsedQueryNode parent);
    }
}