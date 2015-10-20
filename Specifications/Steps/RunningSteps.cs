
namespace SystemDot.GraphQL.Specifications.Steps
{
    using System.Collections.Generic;
    using TechTalk.SpecFlow;
    using SystemDot.GraphQL.Runner;
    using SystemDot.GraphQL.Schema;
    using FluentAssertions;

    [Binding]
    public class RunningSteps
    {
        RootDataSourceObject rootDataSource;
        string result;
        GraphQlType currentType;
        GraphQlResolution currentResolution;
        List<GraphQlField> currentFields = new List<GraphQlField>();
        GraphQlSchema schema;

        [Given(@"I have a root data source object")]
        public void GivenIHaveARootDataSourceObject()
        {
            rootDataSource = new RootDataSourceObject();
        }

        [Given(@"that data source has a value property of '(.*)'")]
        public void GivenThatDataSourceHasAValuePropertyOf(string propertyValue)
        {
            rootDataSource.Property = propertyValue;
        }

        [Given(@"that data source has an other value property of '(.*)'")]
        public void GivenThatDataSourceHasAnOtherValuePropertyOf(string propertyValue)
        {
            rootDataSource.OtherProperty = propertyValue;
        }
        
        [Given(@"I have a string type")]
        public void GivenIHaveAStringType()
        {
            currentType = new GraphQlString();
        }

        [Given(@"I have a resolution that resolves the root data source object value property")]
        public void GivenIHaveAResolutionThatResolvesTheRootDataSourceObjectValueProperty()
        {
            currentResolution = new GraphQlFunctionResolution<RootDataSourceObject>(() => rootDataSource);
        }

        [Given(@"I have a field named '(.*)' with that type and resolution")]
        public void GivenIHaveAFieldNamedWithThatTypeAndResolution(string name)
        {
            currentFields.Add(new GraphQlField(name, currentType, currentResolution));
        }

        [Given(@"I have an object type that contains that field")]
        [Given(@"I have an object type that contains those fields")]
        public void GivenIHaveAnObjectTypeThatContainsThatField()
        {
            currentType = new GraphQlObject(currentFields.ToArray());
            currentFields = new List<GraphQlField>();
        }

        [Given(@"I have a resolution that resolves the root data source object")]
        public void GivenIHaveAResolutionThatResolvesTheRootDataSourceObject()
        {
            currentResolution = new GraphQlPropertyResolution<RootDataSourceObject, string>(o => o.Property);
        }

        [Given(@"I have a resolution that resolves the root data source object other value property")]
        public void GivenIHaveAResolutionThatResolvesTheRootDataSourceObjectOtherValueProperty()
        {
            currentResolution = new GraphQlPropertyResolution<RootDataSourceObject, string>(o => o.OtherProperty);
        }

        [Given(@"I have a schema that has a query of that type")]
        public void GivenIHaveASchemaThatHasAQueryOfThatType()
        {
            schema = new GraphQlSchema(currentType.As<GraphQlObject>());
        }
        
        [When(@"I run the query")]
        public void WhenIRunTheQuery(string query)
        {
            result = Runner.Run(query, schema);
        }

        [Then(@"the query result should be")]
        public void ThenTheQueryResultShouldBe(string expectedResult)
        {
            result.Should().Be(expectedResult);
        }
    }
}
