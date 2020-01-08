
using System;

using NJsonSchema.Generation;

namespace Essiq.Showroom.Server
{
    internal class CustomSchemaNameGenerator : ISchemaNameGenerator
    {
        public string Generate(Type type)
        {
            return type.Name
                .Replace("Dto", string.Empty);
        }
    }
}
