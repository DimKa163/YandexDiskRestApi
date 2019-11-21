namespace YandexDisk.Api.Requests
{
    using System.Net;
    using System.Net.Http;
    using YandexDisk.Api.Entities;

    class GetMetaDataAboutPublishResources : Request<Resource>
    {
        public string PublicKey { get; set; }
        public string Path { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public bool PreviewCrop { get; set; }
        public string PreviewSize { get; set; }
        public string Sort { get; set; }
        public GetMetaDataAboutPublishResources() : base("/public/resources")
        {
            HttpMethod = HttpMethod.Get;
        }
        public override string BuildPath(string url)
        {
            Method += $"?public_key={WebUtility.UrlEncode(PublicKey)}{(Path != null ? $"&path={WebUtility.UrlEncode(Path)}" : "")}{(Limit != 0 ? $"&limit={Limit}" : "")}" +
                $"{(Offset != 0 ? $"&offset={Offset}" : "")}{($"&preview_crop={PreviewCrop}")}{(PreviewSize != null ? $"&preview_size={PreviewSize}" : "")}{(Sort != null ? $"&sort={Sort}" : "")}";
            return string.Format(url, Method);
        }
    }
}
