namespace SystemDot.GraphQL.Runner
{
    using System;
    using SystemDot.Core;
    using SystemDot.GraphQL.Schema;
    using SystemDot.GraphQL.Parser;
    using Newtonsoft.Json.Linq;

    public static class Runner
    {
        public static string Run(string query, GraphQlSchema schema)
        {
            ParsedQuery parsedQuery = GraphQlParser.ParseQuery(query);

            var schemaCursor = new GraphQlSchemaCursor(schema);
            var queryTree = new QueryTree();
            var queryTreeCursor = new QueryTreeCursor(queryTree);

            var visitorLookup = new ParsedQueryNodeVisitorLookup();
            visitorLookup.RegisterForNode<ParsedQuery>(new NullQueryParsedQueryNodeVisitor());
            visitorLookup.RegisterForNode<ParsedField>(new FieldSelectionVisitor(schemaCursor, queryTreeCursor));
            ParsedQueryTraverser.Traverse(parsedQuery, visitorLookup);

        }
    }

    public class QueryTreeCursor
    {
        public QueryTree Current { get; set; }

        public QueryTreeCursor(QueryTree queryTree)
        {
            Current = queryTree;
        }
    }

    public class QueryTree : QueryBranch
    {
    }

    public interface QueryBranch
    {
    }

    public class FieldSelectionVisitor : ParsedQueryNodeVisitor
    {
        readonly GraphQlSchemaCursor schemaCursor;
        readonly QueryTreeCursor queryTreeCursor;

        public FieldSelectionVisitor(GraphQlSchemaCursor schemaCursor, QueryTreeCursor queryTreeCursor)
        {
            this.schemaCursor = schemaCursor;
            this.queryTreeCursor = queryTreeCursor;
        }

        public ParsedQueryNode Visit(ParsedQueryNode node, ParsedQueryNode parent)
        {
            var field = node.As<ParsedField>();
            schemaCursor.MoveToChild(field.Name);
            queryTreeCursor.Current = queryTreeCursor.Current.Branch(schemaCursor.Current)
        }
    }

    public class GraphQlSchemaCursor
    {
        public GraphQlSchemaCursor(GraphQlSchema schema)
        {
            Current = schema.Query;
        }

        public GraphQlObject Current { get; set; }
    }

    public class NullQueryParsedQueryNodeVisitor : ParsedQueryNodeVisitor
    {
        public ParsedQueryNode Visit(ParsedQueryNode node, ParsedQueryNode parent)
        {
            return node;
        }
    }
}
