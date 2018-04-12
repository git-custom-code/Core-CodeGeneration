namespace CustomCode.Core.CodeGeneration.Modelling.ClassModel.Tests
{
    using Test.BehaviorDrivenDevelopment;
    using Xunit;

    [UnitTest]
    [Category("Modelling.ClassModel")]
    public sealed class ClassModelFactoryTests : TestCase
    {
        [Fact(DisplayName = "ClassConverter.ReadJson")]
        public void TestDeserializeIClassSuccessfully()
        {
            Given(() => new ClassModelFactory())
            .When(factory => factory.CreateAsnyc("{\"Id\":\"5bc5bb1f-60eb-419e-9458-c11f9a494271\",\"Namespace.Name\": {}}"))
            .Then(model =>
                {
                    model.ShouldNot().BeNull();
                    var classModel = model as IClassModel;
                    classModel.ShouldNot().BeNull();
                    classModel.Namespaces.Count.Should().Be(1);
                    classModel["Namespace.Name"].Name.Should().Be("Namespace.Name");
                    classModel["Namespace.Name"].Classes.Count.Should().Be(0);
                });
        }
    }
}