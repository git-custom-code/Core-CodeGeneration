namespace CustomCode.Core.CodeGeneration.Modelling.IO.Tests
{
    using System;
    using Test.BehaviorDrivenDevelopment;
    using Xunit;

    /// <summary>
    /// Test cases for the <see cref="JsonModelReader"/> type.
    /// </summary>
    [Category("Modelling")]
    public sealed class JsonModelReaderTests : TestCase
    {
        [IntegrationTest]
        [Fact(DisplayName = "Load model from .json file")]
        public void LoadModelFromJsonFileSuccessfully()
        {
            Given(() => new JsonModelReader())
            .When(reader => reader.LoadFromAsync(@".\Data\ClassModel.json"))
            .Then(result =>
                {
                    result.id.Should().Be(new Guid("5bc5bb1f-60eb-419e-9458-c11f9a494001"));
                    result.data.Should().Be(@"{
    ""Id"": ""5bc5bb1f-60eb-419e-9458-c11f9a494001"",
    ""My.First.Namespace"": {
        ""ClassA"": {
            ""DecimalProperty"": ""decimal""
        }
    },
    ""My.Second.Namespace"": {
        ""ClassB"": {
            ""StringProperty"": ""string"",
            ""IntProperty"": ""int""
        },
        ""ClassC"": {
            ""ObjectProperty"": ""object""
        }
    }
}");
                });
        }
    }
}