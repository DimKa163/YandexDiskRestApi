namespace YandexDisk.Api.Entities
{
    using Newtonsoft.Json;
    /// <summary>
    /// Данные о свободном и занятом пространстве на Диске
    /// </summary>
    public class Disk : IEntity
    {
        /// <summary>
        /// Объем файлов, находящихся в Корзине, в байтах.
        /// </summary>
        [JsonProperty("trash_size")]
        public long TrashSize { get; set; }
        /// <summary>
        /// Общий объем Диска, доступный пользователю, в байтах.
        /// </summary>
        [JsonProperty("total_space")]
        public long TotalSpace { get; set; }
        /// <summary>
        /// Объем файлов, уже хранящихся на Диске, в байтах.
        /// </summary>
        [JsonProperty("used_space")]
        public long UsedSpace { get; set; }
        /// <summary>
        /// Абсолютные адреса системных папок Диска. Имена папок зависят от языка интерфейса пользователя в момент создания персонального Диска. Например, для англоязычного пользователя создается папка Downloads, для русскоязычного — Загрузки и т. д.
        /// </summary>
        [JsonProperty("system_folders")]
        public SystemFolders Folders { get; set; }
        /// <summary>
        /// Данные о пользователе
        /// </summary>
        [JsonProperty("user")]
        public User User { get; set; }
        public class SystemFolders
        {
            [JsonProperty("applications")]
            public string Application { get; set; }
            [JsonProperty("downloads")]
            public string Downloads { get; set; }
            [JsonProperty("odnoklassniki")]
            public string Odnoklassniki { get; set; }
            [JsonProperty("google")]
            public string Google { get; set; }
            [JsonProperty("instagram")]
            public string Instagram { get; set; }
            [JsonProperty("vkontakte")]
            public string Vkontakte { get; set; }
            [JsonProperty("mailru")]
            public string MailRu { get; set; }
            [JsonProperty("facebook")]
            public string Facebook { get; set; }
            [JsonProperty("social")]
            public string Social { get; set; }
            [JsonProperty("screenshots")]
            public string Screenshots { get; set; }
            [JsonProperty("photostream")]
            public string Photostream { get; set; }
        }
    }
}
