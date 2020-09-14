namespace YandexDisk.Api.Exceptions
{
    using System;
    using YandexDisk.Api.Entities;
    public class YandexApiUnauthorizedException : Exception
    {
        private string _message;
        protected YandexApiDiskError YandexApiDiskError;

        public override string Message => _message ?? YandexApiDiskError.Message;
        public YandexApiUnauthorizedException(string message)
        {
            _message = message;
        }

        public YandexApiUnauthorizedException(YandexApiDiskError diskError)
        {
            YandexApiDiskError = diskError;
        }
    }
}
