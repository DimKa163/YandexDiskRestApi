namespace YandexDisk.Api.Requests
{
    using System;
    using System.Collections.Generic;
    using YandexDisk.Api.Entities;

    public class AddMetaInfo : Request<Resource>
    {
        public string Path { get; set; }

        public IEnumerable<string> Fields { get; set; }

        public AddMetaInfo():base("/resources")
        {

        }

        public override string BuildPath(string url)
        {
            Method += $"path={Path}{(Fields != null ? $"&fields={string.Join(",", Fields)}" : "")}";
            return string.Format(url, Method);
        }
    }
}
