namespace YandexDisk.Api.Requests
{
    using System.Net;
    using System.Net.Http;
    using YandexDisk.Api.Entities;

    public class SavePublicResourcesInDownloads : Request<Link>
    {
        public string PublicKey { get; set; }
        public string Path { get; set; }

        public string Name { get; set; }

        public SavePublicResourcesInDownloads() : base("/resources/download")
        {
            HttpMethod = HttpMethod.Post;
        }
        public override string BuildPath(string url)
        {
            Method += $"?public_key={WebUtility.UrlEncode(PublicKey)}{(Path != null ? $"&path={WebUtility.UrlEncode(Path)}" : "")}{(Path != null ? $"&name={Name}" : "")}";
            return string.Format(url, Method);
        }
    }
}
