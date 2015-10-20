namespace SystemDot.GraphQL.Schema
{
    public class GraphQlSchema : GraphQlObject
    {
        public GraphQlSchema(GraphQlObject query): base(CreateFields(query))
        {
        }

        static GraphQlField[] CreateFields(GraphQlObject query)
        {
            return new[]
            {
                new GraphQlField("Query", query, new GraphQlResolution())
            };
        }
    }
}
