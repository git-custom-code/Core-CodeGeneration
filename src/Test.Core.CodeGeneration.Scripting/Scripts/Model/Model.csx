#r "Core.CodeGeneration.Modelling.ClassModel"

using System;
using CustomCode.Core.CodeGeneration.Modelling.ClassModel;

var model = await Models.LoadAsync<IClassModel>(@".\Models\ClassModel.json");
foreach(var @namespace in model.Namespaces)
{
    foreach(var @class in @namespace.Classes)
    {
        Code.CreateOrGetFile($"{@class.Name}.cs")
            .EmitOnce($@"
                namespace {@namespace.Name}
                {{
                    public sealed class {@class.Name}
                    {{
                        #region Data")
            .EmitForEach(@class.Properties, property => $@"

                        public {property.Type} {property.Name} {{ get; set }}")
            .EmitOnce($@"

                        #endregion
                    }}
                }}");
    }
};