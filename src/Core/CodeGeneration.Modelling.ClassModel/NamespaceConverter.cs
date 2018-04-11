namespace CustomCode.Core.CodeGeneration.Modelling.ClassModel
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Custom <see cref="JsonConverter"/> for the <see cref="INamespace"/> type.
    /// </summary>
    public sealed class NamespaceConverter : JsonConverter<INamespace>
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
        public override INamespace ReadJson(
            JsonReader reader, Type objectType, INamespace existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                reader.Read();
            }

            if (reader.TokenType != JsonToken.PropertyName)
            {
                return null;
            }

            var @namespace = JProperty.Load(reader);
            var name = @namespace.Name;
            var @object = @namespace.Value as JObject;
            var classes = new HashSet<IClass>();
            foreach (var property in @object.Properties())
            {
                if (property.Value.Type == JTokenType.Object)
                {
                    var @class = property.ToObject<IClass>(serializer);
                    classes.Add(@class);
                }
            }

            return new Namespace(name, classes);
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer"> The <see cref="JsonWriter"/> to write to. </param>
        /// <param name="value"> The value. </param>
        /// <param name="serializer"> The calling serializer. </param>

        public override void WriteJson(JsonWriter writer, INamespace value, JsonSerializer serializer)
        {
            writer.WritePropertyName(value.Name);
            writer.WriteStartObject();

            foreach (var @class in value.Classes)
            {
                serializer.Serialize(writer, @class);
            }

            writer.WriteEndObject();
        }

        #endregion
    }
}