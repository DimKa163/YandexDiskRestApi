namespace YandexDisk.Api.Entities
{
    using Newtonsoft.Json;
    public class YandexApiDiskError : IEntity
    {
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
