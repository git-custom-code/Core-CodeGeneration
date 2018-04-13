namespace CustomCode.Core.CodeGeneration.Modelling.IO.Tests
{
    using Moq;
    using System;
    using Test.BehaviorDrivenDevelopment;
    using Xunit;

    /// <summary>
    /// Test cases for the <see cref="ModelRepository"/> type.
    /// </summary>
    [UnitTest]
    [Category("Modelling")]
    public sealed class ModelRepositoryTests : TestCase
    {
        [Fact(DisplayName = "Load model from repository")]
        public void LoadModelFromRepositorySucessfully()
        {
            Given<ModelRepository>()
            .With((IModelFactory factory) => factory.CreateAsnyc(default)).Returns(new Mock<IModel>().Object)
            .With((IModelReader reader) => reader.LoadFromAsync(default)).Returns((id: Guid.Empty, data: ""))
            .When(repository => repository.LoadAsync<IModel>("path"))
            .Then(model => model.ShouldNot().BeNull());
        }

        [Fact(DisplayName = "Load model from repository failed")]
        public void LoadModelFromRepositoryFailed()
        {
            Given<ModelRepository>()
            .With((IModelFactory factory) => factory.Id).Returns(Guid.NewGuid())
            .With((IModelReader reader) => reader.LoadFromAsync(default)).Returns((id: Guid.Empty, data: ""))
            .When(repository => repository.LoadAsync<IModel>("path"))
            .Then(model => model.Should().BeNull());
        }
    }
}