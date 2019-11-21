namespace YandexDisk.Api.Exceptions
{
    using System;
    public class YandexApiTokenException : Exception
    {
        private string ErrorMessage;
        public override string Message => ErrorMessage;
        public YandexApiTokenException()
        {
            ErrorMessage = "Токен имеет не верный формат.";
        }
        public YandexApiTokenException(string token)
        {
            ErrorMessage = $"Токен {token} имеет неверный формат.";
        }
    }
}
