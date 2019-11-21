namespace YandexDisk.Api.Requests
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using YandexDisk.Api.Entities;

    public class Upload : Request<Link>
    {
        public string Path { get; set; }
        public bool Overwrite { get; set; }
        public IEnumerable<string> Fields { get; set; }
        public Upload() : base("/resources/upload")
        {
            HttpMethod = HttpMethod.Get;
        }
        public override string BuildPath(string url)
        {
            Method += $"?path={WebUtility.UrlEncode(Path)}{($"&overwrite={Overwrite}")}{(Fields != null ? $"&fields={string.Join(",", Fields)}" : "")}";
            return string.Format(url, Method);
        }
    }
}
