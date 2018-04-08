namespace CustomCode.Core.CodeGeneration.Scripting
{
    using Microsoft.CodeAnalysis.CSharp.Scripting;
    using Microsoft.CodeAnalysis.Scripting;
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public sealed class ScriptRunner : IScriptRunner
    {
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

                using (var codeStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true))
                {
                    var script = await Task.Run(() => CSharpScript.Create(codeStream));
                    var compilation = script.GetCompilation();
                    var diagnostics = compilation.GetDiagnostics();
                    var state = await script.RunAsync();
                    var result = state.ReturnValue;
                }
            }
            catch(CompilationErrorException e)
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

        private async Task<string> LoadCodeAsync(string path)
        {
            var code = new StringBuilder();
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true))
            {
                var buffer = new byte[0x1000];
                int bytesRead;
                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    var codeChunk = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    code.Append(codeChunk);
                }
            }

            return code.ToString();
        }
    }
}