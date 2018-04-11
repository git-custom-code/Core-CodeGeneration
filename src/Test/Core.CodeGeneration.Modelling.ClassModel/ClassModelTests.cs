namespace CustomCode.Core.CodeGeneration.Modelling.ClassModel.Tests
{
    using System.Collections.Generic;
    using Test.BehaviorDrivenDevelopment;
    using Xunit;

    [UnitTest]
    [Category("Modelling.ClassModel")]
    public sealed class ClassModelTests : TestCase
    {
        [Fact(DisplayName = "ClassModel.ctor")]
        public void TestClassModelCreationSuccessfully()
        {
            Given()
            .When(() => new ClassModel(null))
            .Then(model => model.Namespaces.Count.Should().Be(0));
        }

        [Fact(DisplayName = "ClassModel.ToString")]
        public void TestClassModelToStringSuccessfully()
        {
            Given(() => new ClassModel(null))
            .When(model => model.ToString())
            .Then(result => result.Should().Be("0 namespaces"));
        }

        [Fact(DisplayName = "ClassModel.ToString")]
        public void TestClassModelWithNamespaceToStringSuccessfully()
        {
            Given(() => new ClassModel(new HashSet<INamespace> { new Namespace("NamespaceName", null) }))
            .When(model => model.ToString())
            .Then(result => result.Should().Be("1 namespace"));
        }

        [Fact(DisplayName = "ClassModel[namespace]")]
        public void TestClassModelGetNamespaceByNameSuccessfully()
        {
            Given(() => new ClassModel(new HashSet<INamespace> { new Namespace("NamespaceName", null) }))
            .When(model => model["NamespaceName"])
            .Then(result =>
                {
                    result.Name.Should().Be("NamespaceName");
                    result.Classes.Count.Should().Be(0);
                });
        }

        [Fact(DisplayName = "ClassModel[namespace]")]
        public void TestClassModelGetNamespaceByInvalidNameSuccessfully()
        {
            Given(() => new ClassModel(new HashSet<INamespace> { new Namespace("NamespaceName", null) }))
            .When(model => model["InvalidName"])
            .Then(result => result.Should().BeNull());
        }
    }
}