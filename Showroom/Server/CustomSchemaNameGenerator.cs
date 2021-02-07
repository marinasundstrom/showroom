
using System;

using NJsonSchema.Generation;

namespace Showroom.Server
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
