namespace YandexDisk.Api.Entities
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using YandexDisk.Api.Enums;

    /// <summary>
    /// Список опубликованных файлов на Диске.
    /// </summary>
    public class PublicResourcesList : IEntity
    {
        /// <summary>
        /// Массив последних загруженных файлов (Resource).
        /// </summary>
        [JsonProperty("items")]
        public List<Resource> Resources { get; set; }
        /// <summary>
        /// Тип ресурса:
        /// «dir» — папка;
        /// «file» — файл.
        /// </summary>
        [JsonProperty("type")]
        public FileType FileType { get; set; }
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
