namespace CustomCode.Core.CodeGeneration.Modelling.ClassModel.Tests
{
    using Test.BehaviorDrivenDevelopment;
    using Xunit;

    [UnitTest]
    [Category("Modelling.ClassModel")]
    public sealed class PropertyTests : TestCase
    {
        [Fact(DisplayName = "Property.ctor")]
        public void TestPropertyCreationSuccessfully()
        {
            Given()
            .When(() => new Property("PropertyName", "PropertyType"))
            .Then(property =>
                {
                    property.Name.Should().Be("PropertyName");
                    property.Type.Should().Be("PropertyType");
                });
        }

        [Fact(DisplayName = "Property.ToString")]
        public void TestPropertyToStringSuccessfully()
        {
            Given(() => new Property("PropertyName", "PropertyType"))
            .When(property => property.ToString())
            .Then(result => result.Should().Be("PropertyName: PropertyType"));
        }

        [Fact(DisplayName = "Property.GetHashCode")]
        public void TestPropertyGetHashCodeSuccessfully()
        {
            Given(() => new Property("PropertyName", "PropertyType"))
            .When(property => property.GetHashCode())
            .Then(result => result.Should().Be("PropertyName".GetHashCode()));
        }
    }
}