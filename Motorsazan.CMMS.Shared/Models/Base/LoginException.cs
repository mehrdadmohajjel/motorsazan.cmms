using System;

namespace Motorsazan.CMMS.Shared.Models.Base
{
    public class LoginException : Exception
    {
        public LoginException() { }

        public LoginException(string message = "کاربر شناسایی نشد، برای ادامه فرآیند بایستی مجددا وارد حساب کاربری خود شوید") : base(message)
        {
        }

        public LoginException(string message, Exception inner) : base(message, inner) { }
    }
}