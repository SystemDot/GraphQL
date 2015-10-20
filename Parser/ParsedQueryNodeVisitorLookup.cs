namespace SystemDot.GraphQL.Parser
{
    using System;
    using System.Collections.Generic;

    public class ParsedQueryNodeVisitorLookup
    {
        readonly Dictionary<Type, ParsedQueryNodeVisitor> inner;

        public ParsedQueryNodeVisitorLookup()
        {
            inner = new Dictionary<Type, ParsedQueryNodeVisitor>();
        }

        public ParsedQueryNodeVisitor Lookup(Type nodeType)
        {
            return inner[nodeType];
        }

        public void RegisterForNode<TNode>(ParsedQueryNodeVisitor toRegister) where TNode : ParsedQueryNode
        {
            inner.Add(typeof(TNode), toRegister);
        }
    }
}