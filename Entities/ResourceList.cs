namespace YandexDisk.Api.Entities
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    /// <summary>
    /// Список ресурсов, содержащихся в папке. Содержит объекты Resource и свойства списка.
    /// </summary>
    public class ResourceList : IEntity
    {
        /// <summary>
        /// Поле, по которому отсортирован список.
        /// </summary>
        [JsonProperty("sort")]
        public string Sort { get; set; }
        /// <summary>
        /// Ключ опубликованной папки, в которой содержатся ресурсы из данного списка.
        /// Включается только в ответ на запрос метаинформации о публичной папке.
        /// </summary>
        [JsonProperty("public_key")]
        public string PublicKey { get; set; }
        /// <summary>
        /// Массив ресурсов (Resource), содержащихся в папке.
        /// Вне зависимости от запрошенной сортировки, ресурсы в массиве упорядочены по их виду: сначала перечисляются все вложенные папки, затем — вложенные файлы.
        /// </summary>
        [JsonProperty("items")]
        public List<Resource> Items { get; set; }
        /// <summary>
        /// Максимальное количество элементов в массиве items, заданное в запросе.
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }
        /// <summary>
        /// Смещение начала списка от первого ресурса в папке.
        /// </summary>
        [JsonProperty("limit")]
        public int Limit { get; set; }
        /// <summary>
        /// Путь к папке, чье содержимое описывается в данном объекте ResourceList.
        /// Для публичной папки значение атрибута всегда равно «/».
        /// </summary>
        [JsonProperty("offset")]
        public int Offset { get; set; }
        /// <summary>
        /// Общее количество ресурсов в папке.
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
