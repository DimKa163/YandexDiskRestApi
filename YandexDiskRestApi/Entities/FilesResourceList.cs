namespace YandexDisk.Api.Entities
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    /// <summary>
    /// Плоский список всех файлов на Диске в алфавитном порядке.
    /// </summary>
    public class FilesResourceList : IEntity
    {
        /// <summary>
        /// Массив последних загруженных файлов (Resource).
        /// </summary>
        [JsonProperty("items")]
        public List<Resource> Resources { get; set; }
        /// <summary>
        /// Максимальное количество элементов в массиве items, заданное в запросе.
        /// </summary>
        [JsonProperty("limit")]
        public int Limit { get; set; }
        /// <summary>
        /// Смещение начала списка от первого ресурса в папке.
        /// </summary>
        [JsonProperty("offset")]
        public int Offset { get; set; }
    }
}
