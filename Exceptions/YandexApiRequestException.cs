namespace YandexDisk.Api.Exceptions
{
    using System;
    using YandexDisk.Api.Entities;

    public class YandexApiRequestException : Exception
    {
        private string _message;
        protected YandexApiDiskError YandexApiDiskError;
        public override string Message => _message ?? YandexApiDiskError.Message;
        public YandexApiRequestException(string message)
        {
            _message = message;
        }

        public YandexApiRequestException(YandexApiDiskError diskError)
        {
            YandexApiDiskError = diskError;
        }
    }
}
