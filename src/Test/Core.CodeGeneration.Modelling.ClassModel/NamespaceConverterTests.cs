namespace CustomCode.Core.CodeGeneration.Modelling.ClassModel.Tests
{
    using Newtonsoft.Json;
    using Test.BehaviorDrivenDevelopment;
    using Xunit;

    [UnitTest]
    [Category("Modelling.ClassModel")]
    public sealed class NamespaceConverterTests : TestCase
    {
        [Fact(DisplayName = "NamespaceConverter.ReadJson")]
        public void TestDeserializeINamespaceSuccessfully()
        {
            Given(() => "{\"NamespaceName\":{\"ClassName\":{}}}")
            .When(json => JsonConvert.DeserializeObject<INamespace>(json))
            .Then(@class =>
                {
                    @class.Name.Should().Be("NamespaceName");
                    @class.Classes.Count.Should().Be(1);
                    @class["ClassName"].Name.Should().Be("ClassName");
                    @class["ClassName"].Properties.Count.Should().Be(0);
                });
        }
    }
}