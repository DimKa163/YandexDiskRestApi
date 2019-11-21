namespace YandexDisk.Api.Requests
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using YandexDisk.Api.Entities;

    public class UploadFromInternet : Request<Link>
    {
        public string Url { get; set; }
        public string Path { get; set; }
        public IEnumerable<string> Fields { get; set; }
        public bool DisableRedirects { get; set; }
        public UploadFromInternet() : base("/resources/upload")
        {
            HttpMethod = HttpMethod.Post;
        }
        public override string BuildPath(string url)
        {
            Method += $"?url={WebUtility.UrlEncode(Url)}&path={WebUtility.UrlEncode(Path)}{(Fields != null ? $"&fields={string.Join(",", Fields)}" : "")}{$"&disable_redirects={DisableRedirects}"}";
            return string.Format(url, Method);
        }
    }
}
