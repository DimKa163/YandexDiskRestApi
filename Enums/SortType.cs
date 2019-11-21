namespace YandexDisk.Api.Enums
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SortType
    {
        name, path, created, modified, size
    }
}
