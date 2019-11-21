namespace YandexDisk.Api.Requests
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using YandexDisk.Api.Entities;

    public class Delete : Request<Link>
    {
        public string Path { get; set; }
        public bool Permamently { get; set; }
        public IEnumerable<string> Fields { get; set; }
        public Delete() : base("/resources")
        {
            HttpMethod = HttpMethod.Delete;
        }

        public override string BuildPath(string url)
        {
            Method += $"?path={WebUtility.UrlEncode(Path)}{($"&permanently={Permamently}")}{(Fields != null ? $"&fields={string.Join(",", Fields)}" : "")}";
            return string.Format(url, Method);
        }
    }
}
