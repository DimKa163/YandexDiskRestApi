namespace YandexDisk.Api.Entities
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;
    using System.Collections.Generic;
    using YandexDisk.Api.Enums;

    /// <summary>
    /// Описание ресурса, мета-информация о файле или папке. Включается в ответ на запрос метаинформации.
    /// </summary>
    public class Resource : IEntity
    {
        /// <summary>
        /// Ключ опубликованного ресурса.
        /// Включается в ответ только если указанный файл или папка опубликован.
        /// </summary>
        [JsonProperty("public_key")]
        public string PublicKey { get; set; }
        /// <summary>
        /// Ресурсы, непосредственно содержащиеся в папке (содержит объект ResourceList).
        /// Включается в ответ только при запросе метаинформации о папке.
        /// </summary>
        [JsonProperty("_embedded")]
        public ResourceList ResourceList { get; set; }
        /// <summary>
        /// Имя ресурса.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// Дата и время создания ресурса.
        /// </summary>
        [JsonProperty("created")]
        [JsonConverter(typeof(JavaScriptDateTimeConverter))]
        public DateTime Created { get; set; }
        /// <summary>
        /// Объект со всеми атрибутами, заданными с помощью запроса Добавление метаинформации для ресурса. Содержит только ключи вида имя:значение (объекты или массивы содержать не может).
        /// </summary>
        [JsonProperty("custom_properties")]
        public Dictionary<string, int> CustomProperty { get; set; }
        /// <summary>
        /// Ссылка на опубликованный ресурс.
        /// Включается в ответ только если указанный файл или папка опубликован.
        /// </summary>
        [JsonProperty("public_url")]
        public string PublicUrl { get; set; }
        /// <summary>
        /// Путь к ресурсу до перемещения в Корзину.
        /// Включается в ответ только для запроса метаинформации о ресурсе в Корзине.
        /// </summary>
        [JsonProperty("origin_path")]
        public string OriginalPath { get; set; }
        /// <summary>
        /// Дата и время изменения ресурса, в формате
        /// </summary>
        [JsonProperty("modified")]
        [JsonConverter(typeof(JavaScriptDateTimeConverter))]
        public DateTime Modified { get; set; }
        /// <summary>
        /// Полный путь к ресурсу на Диске.
        /// В метаинформации опубликованной папки пути указываются относительно самой папки. Для опубликованных файлов значение ключа всегда «/».
        /// Для ресурса, находящегося в Корзине, к атрибуту может быть добавлен уникальный идентификатор (например, trash:/foo_1408546879). С помощью этого идентификатора ресурс можно отличить от других удаленных ресурсов с тем же именем.
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }
        /// <summary>
        /// MD5-хэш файла.
        /// </summary>
        [JsonProperty("md5")]
        public string MD5 { get; set; }
        /// <summary>
        /// Тип ресурса:
        /// «dir» — папка;
        /// «file» — файл.
        /// </summary>
        [JsonProperty("type")]
        public FileType Type { get; set; }
        /// <summary>
        /// MIME-тип файла.
        /// </summary>
        [JsonProperty("mime_type")]
        public string MimeType { get; set; }
        /// <summary>
        /// Размер файла.
        /// </summary>
        [JsonProperty("size")]
        public long Size { get; set; }
    }
}
