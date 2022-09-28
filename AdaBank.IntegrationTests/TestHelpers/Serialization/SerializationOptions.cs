using System.Text.Json;

namespace AdaBank.IntegrationTests.TestHelpers.Serialization
{
    public static class SerializationOptions
    {
        public static JsonSerializerOptions DefaultSerializationOptions => new() { IgnoreNullValues = true };

        public static JsonSerializerOptions DefaultDeserializationOptions => new() { PropertyNameCaseInsensitive = true };
    }
}