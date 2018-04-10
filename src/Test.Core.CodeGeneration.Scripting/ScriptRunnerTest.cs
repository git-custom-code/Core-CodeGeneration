namespace CustomCode.Core.CodeGeneration.Scripting.Tests
{
    using Features;
    using Xunit;

    public sealed class ScriptRunnerTest
    {
        [Fact]
        public async void Test()
        {
            var runner = new ScriptRunner(new IFeatureAnalyzer[] { new ParameterCollectionAnalyzer(), new ResultCollectionAnalyzer() });
            var script = await runner.ExecuteAsync(@"Scripts\Parameter\MultipleParameter.csx", ("A", 2), ("B", 5));
            script = await runner.ExecuteAsync(@"Scripts\Result\OutValues.csx");
            await runner.ExecuteAsync(@"Scripts\Result\SingleReturnValue.csx");
        }
    }
}