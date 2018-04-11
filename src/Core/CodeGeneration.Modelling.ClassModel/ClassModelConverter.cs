namespace CustomCode.Core.CodeGeneration.Modelling.ClassModel
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Custom <see cref="JsonConverter{T}"/> for the <see cref="IClassModel"/> type.
    /// </summary>
    public sealed class ClassModelConverter : JsonConverter<IClassModel>
    {
        #region Logic

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader"> The <see cref="JsonReader"/> to read from. </param>
        /// <param name="objectType"> Type of the object. </param>
        /// <param name="existingValue">
        /// The existing value of object being read. If there is no existing value then null will be used.
        /// </param>
        /// <param name="hasExistingValue"> The existing value has a value. </param>
        /// <param name="serializer"> The calling serializer. </param>
        /// <returns> The object value. </returns>
        public override IClassModel ReadJson(
            JsonReader reader, Type objectType, IClassModel existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var @object = JObject.Load(reader);
            var namespaces = new HashSet<INamespace>();
            foreach (var property in @object)
            {
                // ToDo what about objects that are not namespaces? ignore all other properties?

                if (property.Value.Type == JTokenType.Object)
                {
                    var @namespace = property.Value.ToObject<INamespace>(serializer);
                    namespaces.Add(@namespace);
                }
            }

            return new ClassModel(namespaces);
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer"> The <see cref="JsonWriter"/> to write to. </param>
        /// <param name="value"> The value. </param>
        /// <param name="serializer"> The calling serializer. </param>
        public override void WriteJson(JsonWriter writer, IClassModel value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("Id");
            writer.WriteValue(ClassModelFactory.TypeId);

            foreach (var @namespace in value.Namespaces)
            {
                serializer.Serialize(writer, @namespace);
            }

            writer.WriteEndObject();
        }

        #endregion
    }
}