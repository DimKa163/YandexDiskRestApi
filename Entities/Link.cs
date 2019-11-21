namespace YandexDisk.Api.Entities
{
    using Newtonsoft.Json;
    /// <summary>
    /// Объект содержит URL для запроса метаданных ресурса.
    /// </summary>
    public class Link : IEntity
    {
        /// <summary>
        /// URL. Может быть шаблонизирован см. Template.
        /// </summary>
        [JsonProperty("href")]
        public string Href { get; set; }
        /// <summary>
        /// HTTP-метод для запроса URL из ключа Href.
        /// </summary>
        [JsonProperty("method")]
        public string Method { get; set; }
        /// <summary>
        /// Признак URL, который был шаблонизирован.
        /// </summary>
        [JsonProperty("templated")]
        public bool Template { get; set; }
    }
}
