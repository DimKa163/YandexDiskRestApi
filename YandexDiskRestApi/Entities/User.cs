namespace YandexDisk.Api.Entities
{
    using Newtonsoft.Json;
    public class User : IEntity
    {
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("login")]
        public string Login { get; set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
        [JsonProperty("uid")]
        public long UId { get; set; }
    }
}
