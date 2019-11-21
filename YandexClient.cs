namespace YandexDisk.Api
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using YandexDisk.Api.Entities;
    using YandexDisk.Api.Enums;
    using YandexDisk.Api.Exceptions;
    using YandexDisk.Api.Requests;

    public class YandexClient : IYandexClient
    {
        protected HttpClient HttpClient;
        protected string Token;   
        protected string BaseUrl { get => "https://cloud-api.yandex.net/v1/disk{0}"; }
        public YandexClient(string token) : this(token, null)
        {

        }

        public YandexClient(string token, HttpClient client)
        {
            if (token != null && token.Length < 39)
                throw new YandexApiTokenException(token);
            Token = token;
            HttpClient = client ?? new HttpClient() { Timeout = new TimeSpan(1, 1, 1) };
        }

        /// <summary>
        /// Производит асинхронный запрос к API диска.
        /// </summary>
        /// <typeparam name="TEntity">Объект запроса.</typeparam>
        /// <param name="request">Типизированный запрос.</param>
        /// <param name="token"></param>
        /// <returns>Возвращает объект запроса.</returns>
        public async virtual Task<TEntity> MakeRequestAsync<TEntity>(IRequest<TEntity> request, CancellationToken token = default)
        {
            var path = request.BuildPath(BaseUrl);
            HttpRequestMessage message = new HttpRequestMessage(request.HttpMethod, path);
            message.Headers.Add("Authorization", $"OAuth {Token}");
            //message.Headers.Add("Content-Type", "application/json");
            HttpResponseMessage response = null;
            try
            {
                response = await HttpClient.SendAsync(message, token).ConfigureAwait(false);
            }
            catch (WebException e)
            {
                //response = (HttpResponseMessage)e.Response;
            }
            string responseAsString = await response.Content.ReadAsStringAsync();
            YandexApiDiskError error = null;
            switch (response.StatusCode)
            {
                case HttpStatusCode.Conflict:
                    error = JsonConvert.DeserializeObject<YandexApiDiskError>(responseAsString);
                    throw new YandexApiRequestException(error);
                case HttpStatusCode.Unauthorized:
                    error = JsonConvert.DeserializeObject<YandexApiDiskError>(responseAsString);
                    throw new YandexApiUnauthorizedException(error);
            }

            return JsonConvert.DeserializeObject<TEntity>(responseAsString);
        }
        #region About Disk
        /// <summary>
        /// API возвращает общую информацию о Диске пользователя: доступный объем, адреса системных папок и т. п.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<Disk> GetDiskInfo(CancellationToken token = default) => MakeRequestAsync(new DiskData(), token);
        #endregion
        #region Resources
        /// <summary>
        /// Зная ключ опубликованного ресурса или публичную ссылку на него, можно запросить метаинформацию об этом ресурсе (свойства файла или свойства и содержимое папки).
        /// </summary>
        /// <param name="publicKey">Ключ опубликованного ресурса или публичная ссылка на ресурс.</param>
        /// <param name="path">Относительный путь к ресурсу внутри публичной папки. Указывая ключ опубликованной папки в параметре public_key, вы можете запросить метаинформацию любого вложенного в нее ресурса. </param>
        /// <param name="limit">Количество ресурсов, вложенных в папку, описание которых следует вернуть в ответе (например, для постраничного вывода).</param>
        /// <param name="offset">Количество ресурсов с начала списка, которые следует опустить в ответе (например, для постраничного вывода).</param>
        /// <param name="preview_crop">Параметр позволяет обрезать превью согласно размеру, заданному в значении параметра preview_size.</param>
        /// <param name="preview_size">Требуемый размер уменьшенного изображения (превью файла), ссылку на которое Диск должен вернуть в ключе preview.</param>
        /// <param name="sort">Атрибут, по которому нужно сортировать список ресурсов, вложенных в папку. В качестве значения можно указывать имена следующих свойств объекта Resource.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<Resource> GetMetaDataAboutPublicResourcesAsync(string publicKey, string path, int limit = 0, int offset = 0,
            bool preview_crop = false, string preview_size = null, string sort = null, CancellationToken token = default) => MakeRequestAsync(new GetMetaDataAboutPublishResources
            {
                PublicKey = publicKey,
                Path = path,
                Limit = limit,
                Offset = offset,
                PreviewCrop = preview_crop,
                PreviewSize = preview_size,
                Sort = sort
            }, token);
        /// <summary>
        /// API возвращает список опубликованных ресурсов на Диске пользователя. Ресурсы в списке отсортированы от опубликованных позже к опубликованным раньше.
        /// Список можно фильтровать по типу ресурса — получать только файлы или только папки.
        /// </summary>
        /// <param name="limit">Количество опубликованных файлов, описание которых следует вернуть в ответе (например, для постраничного вывода).</param>
        /// <param name="offset">Количество ресурсов с начала списка, которые следует опустить в ответе (например, для постраничного вывода).</param>
        /// <param name="type">Тип ресурса</param>
        /// <param name="fields">Список свойств JSON, которые следует включить в ответ. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <param name="preview_size">Требуемый размер уменьшенного изображения (превью файла), ссылку на которое Диск должен вернуть в ключе preview</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<PublicResourcesList> GetPublicResourcesList(int limit = 20, int offset = 0, FileType type = FileType.file,
            IEnumerable<string> fields = null, string preview_size = null, CancellationToken token = default) => MakeRequestAsync(new GetPublicResourcesList()
            {
                Limit = limit,
                Offset = offset,
                Type = type,
                Fields = fields,
                PreviewSize = preview_size
            }, token);
        /// <summary>
        /// Запрашивать метаинформацию о файлах и папках можно, указав путь к соответствующему ресурсу на диске. Метаинформация включает в себя собственные свойства файлов и папок, а также свойства и содержимое вложенных папок.
        /// </summary>
        /// <param name="path">Путь к нужному ресурсу относительно корневого каталога Диска. Путь к ресурсу в Корзине следует указывать относительно корневого каталога Корзины.</param>
        /// <param name="fields">Список свойств JSON, которые следует включить в ответ. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <param name="limit">Количество ресурсов, вложенных в папку, описание которых следует вернуть в ответе (например, для постраничного вывода).</param>
        /// <param name="offset">Количество вложенных в папку ресурсов, которые следует опустить в ответе (например, для постраничного вывода). Список сортируется согласно значению параметра sort.</param>
        /// <param name="preview_crop">Параметр позволяет обрезать превью согласно размеру, заданному в значении параметра preview_size.</param>
        /// <param name="preview_size">Требуемый размер уменьшенного изображения (превью файла), ссылку на которое Диск должен вернуть в ключе preview.</param>
        /// <param name="sort">Атрибут, по которому нужно сортировать список ресурсов, вложенных в папку. В качестве значения можно указывать имена следующих свойств объекта Resource.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<ResourceList> GetMetaData(string path, IEnumerable<string> fields = null, int limit = 0, int offset = 0, bool preview_crop = false, SizeType? preview_size = null, SortType? sort = null, CancellationToken token = default) => MakeRequestAsync(new GetMetaData()
        {
            Path = path,
            Fields = fields,
            Limit = limit,
            Offset = offset,
            PreviewCrop = preview_crop,
            PreviewSize = preview_size,
            Sort = sort
        }, token);
        #endregion
        #region Folders
        /// <summary>
        /// Создавать папки на Диске можно, указывая требуемый путь к новой папке.
        /// </summary>
        /// <param name="path">Путь к создаваемой папке</param>
        /// <param name="fields">Список свойств JSON, которые следует включить в ответ. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <param name="token"></param>
        /// <returns>Возвращает объект ссылки Link.</returns>
        public Task<Link> CreateFolderAsync(string path, IEnumerable<string> fields = null, CancellationToken token = default) => MakeRequestAsync(new CreateFolder()
        {
            Path = path,
            Fields = fields
        }, token);
        /// <summary>
        /// Удалять файлы и папки на Диске пользователя можно, указывая путь к удаляемому ресурсу. Помните, что перемещение ресурсов в Корзину никак не влияет на доступное место на Диске. Чтобы освободить место, следует также удалять ресурсы из Корзины.
        /// </summary>
        /// <param name="path">Путь к удаляемому ресурсу.</param>
        /// <param name="permamently">Признак безвозвратного удаления.</param>
        /// <param name="fields">Список свойств JSON, которые следует включить в ответ. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<Link> DeleteResourceAsync(string path, bool permamently = false, IEnumerable<string> fields = null, CancellationToken token = default) => MakeRequestAsync(new Delete()
        {
            Path = path,
            Permamently = permamently,
            Fields = fields
        }, token);
        /// <summary>
        /// Перемещать файлы и папки на Диске можно, указывая текущий путь к ресурсу и его новое положение.
        /// </summary>
        /// <param name="from">Путь к перемещаемому ресурсу.</param>
        /// <param name="path">Путь к новому положению ресурса.</param>
        /// <param name="overwrite">Признак перезаписи файлов.</param>
        /// <param name="fields">Список свойств JSON, которые следует включить в ответ. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <param name="token"></param>
        /// <returns>Возвращает объект ссылки Link.</returns>
        public Task<Link> MoveAsync(string from, string path, bool overwrite = false, IEnumerable<string> fields = null, CancellationToken token = default) => MakeRequestAsync(new Move
        {
            From = from,
            Path = path,
            Overwrite = overwrite,
            Fields = fields
        }, token);
        /// <summary>
        /// Копировать файлы и папки на Диске пользователя можно, указывая путь к ресурсу и требуемый путь к его копии.
        /// </summary>
        /// <param name="from">Путь к копируемому ресурсу.</param>
        /// <param name="path">Путь к создаваемой копии ресурса.</param>
        /// <param name="overwrite">Признак перезаписи.</param>
        /// <param name="fields">Список свойств JSON, которые следует включить в ответ. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <param name="token"></param>
        /// <returns>Возвращает объект ссылки Link.</returns>
        public Task<Link> CopyAsync(string from, string path, bool overwrite = false, IEnumerable<string> fields = null, CancellationToken token = default) => MakeRequestAsync(new Copy
        {
            From = from,
            Path = path,
            Overwrite = overwrite,
            Fields = fields
        }, token);
        /// <summary>
        /// Файлы, перемещенные в Корзину, можно окончательно удалить. Корзина считается папкой на Диске, поэтому доступное на Диске место при этом увеличивается.
        /// </summary>
        /// <param name="path">Путь к удаляемому ресурсу относительно корневого каталога Корзины.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<Link> ClearTrashAsync(string path = null, CancellationToken token = default) => MakeRequestAsync(new ClearTrash
        {
            Path = path
        }, token);
        /// <summary>
        /// Перемещенный в Корзину ресурс можно восстановить на прежнем месте, указав путь к нему в корзине. Восстанавливаемый ресурс при этом можно переименовать.
        /// </summary>
        /// <param name="path">Путь к восстанавливаемому ресурсу относительно корневого каталога Корзины.</param>
        /// <param name="overwrite">Признак перезаписи. </param>
        /// <param name="name">Новое имя восстанавливаемого ресурса.</param>
        /// <param name="token"></param>
        /// <returns>Возвращает объект ссылки Link.</returns>
        public Task<Link> RestoreAsync(string path, bool overwrite = false, string name = null, CancellationToken token = default) => MakeRequestAsync(new Restore
        {
            Path = path,
            Overwrite = overwrite,
            Name = name
        }, token);
        #endregion
        #region Publications
        /// <summary>
        /// Ресурс становится доступен по прямой ссылке. Опубликовать ресурс можно только с OAuth-токеном владельца файла.
        /// </summary>
        /// <param name="path">Путь к публикуемому ресурсу.</param>
        /// <param name="token"></param>
        /// <returns>Возвращает объект ссылки Link.</returns>
        public Task<Link> PublishAsync(string path, CancellationToken token = default) => MakeRequestAsync(new Publish
        {
            Path = path
        }, token);
        /// <summary>
        /// Ресурс теряет атрибуты public_key и public_url, публичные ссылки на него перестают работать. Закрыть доступ к ресурсу можно только с OAuth-токеном владельца ресурса.
        /// </summary>
        /// <param name="path">Путь к закрываемому ресурсу. </param>
        /// <param name="token"></param>
        /// <returns>Возвращает объект ссылки Link.</returns>
        public Task<Link> UnPublishAsync(string path, CancellationToken token = default) => MakeRequestAsync(new UnPublish
        {
            Path = path
        }, token);
        #endregion
        #region Downloads
        /// <summary>
        /// Чтобы получить URL для непосредственной загрузки файла, необходимо передать API путь на Диске, по которому загруженный файл должен быть доступен.
        /// </summary>
        /// <param name="path">Путь к скачиваемому файлу.</param>
        /// <param name="fields">Список свойств JSON, которые следует включить в ответ. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <param name="token"></param>
        /// <returns>Возвращает объект ссылки Link.</returns>
        public Task<Link> GetDownloadLinkAsync(string path, IEnumerable<string> fields = null, CancellationToken token = default) => MakeRequestAsync(new Download
        {
            Path = path,
            Fields = fields
        }, token);
        /// <summary>
        /// Сообщив API Диска желаемый путь для загружаемого файла, вы получаете URL для обращения к загрузчику файлов.
        /// </summary>
        /// <param name="path">Путь, по которому следует загрузить файл.</param>
        /// <param name="overwrite">Признак перезаписи файла.</param>
        /// <param name="fields">Список свойств JSON, которые следует включить в ответ. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <param name="token"></param>
        /// <returns>Возвращает объект ссылки Link.</returns>
        public Task<Link> GetUploadLinkAsync(string path, bool overwrite = false, IEnumerable<string> fields = null, CancellationToken token = default) => MakeRequestAsync(new Upload
        {
            Path = path,
            Overwrite = overwrite,
            Fields = fields
        }, token);
        /// <summary>
        /// Яндекс.Диск может скачать файл на Диск пользователя. Для этого следует передать в запросе URL файла. Если при скачивании возникла ошибка, Диск не будет пытаться скачать файл еще раз.
        /// </summary>
        /// <param name="url">Ссылка на скачиваемый файл. </param>
        /// <param name="path">Путь на Диске, по которому должен быть доступен скачанный файл.</param>
        /// <param name="fields">Список свойств JSON, которые следует включить в ответ. Если параметр не указан, ответ возвращается полностью, без сокращений.</param>
        /// <param name="disable_redirects">Параметр помогает запретить редиректы по адресу, заданному в параметре url.</param>
        /// <param name="token"></param>
        /// <returns>Возвращает объект ссылки Link.</returns>
        public Task<Link> UploadFileFromInternetAsync(string url, string path, IEnumerable<string> fields = null, bool disable_redirects = false,
            CancellationToken token = default) => MakeRequestAsync(new UploadFromInternet
            {
                Url = url,
                Path = path,
                Fields = fields,
                DisableRedirects = disable_redirects
            }, token);
        /// <summary>
        /// Ресурс, опубликованный на Диске, можно скачать, зная его ключ или публичную ссылку на него. Также этой операцией можно скачивать отдельные файлы из публичных папок.
        /// </summary>
        /// <param name="publicKey">Ключ публичного файла или папки.</param>
        /// <param name="path">Путь к файлу внутри публичной папки. </param>
        /// <param name="token"></param>
        /// <returns>Возвращает объект ссылки Link.</returns>
        public Task<Link> GetDownloadLinkOfPublicResources(string publicKey, string path, CancellationToken token = default) => MakeRequestAsync(new DownloadPublicResources
        {
            PublicKey = publicKey,
            Path = path
        }, token);
        /// <summary>
        /// Файл, опубликованный на Диске, можно скопировать в папку «Загрузки» на Диске пользователя. Для этого нужно знать ключ файла или публичную ссылку на него. Если вы знаете ключ публичной папки, вы также можете копировать отдельные файлы из нее.
        /// </summary>
        /// <param name="publicKey">Ключ сохраняемого ресурса.</param>
        /// <param name="path">Путь внутри публичной папки. Следует указать, если в значении параметра public_key передан ключ публичной папки, в которой находится нужный файл.</param>
        /// <param name="name">Имя, под которым файл следует сохранить в папку «Загрузки».</param>
        /// <param name="token"></param>
        /// <returns>Возвращает объект ссылки Link.</returns>
        public Task<Link> SavePublicResourceInDownloadsAsync(string publicKey, string path = null, string name = null, CancellationToken token = default) => MakeRequestAsync(new SavePublicResourcesInDownloads
        {
            PublicKey = publicKey,
            Path = path,
            Name = name
        }, token);
        #endregion
    }
}
