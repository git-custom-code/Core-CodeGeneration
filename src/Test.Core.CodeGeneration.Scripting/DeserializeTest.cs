namespace CustomCode.Core.CodeGeneration.Scripting.Tests
{
    using Modelling.ClassModel;
    using Newtonsoft.Json;
    using Xunit;

    public sealed class DeserializeTest
    {
        [Fact]
        public void Test()
        {
            var model = JsonConvert.DeserializeObject<IClassModel>(@"
                {
                    ""Id"": ""5bc5bb1f-60eb-419e-9458-c11f9a494271"",
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
                }
            ");
            var json = JsonConvert.SerializeObject(model, Formatting.Indented);
        }
    }
}