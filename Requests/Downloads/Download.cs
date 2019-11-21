namespace YandexDisk.Api.Requests
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using YandexDisk.Api.Entities;

    public class Download : Request<Link>
    {
        public string Path { get; set; }
        public IEnumerable<string> Fields { get; set; }
        public Download() : base("/resources/download")
        {
            HttpMethod = HttpMethod.Get;
        }
        public override string BuildPath(string url)
        {
            Method += $"?path={WebUtility.UrlEncode(Path)}{(Fields != null ? $"&fields={string.Join(",", Fields)}" : "")}";
            return string.Format(url, Method);
        }
    }
}
