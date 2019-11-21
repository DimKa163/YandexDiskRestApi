using System.Threading;
using System.Threading.Tasks;
using YandexDisk.Api.Requests;

namespace YandexDisk
{
    public interface IYandexClient
    {
        /// <summary>
        /// Производит асинхронный запрос к API диска.
        /// </summary>
        /// <typeparam name="TEntity">Объект запроса.</typeparam>
        /// <param name="request">Типизированный запрос.</param>
        /// <param name="token"></param>
        /// <returns>Возвращает объект запроса.</returns>
        Task<TEntity> MakeRequestAsync<TEntity>(IRequest<TEntity> request, CancellationToken token = default);
    }
}
