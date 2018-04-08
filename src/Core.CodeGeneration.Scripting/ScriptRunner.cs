namespace CustomCode.Core.CodeGeneration.Scripting
{
    using Features;
    using Microsoft.CodeAnalysis.CSharp.Scripting;
    using Microsoft.CodeAnalysis.Scripting;
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;
 
    public sealed class ScriptRunner : IScriptRunner
    {
        #region Dependencies

        /// <summary>
        /// Creates a new instance of the <see cref="ScriptRunner"/> type.
        /// </summary>
        /// <param name="featureAnalyzers"> A collection with available <see cref="IFeatureAnalyzer"/>s. </param>
        public ScriptRunner(IEnumerable<IFeatureAnalyzer> featureAnalyzers)
        {
            FeatureAnalyzers = featureAnalyzers;
        }

        /// <summary>
        /// Gets a collection with available <see cref="IFeatureAnalyzer"/>s.
        /// </summary>
        private IEnumerable<IFeatureAnalyzer> FeatureAnalyzers { get; }

        #endregion

        public async Task<IScript> ExecuteAsync(string path)
        {
            try
            {
                if (string.IsNullOrEmpty(path))
                {
                    throw new ArgumentNullException(path);
                }

                path = GetRootedPath(ref path);

                if (!File.Exists(path))
                {
                    throw new FileNotFoundException($"A script file with the path <{path}> was not found.");
                }

                var features = new HashSet<IFeature>();
                using (var codeStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true))
                {
                    var options = CreateOptions();
                    var script = await Task.Run(() => CSharpScript.Create(codeStream, options, globalsType: typeof(ScriptContext)));
                    var code = script.Code;
                    var compilation = script.GetCompilation();

                    foreach (var analyzer in FeatureAnalyzers)
                    {
                        if (analyzer.HasFeature(compilation.SyntaxTrees, out var feature))
                        {
                            features.Add(feature);
                        }
                    }

                    var diagnostics = compilation.GetDiagnostics();
                    var state = await script.RunAsync(new ScriptContext());
                    var result = state.ReturnValue;

                    return new Script(code, features);
                }
            }
            catch (CompilationErrorException e)
            {
                // ToDo
            }

            return null;
        }

        private string GetRootedPath(ref string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            if (!Path.IsPathRooted(path))
            {
                var assembly = typeof(ScriptRunner).GetTypeInfo().Assembly;
                var uri = new UriBuilder(assembly.CodeBase);
                var assemblyPath = Uri.UnescapeDataString(uri.Path);
                assemblyPath = Path.GetDirectoryName(assemblyPath);
                return Path.Combine(assemblyPath, path);
            }

            return path;
        }

        private ScriptOptions CreateOptions()
        {
            var options = ScriptOptions
                .Default
                .WithReferences(new[]
                    {
                        typeof(DynamicObject).GetTypeInfo().Assembly, // System.Dynamic.Runtime
                        typeof(Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo).GetTypeInfo().Assembly // Microsoft.CSharp
                    })
                .WithImports(new[]
                    {
                            typeof(DynamicObject).GetTypeInfo().Namespace // System.Dynamic
                    });
            return options;
        }
    }
}