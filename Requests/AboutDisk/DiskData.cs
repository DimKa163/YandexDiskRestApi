namespace YandexDisk.Api.Requests
{
    using System.Net.Http;
    using YandexDisk.Api.Entities;

    public class DiskData : Request<Disk>
    {
        public DiskData():base("")
        {
            HttpMethod = HttpMethod.Get;
        }

        public override string BuildPath(string url)
        {
            return string.Format(url, Method);
        }
    }
}
