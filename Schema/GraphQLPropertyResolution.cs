namespace SystemDot.GraphQL.Schema
{
    using System;

    public class GraphQlPropertyResolution<TObject, TProperty> : GraphQlResolution
    {
        public GraphQlPropertyResolution(Func<TObject, TProperty> property)
        {
        }
    }
}