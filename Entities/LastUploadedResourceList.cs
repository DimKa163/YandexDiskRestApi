namespace YandexDisk.Api.Entities
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    /// <summary>
    /// Список последних добавленных на Диск файлов, отсортированных по дате загрузки (от поздних к ранним).
    /// </summary>
    public class LastUploadedResourceList : IEntity
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
    }
}
