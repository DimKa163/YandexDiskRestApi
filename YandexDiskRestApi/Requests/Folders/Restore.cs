namespace YandexDisk.Api.Requests
{
    using System.Net;
    using System.Net.Http;
    using YandexDisk.Api.Entities;

    public class Restore : Request<Link>
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public bool Overwrite { get; set; }
        public Restore() : base("/trash/resources/restore")
        {
            HttpMethod = HttpMethod.Put;
        }

        public override string BuildPath(string url)
        {
            Method += $"?path={WebUtility.UrlEncode(Path)}{(Name != null ? $"&name={Name}" : "")}{($"&overwrite={Overwrite}")}";
            return string.Format(url, Method);
        }
    }
}
