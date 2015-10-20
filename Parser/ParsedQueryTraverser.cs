namespace SystemDot.GraphQL.Parser
{
    using System.Linq;
    using SystemDot.Core;

    public static class ParsedQueryTraverser
    {
        public static ParsedQueryNode Traverse(ParsedQueryNode node, ParsedQueryNodeVisitorLookup visitor)
        {
            return Traverse(node, visitor, null);
        }

        static ParsedQueryNode Traverse(ParsedQueryNode node, ParsedQueryNodeVisitorLookup visitor, ParsedQueryNode parent)
        {
            if (node == null) return null;

            if (node.GetType() == typeof (ParsedQuery))
            {
                var query = node.As<ParsedQuery>();
                node = new ParsedQuery(query.Fields.Select(n => Traverse(n, visitor, node)).ToArray());
            }

            if (node.GetType() == typeof (ParsedField))
            {
                var field = node.As<ParsedField>();

                node = new ParsedField(
                    field.Value,
                    field.Alias,
                    field.Parameters.Select(n => Traverse(n, visitor, node)).ToArray(),
                    field.Fields.Select(n => Traverse(n, visitor, node)).ToArray());
            }

            if (node.GetType() == typeof (ParsedArgument))
            {
                var arg = node.As<ParsedArgument>();
                node = new ParsedArgument(arg.Name, Traverse(arg.Value, visitor, node));
            }


            return visitor.Lookup(node.GetType()).Visit(node, parent);
        }
    }
}