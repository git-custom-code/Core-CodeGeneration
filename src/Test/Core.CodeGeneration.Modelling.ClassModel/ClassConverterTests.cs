namespace CustomCode.Core.CodeGeneration.Modelling.ClassModel.Tests
{
    using Newtonsoft.Json;
    using Test.BehaviorDrivenDevelopment;
    using Xunit;

    [UnitTest]
    [Category("Modelling.ClassModel")]
    public sealed class ClassConverterTests : TestCase
    {
        [Fact(DisplayName = "ClassConverter.ReadJson")]
        public void TestDeserializeIClassSuccessfully()
        {
            Given(() => "{\"ClassName\":{\"PropertyName\":\"PropertyType\"}}")
            .When(json => JsonConvert.DeserializeObject<IClass>(json))
            .Then(@class =>
                {
                    @class.Name.Should().Be("ClassName");
                    @class.Properties.Count.Should().Be(1);
                    @class["PropertyName"].Name.Should().Be("PropertyName");
                    @class["PropertyName"].Type.Should().Be("PropertyType");
                });
        }
    }
}