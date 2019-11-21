namespace YandexDisk.Api.Requests
{
    using System.Net;
    using System.Net.Http;
    using YandexDisk.Api.Entities;

    class UnPublish : Request<Link>
    {
        public string Path { get; set; }
        public UnPublish() : base("/resources/unpublish")
        {
            HttpMethod = HttpMethod.Put;
        }
        public override string BuildPath(string url)
        {
            Method += $"?path={WebUtility.UrlEncode(Path)}";
            return string.Format(url, Method);
        }
    }
}
