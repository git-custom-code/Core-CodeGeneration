namespace CustomCode.Core.CodeGeneration.Scripting.Tests
{
    using Composition;
    using Features;
    using LightInject;
    using System.Reflection;
    using Xunit;

    public sealed class ScriptRunnerTest
    {
        [Fact]
        public async void Test()
        {
            var container = new ServiceContainer();
            container.UseAttributeConventions();
            container.RegisterIocVisibleAssemblies(typeof(ScriptRunnerTest).GetTypeInfo().Assembly.Location);
            var runner = container.GetInstance<IScriptRunner>();

            //var runner = new ScriptRunner(new IFeatureAnalyzer[] { new ParameterCollectionAnalyzer(), new ResultCollectionAnalyzer(), new ModelCollectionAnalyzer() });
            var script = await runner.ExecuteAsync(@"Scripts\Model\Model.csx");
            script = await runner.ExecuteAsync(@"Scripts\Parameter\MultipleParameter.csx", ("A", 2), ("B", 5));
            script = await runner.ExecuteAsync(@"Scripts\Result\OutValues.csx");
            await runner.ExecuteAsync(@"Scripts\Result\SingleReturnValue.csx");
        }
    }
}