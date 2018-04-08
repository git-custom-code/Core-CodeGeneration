namespace CustomCode.Core.CodeGeneration.Scripting
{
    using System.Threading.Tasks;

    public interface IScriptRunner
    {
        Task<IScript> ExecuteAsync(string path);
    }
}