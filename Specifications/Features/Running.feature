Feature: Running

Scenario: Single root single field
	Given I have a root data source object
	And that data source has a value property of 'Test' 
	And I have a string type
	And I have a resolution that resolves the root data source object value property
	And I have a field named 'testTypePropertyField' with that type and resolution
	And I have an object type that contains that field
	And I have a resolution that resolves the root data source object
	And I have a field named 'testField' with that type and resolution
	And I have an object type that contains that field
	And I have a schema that has a query of that type
	When I run the query 
	"""
{ 
	testField {
		testTypePropertyField
		}
}
	"""
	Then the query result should be
	"""
{
   "data": {
     "testField": {
       "testTypePropertyField": "Test"
     }
   }
}
	"""

Scenario: Single root two fields
	Given I have a root data source object
	And that data source has a value property of 'Test' 
	And that data source has an other value property of 'OtherTest' 
	And I have a string type
	And I have a resolution that resolves the root data source object value property
	And I have a field named 'testTypePropertyField' with that type and resolution
	And I have a string type
	And I have a resolution that resolves the root data source object other value property
	And I have a field named 'testOtherTypePropertyField' with that type and resolution
	And I have an object type that contains those fields
	And I have a resolution that resolves the root data source object
	And I have a field named 'testField' with that type and resolution
	And I have an object type that contains that field
	And I have a schema that has a query of that type
	When I run the query 
	"""
query Q { 
	testField {
		testTypePropertyField
		testOtherTypePropertyField
		}
	}
	"""
	Then the query result should be
	"""
{
   "data": {
     "testField": {
       "testTypePropertyField": "Test"
       "testOtherTypePropertyField": "OtherTest"
     }
   }
}
	"""
	
