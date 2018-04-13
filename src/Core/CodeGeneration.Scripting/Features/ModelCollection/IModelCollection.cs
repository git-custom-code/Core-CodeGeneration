namespace CustomCode.Core.CodeGeneration.Scripting.Features
{
    using System.Collections.Generic;

    /// <summary>
    /// Feature for <see cref="IScript"/>s that use <see cref="Modelling.IModel"/> instances.
    /// </summary>
    public interface IModelCollection : IFeature, IEnumerable<(string name, string path)>
    { }
}