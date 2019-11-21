namespace YandexDisk.Api.Requests
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using YandexDisk.Api.Entities;
    using YandexDisk.Api.Enums;

    public class GetMetaData : Request<ResourceList>
    {
        public string Path { get; set; }
        public IEnumerable<string> Fields { get; set; }

        public int Limit { get; set; }

        public int Offset { get; set; }

        public bool PreviewCrop { get; set; }
        public SizeType? PreviewSize { get; set; }

        public SortType? Sort { get; set; }
        public GetMetaData() : base("/resources")
        {
            HttpMethod = HttpMethod.Get;
        }
        public override string BuildPath(string url)
        {
            Method += $"?path={WebUtility.UrlEncode(Path)}{(Fields != null ? $"&fields={string.Join(",", Fields)}" : "")}{(Limit != 0 ? $"&limit={Limit}" : "")}" +
                $"{(Offset != 0 ? $"&offset={Offset}" : "")}{($"&preview_crop={PreviewCrop}")}{(PreviewSize != null ? $"&preview_size={PreviewSize.Value.ToString()}" : "")}{(Sort != null ? $"&sort={Sort.Value.ToString()}" : "")}";
            return string.Format(url, Method);
        }
    }
}
