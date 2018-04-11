namespace CustomCode.Core.CodeGeneration.Modelling.ClassModel
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Custom <see cref="JsonConverter"/> for the <see cref="IClass"/> type.
    /// </summary>
    public sealed class ClassConverter : JsonConverter<IClass>
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
        public override IClass ReadJson(
            JsonReader reader, Type objectType, IClass existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                reader.Read();
            }

            if (reader.TokenType != JsonToken.PropertyName)
            {
                return null;
            }

            var @class = JProperty.Load(reader);
            var name = @class.Name;
            var @object = @class.Value as JObject;
            var properties = new HashSet<IProperty>();
            foreach (var property in @object)
            {
                var p = new Property(property.Key, property.Value.Value<string>());
                properties.Add(p);
            }

            return new Class(name, properties);
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer"> The <see cref="JsonWriter"/> to write to. </param>
        /// <param name="value"> The value. </param>
        /// <param name="serializer"> The calling serializer. </param>
        public override void WriteJson(JsonWriter writer, IClass value, JsonSerializer serializer)
        {
            writer.WritePropertyName(value.Name);
            writer.WriteStartObject();

            foreach (var property in value.Properties)
            {
                writer.WritePropertyName(property.Name);
                writer.WriteValue(property.Type);
            }

            writer.WriteEndObject();
        }

        #endregion
    }
}