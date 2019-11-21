namespace YandexDisk.Api.Requests
{
    using System.Net.Http;

    public interface IRequest<TEntity>
    {
        HttpMethod HttpMethod { get; set; }

        string Method { get; set; }

        string BuildPath(string url);
    }
}
