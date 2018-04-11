namespace CustomCode.Core.CodeGeneration.Modelling.ClassModel.Tests
{
    using System.Collections.Generic;
    using Test.BehaviorDrivenDevelopment;
    using Xunit;

    [UnitTest]
    [Category("Modelling.ClassModel")]
    public sealed class NamespaceTests : TestCase
    {
        [Fact(DisplayName = "Namespace.ctor")]
        public void TestNamespaceCreationSuccessfully()
        {
            Given()
            .When(() => new Namespace("NamespaceName", null))
            .Then(@namespace =>
                {
                    @namespace.Name.Should().Be("NamespaceName");
                    @namespace.Classes.Count.Should().Be(0);
                });
        }

        [Fact(DisplayName = "Namespace.ToString")]
        public void TestNamespaceToStringSuccessfully()
        {
            Given(() => new Namespace("NamespaceName", null))
            .When(@namespace => @namespace.ToString())
            .Then(result => result.Should().Be("NamespaceName: 0 classes"));
        }

        [Fact(DisplayName = "Namespace.ToString")]
        public void TestNamespaceWithClassesToStringSuccessfully()
        {
            Given(() => new Namespace("NamespaceName", new HashSet<IClass> { new Class("ClassName", null) }))
            .When(@namespace => @namespace.ToString())
            .Then(result => result.Should().Be("NamespaceName: 1 class"));
        }

        [Fact(DisplayName = "Namespace.GetHashCode")]
        public void TestNamespaceGetHashCodeSuccessfully()
        {
            Given(() => new Namespace("NamespaceName", null))
            .When(@namespace => @namespace.GetHashCode())
            .Then(result => result.Should().Be("NamespaceName".GetHashCode()));
        }

        [Fact(DisplayName = "Namespace[class]")]
        public void TestNamespaceGetClassByNameSuccessfully()
        {
            Given(() => new Namespace("NamespaceName", new HashSet<IClass> { new Class("ClassName", null) }))
            .When(@namespace => @namespace["ClassName"])
            .Then(result =>
                {
                    result.Name.Should().Be("ClassName");
                    result.Properties.Count.Should().Be(0);
                });
        }

        [Fact(DisplayName = "Namespace[class]")]
        public void TestNamespaceGetClassByInvalidNameSuccessfully()
        {
            Given(() => new Namespace("NamespaceName", new HashSet<IClass> { new Class("ClassName", null) }))
            .When(@namespace => @namespace["InvalidName"])
            .Then(result => result.Should().BeNull());
        }
    }
}