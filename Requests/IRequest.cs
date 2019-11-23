namespace YandexDisk.Api.Requests
{
    using System.Net.Http;
    using YandexDisk.Api.Entities;
    public interface IRequest<TEntity> where TEntity : IEntity
    {
        HttpMethod HttpMethod { get; set; }

        string Method { get; set; }

        string BuildPath(string url);
    }
}
