namespace SystemDot.GraphQL.Schema
{
    using System;

    public class GraphQlFunctionResolution<TObject> : GraphQlResolution
    {
        public GraphQlFunctionResolution(Func<TObject> function)
        {
        }
    }
}