namespace YandexDisk.Api.Requests
{
    using System.Collections.Generic;
    using System.Net.Http;
    using YandexDisk.Api.Entities;
    using YandexDisk.Api.Enums;

    public class GetPublicResourcesList : Request<PublicResourcesList>
    {
        public IEnumerable<string> Fields { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public FileType Type { get; set; }
        public string PreviewSize { get; set; }

        public GetPublicResourcesList() : base("/resources/public")
        {
            HttpMethod = HttpMethod.Get;
        }

        public override string BuildPath(string url)
        {
            Method += $"?limit={Limit}{(Offset != 0 ? $"&offset={Offset}" : "")}{($"&type={Type.ToString()}")}{(Fields != null ? $"&fields={string.Join(",", Fields)}" : "")}{(PreviewSize != null ? $"&preview_size={PreviewSize}" : "")}";
            return string.Format(url, Method);
        }
    }
}
