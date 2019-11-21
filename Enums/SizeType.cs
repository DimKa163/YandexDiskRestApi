namespace YandexDisk.Api.Enums
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SizeType
    {
        S, M, L, XL, XXL, XXXL
    }
}
