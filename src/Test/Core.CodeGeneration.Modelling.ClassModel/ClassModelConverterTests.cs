namespace CustomCode.Core.CodeGeneration.Modelling.ClassModel.Tests
{
    using Newtonsoft.Json;
    using Test.BehaviorDrivenDevelopment;
    using Xunit;

    [UnitTest]
    [Category("Modelling.ClassModel")]
    public sealed class ClassModelConverterTests : TestCase
    {
        [Fact(DisplayName = "ClassModelConverter.ReadJson")]
        public void TestDeserializeIClassModelSuccessfully()
        {
            Given(() => "{\"Id\":\"5bc5bb1f-60eb-419e-9458-c11f9a494271\",\"Namespace.Name\": {}}")
            .When(json => JsonConvert.DeserializeObject<IClassModel>(json))
            .Then(model => model.Namespaces.Count.Should().Be(1));
        }
    }
}