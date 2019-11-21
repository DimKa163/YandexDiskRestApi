namespace YandexDisk.Api.Enums
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FileType
    {
        /// <summary>
        /// Папка.
        /// </summary>
        dir,
        /// <summary>
        /// Файл.
        /// </summary>
        file
    }
}
