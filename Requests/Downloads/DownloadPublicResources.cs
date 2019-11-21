namespace YandexDisk.Api.Requests
{
    using System.Net;
    using System.Net.Http;
    using YandexDisk.Api.Entities;

    public class DownloadPublicResources : Request<Link>
    {
        public string PublicKey { get; set; }
        public string Path { get; set; }

        public DownloadPublicResources() : base("/public/resources/download")
        {
            HttpMethod = HttpMethod.Get;
        }
        public override string BuildPath(string url)
        {
            Method += $"?public_key={WebUtility.UrlEncode(PublicKey)}{(Path != null ? $"&path={WebUtility.UrlEncode(Path)}" : "")}";
            return string.Format(url, Method);
        }
    }
}
