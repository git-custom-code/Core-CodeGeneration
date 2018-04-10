namespace CustomCode.Core.CodeGeneration.Modelling.IO
{
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Type that allows asynchronous loading of persisted .json <see cref="IModel"/>s from disk.
    /// </summary>
    public sealed class JsonModelReader : IModelReader
    {
        #region Logic

        /// <summary>
        /// Asynchronously load a persisted .json <see cref="IModel"/> from disk.
        /// </summary>
        /// <param name="path"> The path to the persisted model. </param>
        /// <returns> The model's (type) id and content. </returns>
        public async Task<(Guid id, string model)> LoadFromAsync(string path)
        {
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true))
            using (var streamReader = new StreamReader(stream))
            {
                var json = await streamReader.ReadToEndAsync();
                using (var stringReader = new StringReader(json))
                using (var reader = new JsonTextReader(stringReader))
                {
                    while (reader.Read())
                    {
                        if (reader.TokenType == JsonToken.PropertyName &&
                            string.Equals(reader.Value as string, nameof(IModelFactory.Id), StringComparison.OrdinalIgnoreCase))
                        {
                            reader.Read();
                            var serializer = new JsonSerializer();
                            var id = serializer.Deserialize<Guid>(reader);
                            return (id: id, model: json);
                        }
                    }
                }
            }

            // ToDo: exception handling
            return (Guid.Empty, string.Empty);
        }

        #endregion
    }
}