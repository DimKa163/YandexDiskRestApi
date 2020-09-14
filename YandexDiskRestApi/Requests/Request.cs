using System.Net.Http;
using YandexDisk.Api.Entities;

namespace YandexDisk.Api.Requests
{
    public abstract class Request<TEntity> : IRequest<TEntity> where TEntity : IEntity
    {
        public HttpMethod HttpMethod { get; set; }
        public string Method { get; set; }
        public Request(string method)
        {
            Method = method;
        }

        public abstract string BuildPath(string url);
    }
}
