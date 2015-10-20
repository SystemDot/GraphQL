namespace SystemDot.GraphQL.Schema
{
    public class GraphQlObject : GraphQlType
    {
        public GraphQlField[] Fields { get; private set; }

        public GraphQlObject(params GraphQlField[] fields)
        {
            Fields = fields;
        }
    }
}