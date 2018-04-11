namespace CustomCode.Core.CodeGeneration.Modelling.ClassModel.Tests
{
    using System.Collections.Generic;
    using Test.BehaviorDrivenDevelopment;
    using Xunit;

    [UnitTest]
    [Category("Modelling.ClassModel")]
    public sealed class ClassTests : TestCase
    {
        [Fact(DisplayName = "Class.ctor")]
        public void TestClassCreationSuccessfully()
        {
            Given()
            .When(() => new Class("ClassName", null))
            .Then(@class =>
                {
                    @class.Name.Should().Be("ClassName");
                    @class.Properties.Count.Should().Be(0);
                });
        }

        [Fact(DisplayName = "Class.ToString")]
        public void TestClassToStringSuccessfully()
        {
            Given(() => new Class("ClassName", null))
            .When(@class => @class.ToString())
            .Then(result => result.Should().Be("ClassName: 0 properties"));
        }

        [Fact(DisplayName = "Class.ToString")]
        public void TestClassWithPropertyToStringSuccessfully()
        {
            Given(() => new Class("ClassName", new HashSet<IProperty> { new Property("PropertyName", "PropertyType") }))
            .When(@class => @class.ToString())
            .Then(result => result.Should().Be("ClassName: 1 property"));
        }

        [Fact(DisplayName = "Class.GetHashCode")]
        public void TestClassGetHashCodeSuccessfully()
        {
            Given(() => new Class("ClassName", null))
            .When(@class => @class.GetHashCode())
            .Then(result => result.Should().Be("ClassName".GetHashCode()));
        }

        [Fact(DisplayName = "Class[property]")]
        public void TestClassGetPropertyByNameSuccessfully()
        {
            Given(() => new Class("ClassName", new HashSet<IProperty> { new Property("PropertyName", "PropertyType") }))
            .When(@class => @class["PropertyName"])
            .Then(result =>
                {
                    result.Name.Should().Be("PropertyName");
                    result.Type.Should().Be("PropertyType");
                });
        }

        [Fact(DisplayName = "Class[property]")]
        public void TestClassGetPropertyByInvalidNameSuccessfully()
        {
            Given(() => new Class("ClassName", new HashSet<IProperty> { new Property("PropertyName", "PropertyType") }))
            .When(@class => @class["InvalidName"])
            .Then(result => result.Should().BeNull());
        }
    }
}