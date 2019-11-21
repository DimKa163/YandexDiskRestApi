namespace YandexDisk.Api.Requests
{
    using System.Net;
    using System.Net.Http;
    using YandexDisk.Api.Entities;

    public class ClearTrash : Request<Link>
    {
        public string Path { get; set; }
        public ClearTrash() : base("/trash/resources")
        {
            HttpMethod = HttpMethod.Delete;
        }
        public override string BuildPath(string url)
        {
            Method += $"?{(Path != null ? $"path={WebUtility.UrlEncode(Path)}" : "")}";
            return string.Format(url, Method);
        }
    }
}
