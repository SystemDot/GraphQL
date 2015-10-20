namespace SystemDot.GraphQL.Schema
{
    public class GraphQlField : GraphQlNode
    {
        readonly string name;
        readonly GraphQlType type;
        readonly GraphQlResolution resolution;

        public GraphQlField(string name, GraphQlType type, GraphQlResolution resolution)
        {
            this.name = name;
            this.type = type;
            this.resolution = resolution;
        }
    }
}