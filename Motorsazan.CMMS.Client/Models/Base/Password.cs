using System.Text.RegularExpressions;
using Motorsazan.CMMS.Shared.Utilities;

namespace ClientApp.Models.Base
{
    public class Password
    {
        private string hashValue;

        public Reason Reason { set; get; }

        public string HashValue
        {
            set { hashValue = value; }
            get { return hashValue ?? InitiatePasswordHash(); }
        }

        public string PlainTextValue { set; get; }

        public bool PlainTextValueContentIsValid { set; get; }

        private string InitiatePasswordHash()
        {
            return Converter.ToSHA256Hash(PlainTextValue);
        }

        public bool DoesPlainTextContentValid()
        {
            var len = new Regex("^.{6,20}$");
            var space = new Regex("^\\S*$");
            var num = new Regex("\\d");
            var alpha = new Regex("\\D");


            if(!len.IsMatch(PlainTextValue))
            {
                PlainTextValueContentIsValid = false;
                Reason = new Reason("GC0003");
            }
            else if(!space.IsMatch(PlainTextValue))
            {
                PlainTextValueContentIsValid = false;
                Reason = new Reason("GC0003");
            }
            else if(!num.IsMatch(PlainTextValue))
            {
                PlainTextValueContentIsValid = false;
                Reason = new Reason("GC0003");
            }
            else if(!alpha.IsMatch(PlainTextValue))
            {
                PlainTextValueContentIsValid = false;
                Reason = new Reason("GC0003");
            }
            else
            {
                PlainTextValueContentIsValid = true;
            }

            return true;

        }
    }
}