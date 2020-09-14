namespace YandexDisk.Api.Requests
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using YandexDisk.Api.Entities;

    public class Move : Request<Link>
    {
        public string From { get; set; }
        public string Path { get; set; }
        public bool Overwrite { get; set; }
        public IEnumerable<string> Fields { get; set; }
        public Move() : base("/resources/move")
        {
            HttpMethod = HttpMethod.Post;
        }
        public override string BuildPath(string url)
        {
            Method += $"?from={WebUtility.UrlEncode(From)}&path={WebUtility.UrlEncode(Path)}{($"&overwrite={Overwrite}")}{(Fields != null ? $"&fields={string.Join(",", Fields)}" : "")}";
            return string.Format(url, Method);
        }
    }
}
